using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CsvHelper;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Inc2GISParser
{
    public class Parser: IDisposable
    {
        private IWebDriver _driver;
        private readonly string _rootURL = "https://2gis.ru";
        private readonly TimeSpan _waitForElement = TimeSpan.FromSeconds(36);
        private CsvWriter _csv = null;
        public Parser()
        {
        }

        public void Init()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Navigate().GoToUrl(_rootURL);
        }

        public void InitCsv(string csvPath)
        {
            var sw = new StreamWriter(csvPath, true, Encoding.GetEncoding(1251)) { AutoFlush = true };

            _csv = new CsvWriter(sw);
            _csv.Configuration.Delimiter = ";";
            _csv.WriteHeader<Data>();
            _csv.NextRecord();
            _csv.Flush();

        }

        public void EnterCity(string cityname)
        {
            var pageSource = _driver.PageSource;
            var doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            var cityNodes = doc.DocumentNode.SelectSingleNode(@"//*/header/div[text()=""Россия""]/parent::*/following-sibling::*");

            var cityNode = cityNodes.Descendants().FirstOrDefault(n => n.InnerText == cityname);

            if (cityNode == null)
            {
                throw new ApplicationException("Внутренняя ошибка парсера. Не могу выделить ноду города.");
            }

            var element = _driver.FindElement(By.XPath(cityNode.XPath));
            HoverAndClick(element);
        }

        public void TypeRequest(string request)
        {
            WaitForAllPreloaders();
            WaitForReadyByCss(".online__map");
            var element = WaitForReadyByCss(@"input[name=""search[query]""]");
            HoverAndClick(element);
            element.Clear();
            element.SendKeys(request);
        }

        //Основной метод парсинга, лучше запускать в отдельном потоке
        public void Parsing(string csvPath, bool isCapital, Action<string> console, 
            Action uiFinaly, int rpsDelay, CancellationToken cancelToken)
        {
            WaitForAllPreloaders();

            console("Ожидание построение фреймов...");
            Thread.Sleep(8500); // Ожидание построение фрейма

            if (!isCapital)
            {
                //toggle filter button
                var element = WaitForReadyByCss("label[title='На карте']");
                HoverAndClick(element);
                console("Фильтрация по выбранному региону...");
                Thread.Sleep(8500); // Ожидание построение фрейма
                console("Ожидание построение фреймов...");
                //фильтруем и ждем окончания фильтрации - нам нужны результаты по региону, если выбрали регион
                
                WaitForFiltering();
            }

            WaitForAllPreloaders();

            console("Инициализация таблицы csv");

            InitCsv(csvPath);

            console("Парсинг...");

            while (true)
            {
                try
                {
                    WaitForReadyByCss(@".searchResults__list");

                    var elements =
                        _driver.FindElements(By.XPath(@"//*/div[contains(@class, 'searchResults__list')]/article"));

                    //По очереди кликаем на айтемы по id. Параллельно следим за классом _busy двух фреймов
                    foreach (var element in elements)
                    {
                        HoverAndClick(element.FindElement(By.CssSelector(".miniCard__headerTitle")));

                        //wait for frames
                        //WaitForReadyByCss(@"[data-module=""frame""]:not(._busy)", 1);
                        cancelToken.ThrowIfCancellationRequested();

                        WaitForAllPreloaders();

                        Thread.Sleep(rpsDelay);

                        ScrapeData(csvPath, console);
                    }

                    try
                    {
                        _driver.FindElement(
                            By.XPath(
                                "//*/div[contains(@class, 'pagination')]/div[contains(@class, 'pagination__arrow') and contains(@class ,'_right') and contains(@class ,'_disabled')]"));

                        console("Парсинг окончен...");
                        uiFinaly();

                        return;
                    }
                    catch (NoSuchElementException)
                    {
                        // continue...
                    }

                    var nextPage = _driver.FindElement(
                        By.XPath(
                            "//*/div[contains(@class, 'pagination')]/div[contains(@class, 'pagination__arrow') and contains(@class ,'_right')]"));

                    cancelToken.ThrowIfCancellationRequested();

                    console("Переход на следующую страницу...");

                    HoverAndClick(nextPage);

                    //wait for frames
                    //WaitForReadyByCss(@"[data-module=""frame""]:not(._busy)", 1);

                    cancelToken.ThrowIfCancellationRequested();

                    WaitForAllPreloaders();

                    Thread.Sleep(rpsDelay);
                }
                catch (OperationCanceledException)
                {
                    console("Парсинг был отменен!");
                    uiFinaly();
                    return;
                }
                catch (Exception e)
                {
                    console($"Произошла ошибка во время парсинга:{Environment.NewLine}{e.Message}");
                    return;
                }
            }
        }

        private void ScrapeData(string csvPath, Action<string> console)
        {
            WaitForAllPreloaders();

            Thread.Sleep(4500);

            IWebElement element = null;
            try
            {
                element = _driver.FindElement(By.CssSelector(".card__wrap  .card__content"));
            }
            catch (NoSuchElementException)
            {
                try
                {
                    element = _driver.FindElement(By.CssSelector(".frame__content"));
                }
                catch (NoSuchElementException)
                {
                    throw new ApplicationException("Не найден фрейм с информацией");
                }
            }

            IWebElement address = null;
            try
            {
                address = element.FindElement(By.CssSelector(".card__addressPart .card__addressLink"));
            }
            catch (NoSuchElementException)
            {
                try
                {
                    address = _driver.FindElement(By.CssSelector(".mediaCardHeader__cardAddressName"));
                }
                catch (NoSuchElementException)
                {
                    throw new ApplicationException("Не найден адрес");
                }
            }

            IWebElement name = null;
            try
            {
                name = _driver.FindElement(By.CssSelector(".card__wrap .cardHeader__headerNameText"));
            }
            catch (NoSuchElementException)
            {
                try
                {
                    name = _driver.FindElement(By.CssSelector(".mediaCardHeader__cardHeaderName"));
                }
                catch (NoSuchElementException)
                {
                    throw new ApplicationException("Не найдено наименование организации");
                }
            }

            ReadOnlyCollection<IWebElement> cites = null;
            try
            {
                cites = element.FindElements(By.CssSelector("div.contact__websites > div> a"));
            }
            catch (NoSuchElementException)
            {
                try
                {
                    cites = _driver.FindElements(By.CssSelector(".mediaContacts__groupItem._website> a"));
                }
                catch (NoSuchElementException)
                {
                }
                // nothing
            }

            if (cites == null || cites.Count == 0)
            {
                try
                {
                    cites = _driver.FindElements(By.CssSelector(".mediaContacts__groupItem._website> a"));
                }
                catch (NoSuchElementException)
                {
                }
                // nothing
            }


            IWebElement vk = null;
            try
            {
                vk = element.FindElement(By.CssSelector("._type_vkontakte > a"));
                // vk -> link -> base64 part -> decode structure -> take non-json part
            }
            catch (NoSuchElementException)
            {
                try
                {
                    vk =
                        _driver.FindElement(
                            By.CssSelector(".mediaContacts__socialItem.mediaContacts__socialItem._vkontakte"));
                }
                catch (NoSuchElementException)
                {
                    // nothing
                }
            }

            IWebElement phoneHider = null;
            //perform click to resove phone
            try
            {
                phoneHider = element.FindElement(By.CssSelector(".contact__phonesFadeShow"));
            }
            catch (NoSuchElementException)
            {
                try
                {
                    phoneHider = _driver.FindElement(By.CssSelector(".mediaContacts__block .mediaContacts__showPhones"));
                }
                catch (NoSuchElementException)
                {
                    // nothing
                }
            }

            var phonesLst = new List<string>();
            if (phoneHider != null)
            {
                //Есть телефон - круто
                HoverAndClick(phoneHider);
                WaitForAllPreloaders();

                var phones = element.FindElements(By.CssSelector(".contact__phonesItem._type_phone>a>.contact__phonesItemLinkNumber"));

                if (phones == null || phones.Count == 0)
                {
                    phones = _driver.FindElements(By.CssSelector(".mediaContacts__block a.mediaContacts__phonesNumber"));
                }

                if (phones != null)
                {
                    phonesLst.AddRange(phones.Select(x => x.GetAttribute("innerText")));
                }
            }

            //unwrap all elements to text
            var citesLst = new List<string>();
            if (cites != null && cites.Count > 0)
            {
                citesLst.AddRange(cites.Select(x => x.GetAttribute("innerText")));
            }

            var vkstr = string.Empty;
            if (vk != null)
            {
                var vktmp = vk.GetAttribute("href");
                if (!vktmp.StartsWith("https://vk"))
                {
                    vktmp = vktmp.Replace("http://link.2gis.ru/1.4/", "");
                    vktmp = Encoding.UTF8.GetString(Convert.FromBase64String(vktmp));
                    vkstr = vktmp.Split(new[] {'\r', '\n'}).FirstOrDefault();
                }
                else
                {
                    vkstr = vktmp;
                }
            }

            var nameStr = string.Empty;
            nameStr = name.GetAttribute("innerText");

            var addressStr = string.Empty;
            if (address != null)
            {
                addressStr = address.GetAttribute("innerText");
            }

            console("{");

            if (!string.IsNullOrEmpty(nameStr))
            {
                console($"Организация: {nameStr}");
            }

            if (!string.IsNullOrEmpty(addressStr))
            {
                console($"Адрес: {addressStr}");
            }

            if (phonesLst.Count > 0)
            {
                var label = "Телефон";
                if (phonesLst.Count > 1)
                {
                    label += 'ы';
                }

                console($"{label}: {string.Join(",", phonesLst)}");
            }

            if (citesLst.Count > 0)
            {
                var label = "Сайт";
                if (citesLst.Count > 1)
                {
                    label += 'ы';
                }

                console($"{label}: {string.Join("," , citesLst)}");
            }

            if (!string.IsNullOrEmpty(vkstr))
            {
                console($"Вконтакте: {vkstr}");
            }

            var data = new Data(nameStr, addressStr, string.Join(",", phonesLst), string.Join(",", citesLst), vkstr, _csv);
            data.WriteCSV(csvPath);

            console("}");

            CloseContentFrame();
        }

        private void CloseContentFrame()
        {
            IWebElement element = null;
            try
            {
                element =
                    _driver.FindElements(By.CssSelector(".frame__content a.link.frame__controlsButton._close"))
                        .LastOrDefault();

                if (element != null)
                {
                    HoverAndClick(element);
                    WaitForAllPreloaders();
                    Thread.Sleep(2000);
                }
            }
            catch (NoSuchElementException)
            {
                try
                {
                    // nothing
                }
                catch (NoSuchElementException)
                {
                    // nothing
                }
            }
        }

        private class Data
        {
            public string ORG { get; set; }
            public string ADDRESS { get; set; }

            public string TEL { get; set; }

            public string CITE { get; set; }

            public string VK { get; set; }

            private CsvWriter _csv { get; set; }

            public Data(string organization, string address, string telephone, string cite, string vk, CsvWriter csv)
            {
                VK = vk;
                CITE = cite;
                TEL = telephone;
                ADDRESS = address;
                ORG = organization;

                if (string.IsNullOrEmpty(VK))
                {
                    VK = "-";
                }

                if (string.IsNullOrEmpty(CITE))
                {
                    CITE = "-";
                }

                if (string.IsNullOrEmpty(TEL))
                {
                    TEL = "-";
                }

                _csv = csv;
            }

            public void WriteCSV(string csvPath)
            {
                _csv.WriteRecord(this);
                _csv.NextRecord();
                _csv.Flush();

            }
        }

        public void PressSearch()
        {
            var element = WaitForReadyByCss(@".searchBar__form>button.searchBar__submit._directory[type=""submit""]");
            HoverAndClick(element);
        }

        //private void PerformClick(string selector)
        //{
        //IWebElement link = _driver.FindElement(By.XPath("//a[starts-with(@href, 'https://2gis.ru')]"));

        //String mouseOverScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');evObj.initEvent('mouseover', " +
        //        "true, false); arguments[0].dispatchEvent(evObj);" +
        //        "} else if(document.createEventObject)" +
        //            "{ arguments[0].fireEvent('onmouseover');}";

        //String onClickScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');evObj.initEvent('click', " +
        //        "true, false); arguments[0].dispatchEvent(evObj);" +
        //        "} else if(document.createEventObject)" +
        //        "{ arguments[0].fireEvent('onclick');}";

        //((IJavaScriptExecutor)_driver).ExecuteScript(mouseOverScript, link);
        //((IJavaScriptExecutor)_driver).ExecuteScript(onClickScript, link);
        //}

        private void GoCityListPage()
        {
            const string cssLogo = "div.searchBar__forms > div > a";
            WaitForReadyByCss(cssLogo);
            var element = _driver.FindElement(By.CssSelector(cssLogo));
            HoverAndClick(element);

            const string cssSelector = "div.dashboard__wrap > div.dashboard__body > div.dashboard__city > h1 > a > span";
            WaitForReadyByCss(cssSelector);
            element = _driver.FindElement(By.CssSelector(cssSelector));
            HoverAndClick(element);

            //Open russian tab
            const string cssSelector2 = "div.world__main > div > div.world__countries > section:nth-child(1)";
            WaitForReadyByCss(cssSelector2);
            element = _driver.FindElement(By.XPath(@"//*/header/div[text()=""Россия""]"));
            HoverAndClick(element);
        }

        private void HoverAndClick(IWebElement element)
        {
            HoverAndClick(element, element);
        }

        private void HoverAndClick(IWebElement elementToHover, IWebElement elementToClick)
        {
            var action = new Actions(_driver);
            action.MoveToElement(elementToHover).Click(elementToClick).Build().Perform();
        }

        private IWebElement WaitForReadyByXPath(string xpath)
        {
            var wait = new WebDriverWait(_driver, _waitForElement);
            return wait.Until(d => d.FindElement(By.XPath(xpath)));
        }

        private void WaitForFiltering()
        {
            var wait = new WebDriverWait(_driver, _waitForElement);
            wait.Until(d => d.FindElements(By.CssSelector("._filtering_inProgress")).Count == 0);
        }

        /// <summary>
        /// Другой подход к ожиданию ajax запросов
        /// </summary>
        private void WaitForAllPreloaders()
        {
            var wait = new WebDriverWait(_driver, _waitForElement);
            wait.Until(d =>
            {
                var isHavePreloaders = (bool)((IJavaScriptExecutor)d).
                    ExecuteScript("if (window.jQuery) { var res = false; var preloaders = $('.preloader'); for (var i = 0; i < preloaders.length; i++) { if ($(preloaders[i]).css('display') !== 'none') { res = true; break; } } return res; } return true; /*still loading*/");
                return !isHavePreloaders;
            });
        }

        private IWebElement WaitForReadyByCss(string selector, int count = 0)
        {
            var wait = new WebDriverWait(_driver, _waitForElement);
            wait.Until(d => d.FindElements(By.CssSelector(selector)).Count > count);
            //wait.Until(d =>
            //{
            //    var isElementFound = (bool)((IJavaScriptExecutor)d).
            //        ExecuteScript($"return $('{selector}').length > {count}");
            //    return isElementFound;
            //});

            return _driver.FindElement(By.CssSelector(selector));
        }

        public Dictionary<string, List<string>> GetCities()
        {
            GoCityListPage();

            var pageSource = _driver.PageSource;
            var doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            var cityNodes = doc.DocumentNode.SelectNodes(@"//*/header/div[text()=""Россия""]/parent::*/following-sibling::*/li");

            var res = new Dictionary<string, List<string>>();

            foreach (var cityNode in cityNodes)
            {
                var mainCity = HapByClass(cityNode, "world__listItemNameLink").First().InnerText;
                var regionCities = HapByClass(cityNode, "world__settlementsItemLink").Select(n => n.InnerText).ToList();
                res.Add(mainCity, regionCities);
            }

            return res;
        }

        private IEnumerable<HtmlNode> HapByClass(HtmlNode node, string @class)
        {
            return node.Descendants().Where(n => n.HasClass(@class));
        }

        public void Quit()
        {
            _driver.Quit();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _driver.Close();
                    _driver.Dispose();

                    _csv.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Parser() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
