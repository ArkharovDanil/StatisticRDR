namespace StatisticRDR
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLibrary = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.comboBoxStatForms = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.показатьСпискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокБиблиотекToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.списокКатегорийToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьСпискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокБиблиотекToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокКатегорийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменитьВерсиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.основнаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тестоваяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начальноеСостояниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПриложенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПодключенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПоискаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.настройкаВыводаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(692, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "Запуск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(436, 114);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(125, 30);
            this.textBoxDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дата";
            // 
            // comboBoxLibrary
            // 
            this.comboBoxLibrary.FormattingEnabled = true;
            this.comboBoxLibrary.Location = new System.Drawing.Point(222, 114);
            this.comboBoxLibrary.Name = "comboBoxLibrary";
            this.comboBoxLibrary.Size = new System.Drawing.Size(121, 33);
            this.comboBoxLibrary.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Библиотека";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(692, 114);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(243, 29);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "По всем библиотекам";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(692, 162);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(279, 29);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "По выбранной библиотеке";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Location = new System.Drawing.Point(17, 220);
            this.textBoxAnswer.Multiline = true;
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.ReadOnly = true;
            this.textBoxAnswer.Size = new System.Drawing.Size(254, 130);
            this.textBoxAnswer.TabIndex = 7;
            // 
            // comboBoxStatForms
            // 
            this.comboBoxStatForms.FormattingEnabled = true;
            this.comboBoxStatForms.Location = new System.Drawing.Point(17, 113);
            this.comboBoxStatForms.Name = "comboBoxStatForms";
            this.comboBoxStatForms.Size = new System.Drawing.Size(196, 33);
            this.comboBoxStatForms.TabIndex = 8;
            this.comboBoxStatForms.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatForms_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Стат. форма";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Статус";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.показатьСпискиToolStripMenuItem,
            this.изменитьСпискиToolStripMenuItem,
            this.сменитьВерсиюToolStripMenuItem,
            this.начальноеСостояниеToolStripMenuItem,
            this.настройкаПриложенияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // показатьСпискиToolStripMenuItem
            // 
            this.показатьСпискиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокБиблиотекToolStripMenuItem1,
            this.списокКатегорийToolStripMenuItem1});
            this.показатьСпискиToolStripMenuItem.Name = "показатьСпискиToolStripMenuItem";
            this.показатьСпискиToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.показатьСпискиToolStripMenuItem.Text = "Показать списки";
            // 
            // списокБиблиотекToolStripMenuItem1
            // 
            this.списокБиблиотекToolStripMenuItem1.Name = "списокБиблиотекToolStripMenuItem1";
            this.списокБиблиотекToolStripMenuItem1.Size = new System.Drawing.Size(220, 26);
            this.списокБиблиотекToolStripMenuItem1.Text = "Список библиотек";
            this.списокБиблиотекToolStripMenuItem1.Click += new System.EventHandler(this.списокБиблиотекToolStripMenuItem1_Click);
            // 
            // списокКатегорийToolStripMenuItem1
            // 
            this.списокКатегорийToolStripMenuItem1.Name = "списокКатегорийToolStripMenuItem1";
            this.списокКатегорийToolStripMenuItem1.Size = new System.Drawing.Size(220, 26);
            this.списокКатегорийToolStripMenuItem1.Text = "Список категорий";
            this.списокКатегорийToolStripMenuItem1.Click += new System.EventHandler(this.списокКатегорийToolStripMenuItem1_Click);
            // 
            // изменитьСпискиToolStripMenuItem
            // 
            this.изменитьСпискиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокБиблиотекToolStripMenuItem,
            this.списокКатегорийToolStripMenuItem});
            this.изменитьСпискиToolStripMenuItem.Name = "изменитьСпискиToolStripMenuItem";
            this.изменитьСпискиToolStripMenuItem.Size = new System.Drawing.Size(144, 24);
            this.изменитьСпискиToolStripMenuItem.Text = "Изменить списки";
            // 
            // списокБиблиотекToolStripMenuItem
            // 
            this.списокБиблиотекToolStripMenuItem.Name = "списокБиблиотекToolStripMenuItem";
            this.списокБиблиотекToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.списокБиблиотекToolStripMenuItem.Text = "Список библиотек";
            this.списокБиблиотекToolStripMenuItem.Click += new System.EventHandler(this.списокБиблиотекToolStripMenuItem_Click);
            // 
            // списокКатегорийToolStripMenuItem
            // 
            this.списокКатегорийToolStripMenuItem.Name = "списокКатегорийToolStripMenuItem";
            this.списокКатегорийToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.списокКатегорийToolStripMenuItem.Text = "Список категорий";
            this.списокКатегорийToolStripMenuItem.Click += new System.EventHandler(this.списокКатегорийToolStripMenuItem_Click);
            // 
            // сменитьВерсиюToolStripMenuItem
            // 
            this.сменитьВерсиюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.основнаяToolStripMenuItem,
            this.тестоваяToolStripMenuItem});
            this.сменитьВерсиюToolStripMenuItem.Name = "сменитьВерсиюToolStripMenuItem";
            this.сменитьВерсиюToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.сменитьВерсиюToolStripMenuItem.Text = "Сменить версию";
            // 
            // основнаяToolStripMenuItem
            // 
            this.основнаяToolStripMenuItem.Name = "основнаяToolStripMenuItem";
            this.основнаяToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.основнаяToolStripMenuItem.Text = "Основная";
            this.основнаяToolStripMenuItem.Click += new System.EventHandler(this.основнаяToolStripMenuItem_Click);
            // 
            // тестоваяToolStripMenuItem
            // 
            this.тестоваяToolStripMenuItem.Name = "тестоваяToolStripMenuItem";
            this.тестоваяToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.тестоваяToolStripMenuItem.Text = "Тестовая";
            this.тестоваяToolStripMenuItem.Click += new System.EventHandler(this.тестоваяToolStripMenuItem_Click);
            // 
            // начальноеСостояниеToolStripMenuItem
            // 
            this.начальноеСостояниеToolStripMenuItem.Name = "начальноеСостояниеToolStripMenuItem";
            this.начальноеСостояниеToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.начальноеСостояниеToolStripMenuItem.Text = "Начальное состояние";
            this.начальноеСостояниеToolStripMenuItem.Click += new System.EventHandler(this.начальноеСостояниеToolStripMenuItem_Click);
            // 
            // настройкаПриложенияToolStripMenuItem
            // 
            this.настройкаПриложенияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаПодключенияToolStripMenuItem,
            this.настройкаПоискаToolStripMenuItem,
            this.настройкаВыводаToolStripMenuItem});
            this.настройкаПриложенияToolStripMenuItem.Name = "настройкаПриложенияToolStripMenuItem";
            this.настройкаПриложенияToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.настройкаПриложенияToolStripMenuItem.Text = "Настройка приложения";
            this.настройкаПриложенияToolStripMenuItem.Click += new System.EventHandler(this.настройкаПриложенияToolStripMenuItem_Click);
            // 
            // настройкаПодключенияToolStripMenuItem
            // 
            this.настройкаПодключенияToolStripMenuItem.Name = "настройкаПодключенияToolStripMenuItem";
            this.настройкаПодключенияToolStripMenuItem.Size = new System.Drawing.Size(265, 26);
            this.настройкаПодключенияToolStripMenuItem.Text = "Настройка подключения";
            this.настройкаПодключенияToolStripMenuItem.Click += new System.EventHandler(this.настройкаПодключенияToolStripMenuItem_Click);
            // 
            // настройкаПоискаToolStripMenuItem
            // 
            this.настройкаПоискаToolStripMenuItem.Name = "настройкаПоискаToolStripMenuItem";
            this.настройкаПоискаToolStripMenuItem.Size = new System.Drawing.Size(265, 26);
            this.настройкаПоискаToolStripMenuItem.Text = "Настройка поиска";
            this.настройкаПоискаToolStripMenuItem.Click += new System.EventHandler(this.настройкаПоискаToolStripMenuItem_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 356);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(544, 43);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 402);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Метка по версии";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 539);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Описание формы";
            // 
            // настройкаВыводаToolStripMenuItem
            // 
            this.настройкаВыводаToolStripMenuItem.Name = "настройкаВыводаToolStripMenuItem";
            this.настройкаВыводаToolStripMenuItem.Size = new System.Drawing.Size(265, 26);
            this.настройкаВыводаToolStripMenuItem.Text = "Настройка вывода";
            this.настройкаВыводаToolStripMenuItem.Click += new System.EventHandler(this.настройкаВыводаToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxStatForms);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxLibrary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox textBoxDate;
        public System.Windows.Forms.ComboBox comboBoxLibrary;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.TextBox textBoxAnswer;
        public System.Windows.Forms.ComboBox comboBoxStatForms;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem изменитьСпискиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокБиблиотекToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокКатегорийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьСпискиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокБиблиотекToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem списокКатегорийToolStripMenuItem1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem сменитьВерсиюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem основнаяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тестоваяToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem начальноеСостояниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаПриложенияToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem настройкаПодключенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаПоискаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаВыводаToolStripMenuItem;
    }
}

