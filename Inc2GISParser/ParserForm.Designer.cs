namespace Inc2GISParser
{
    partial class ParserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParserForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCity = new System.Windows.Forms.RadioButton();
            this.radioButtonCapital = new System.Windows.Forms.RadioButton();
            this.buttonLoadLocations = new System.Windows.Forms.Button();
            this.comboBoxCities = new System.Windows.Forms.ComboBox();
            this.comboBoxCapitals = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownRPS = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialogOutput = new System.Windows.Forms.SaveFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelCsvFile = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxRequest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.parserConsole = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRPS)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCity);
            this.groupBox1.Controls.Add(this.radioButtonCapital);
            this.groupBox1.Controls.Add(this.buttonLoadLocations);
            this.groupBox1.Controls.Add(this.comboBoxCities);
            this.groupBox1.Controls.Add(this.comboBoxCapitals);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 127);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Место";
            // 
            // radioButtonCity
            // 
            this.radioButtonCity.AutoSize = true;
            this.radioButtonCity.Location = new System.Drawing.Point(236, 58);
            this.radioButtonCity.Name = "radioButtonCity";
            this.radioButtonCity.Size = new System.Drawing.Size(55, 17);
            this.radioButtonCity.TabIndex = 3;
            this.radioButtonCity.Text = "Город";
            this.radioButtonCity.UseVisualStyleBackColor = true;
            // 
            // radioButtonCapital
            // 
            this.radioButtonCapital.AutoSize = true;
            this.radioButtonCapital.Checked = true;
            this.radioButtonCapital.Location = new System.Drawing.Point(236, 23);
            this.radioButtonCapital.Name = "radioButtonCapital";
            this.radioButtonCapital.Size = new System.Drawing.Size(67, 17);
            this.radioButtonCapital.TabIndex = 2;
            this.radioButtonCapital.TabStop = true;
            this.radioButtonCapital.Text = "Столица";
            this.radioButtonCapital.UseVisualStyleBackColor = true;
            // 
            // buttonLoadLocations
            // 
            this.buttonLoadLocations.Location = new System.Drawing.Point(269, 98);
            this.buttonLoadLocations.Name = "buttonLoadLocations";
            this.buttonLoadLocations.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadLocations.TabIndex = 1;
            this.buttonLoadLocations.Text = "Загрузить";
            this.buttonLoadLocations.UseVisualStyleBackColor = true;
            this.buttonLoadLocations.Click += new System.EventHandler(this.buttonLoadLocations_Click);
            // 
            // comboBoxCities
            // 
            this.comboBoxCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCities.FormattingEnabled = true;
            this.comboBoxCities.Location = new System.Drawing.Point(33, 58);
            this.comboBoxCities.Name = "comboBoxCities";
            this.comboBoxCities.Size = new System.Drawing.Size(177, 21);
            this.comboBoxCities.TabIndex = 0;
            // 
            // comboBoxCapitals
            // 
            this.comboBoxCapitals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCapitals.FormattingEnabled = true;
            this.comboBoxCapitals.Location = new System.Drawing.Point(33, 19);
            this.comboBoxCapitals.Name = "comboBoxCapitals";
            this.comboBoxCapitals.Size = new System.Drawing.Size(177, 21);
            this.comboBoxCapitals.TabIndex = 0;
            this.comboBoxCapitals.SelectedIndexChanged += new System.EventHandler(this.comboBoxCapitals_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "1. Нажмите на кнопку загрузить районы и города\r\n и сделайте свой выбор.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDownRPS);
            this.groupBox2.Location = new System.Drawing.Point(12, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Задержка RPS (мс)";
            // 
            // numericUpDownRPS
            // 
            this.numericUpDownRPS.Location = new System.Drawing.Point(125, 44);
            this.numericUpDownRPS.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRPS.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownRPS.Name = "numericUpDownRPS";
            this.numericUpDownRPS.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownRPS.TabIndex = 0;
            this.numericUpDownRPS.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownRPS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDownRPS_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "2. Укажите время задержки в мс между обращениями к серверу 2ГИС.";
            // 
            // saveFileDialogOutput
            // 
            this.saveFileDialogOutput.DefaultExt = "csv";
            this.saveFileDialogOutput.Filter = "CSV Таблица (*.csv)|*.csv";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelCsvFile);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(12, 251);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 72);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Вывод";
            // 
            // labelCsvFile
            // 
            this.labelCsvFile.AutoSize = true;
            this.labelCsvFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCsvFile.Location = new System.Drawing.Point(132, 34);
            this.labelCsvFile.Name = "labelCsvFile";
            this.labelCsvFile.Size = new System.Drawing.Size(0, 13);
            this.labelCsvFile.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 22);
            this.button2.TabIndex = 0;
            this.button2.Text = "Указать файл";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(359, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "3. Укажите файл на диске куда будет сохраняться данные парсинга.";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxRequest);
            this.groupBox4.Location = new System.Drawing.Point(12, 329);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(412, 68);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Запрос";
            // 
            // textBoxRequest
            // 
            this.textBoxRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxRequest.Location = new System.Drawing.Point(9, 29);
            this.textBoxRequest.MaxLength = 500;
            this.textBoxRequest.Name = "textBoxRequest";
            this.textBoxRequest.Size = new System.Drawing.Size(381, 20);
            this.textBoxRequest.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "4. Укажите запрос к 2ГИС.";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.parserConsole);
            this.groupBox5.Controls.Add(this.btnStop);
            this.groupBox5.Controls.Add(this.btnStart);
            this.groupBox5.Location = new System.Drawing.Point(12, 405);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(631, 309);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Процесс";
            // 
            // parserConsole
            // 
            this.parserConsole.Location = new System.Drawing.Point(9, 74);
            this.parserConsole.Multiline = true;
            this.parserConsole.Name = "parserConsole";
            this.parserConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.parserConsole.Size = new System.Drawing.Size(616, 229);
            this.parserConsole.TabIndex = 1;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(395, 32);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(140, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Остановить";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(70, 32);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(140, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Запуск";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 726);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ParserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IncWell 2GIS Parser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParserForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRPS)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLoadLocations;
        private System.Windows.Forms.ComboBox comboBoxCities;
        private System.Windows.Forms.ComboBox comboBoxCapitals;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownRPS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialogOutput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxRequest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox parserConsole;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label labelCsvFile;
        private System.Windows.Forms.RadioButton radioButtonCity;
        private System.Windows.Forms.RadioButton radioButtonCapital;
    }
}

