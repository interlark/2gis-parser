using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inc2GISParser
{
    public partial class ParserForm : Form
    {
        private Parser parser = new Parser();
        private Dictionary<string, List<string>> cities;
        private CancellationTokenSource cts;

        public ParserForm()
        {
            InitializeComponent();
            WriteLog("Инициализация парсера...");
            parser.Init();
        }

        private void numericUpDownRPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialogOutput.ShowDialog() == DialogResult.OK)
            {
                labelCsvFile.Text = System.IO.Path.GetFileName(saveFileDialogOutput.FileName);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saveFileDialogOutput.FileName))
            {
                WriteLog("Не указан csv файл вывода! Отмена старта.");
            }

            if (string.IsNullOrEmpty(textBoxRequest.Text))
            {
                WriteLog("Запрос не выбран! Отмена старта.");
                return;
            }

            if (radioButtonCapital.Checked)
            {
                if (string.IsNullOrEmpty(comboBoxCapitals.Text))
                {
                    WriteLog("Город не выбран! Отмена старта.");
                    return;
                }

                WriteLog("Выбор города...");
                parser.EnterCity(comboBoxCapitals.Text);
            }
            else
            {
                if (string.IsNullOrEmpty(comboBoxCities.Text))
                {
                    WriteLog("Город не выбран! Отмена старта.");
                    return;
                }

                WriteLog("Выбор города...");
                parser.EnterCity(comboBoxCities.Text);
            }

            WriteLog("Ввод запроса...");
            parser.TypeRequest(textBoxRequest.Text);

            WriteLog("Выполняем поиск...");
            parser.PressSearch();

            cts = new CancellationTokenSource();
            Task.Factory.StartNew(
                () => parser.Parsing(saveFileDialogOutput.FileName, radioButtonCapital.Checked, WriteLog,
                    ClearParsing, (int) numericUpDownRPS.Value, cts.Token), cts.Token);

            btnStop.Enabled = true;
            btnStart.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
        }

        private void buttonLoadLocations_Click(object sender, EventArgs e)
        {
            WriteLog("Загрузка городов...");

            //set up on 1st elements
            cities = parser.GetCities();
            //comboBoxCapitals.DataSource = new BindingSource(cities, null);
            //comboBoxCapitals.DisplayMember = "Key";
            comboBoxCapitals.Items.AddRange(cities.Select(n => n.Key).Cast<object>().ToArray());
            comboBoxCapitals.SelectedIndex = 0;

            buttonLoadLocations.Enabled = false;
        }

        private void comboBoxCapitals_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var bs = new BindingSource {DataSource = cities.ElementAt(comboBoxCapitals.SelectedIndex).Value};
            //comboBoxCities.DataSource = bs;
            comboBoxCities.Items.Clear();
            comboBoxCities.Items.AddRange(cities.ElementAt(comboBoxCapitals.SelectedIndex).Value.Cast<object>().ToArray());
            if (comboBoxCities.Items.Count > 0)
            {
                comboBoxCities.SelectedIndex = 0;
            }
        }

        private void WriteLog(string line)
        {
            if (parserConsole.InvokeRequired)
            {
                parserConsole.Invoke(new Action(() => parserConsole.AppendText(line + Environment.NewLine)));
            }
            else
            {
                parserConsole.AppendText(line + Environment.NewLine);
            }
        }

        private void ClearParsing()
        {
            if (comboBoxCapitals.InvokeRequired)
            {
                comboBoxCapitals.Invoke(new Action(() =>
                {
                    comboBoxCapitals.DataSource = null;
                    comboBoxCities.DataSource = null;
                    comboBoxCapitals.Items.Clear();
                    comboBoxCities.Items.Clear();
                    buttonLoadLocations.Enabled = true;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = true;
                    btnStop.Enabled = false;
                    btnStart.Enabled = true;
                    comboBoxCapitals.Text = string.Empty;
                    comboBoxCities.Text = string.Empty;
                    comboBoxCapitals.Enabled = true;
                    comboBoxCities.Enabled = true;
                }));
            }
            else
            {
                comboBoxCapitals.DataSource = null;
                comboBoxCities.DataSource = null;
                comboBoxCapitals.Items.Clear();
                comboBoxCities.Items.Clear();
                buttonLoadLocations.Enabled = true;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                btnStop.Enabled = false;
                btnStart.Enabled = true;
                comboBoxCapitals.Text = string.Empty;
                comboBoxCities.Text = string.Empty;
                comboBoxCapitals.Enabled = true;
                comboBoxCities.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            ClearParsing();
        }

        private void ParserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parser.Quit();
        }
    }
}
