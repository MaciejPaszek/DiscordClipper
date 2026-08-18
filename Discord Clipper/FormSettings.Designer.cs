namespace DiscordClipper
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox3 = new GroupBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            label4 = new Label();
            comboBoxOutputFileFormat = new ComboBox();
            buttonOutputFolder = new Button();
            textBoxOutputFolder = new TextBox();
            label5 = new Label();
            comboBoxEncoder = new ComboBox();
            comboBoxFrameRate = new ComboBox();
            comboBoxResolution = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label12 = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            label13 = new Label();
            numericUpDownMaxVideoBitrate = new NumericUpDown();
            groupBox4 = new GroupBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            buttonShowDiscordWebhook = new Button();
            comboBoxDiscordMode = new ComboBox();
            label9 = new Label();
            textBoxDiscordWebhook = new TextBox();
            label10 = new Label();
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            comboBoxColorMode = new ComboBox();
            groupBox2 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label2 = new Label();
            comboBoxInputFileFormat = new ComboBox();
            label3 = new Label();
            buttonInputFolder = new Button();
            textBoxInputFolder = new TextBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            buttonAccept = new Button();
            buttonCancel = new Button();
            tableLayoutPanel1.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMaxVideoBitrate).BeginInit();
            groupBox4.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(groupBox3, 0, 2);
            tableLayoutPanel1.Controls.Add(groupBox4, 0, 3);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(484, 561);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.AutoSize = true;
            groupBox3.Controls.Add(tableLayoutPanel5);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(10, 175);
            groupBox3.Margin = new Padding(10, 5, 10, 10);
            groupBox3.MinimumSize = new Size(350, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(464, 208);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Ustawienia Wyjścia";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel5.Controls.Add(label4, 0, 1);
            tableLayoutPanel5.Controls.Add(comboBoxOutputFileFormat, 1, 1);
            tableLayoutPanel5.Controls.Add(buttonOutputFolder, 2, 0);
            tableLayoutPanel5.Controls.Add(textBoxOutputFolder, 1, 0);
            tableLayoutPanel5.Controls.Add(label5, 0, 0);
            tableLayoutPanel5.Controls.Add(comboBoxEncoder, 1, 4);
            tableLayoutPanel5.Controls.Add(comboBoxFrameRate, 1, 3);
            tableLayoutPanel5.Controls.Add(comboBoxResolution, 1, 2);
            tableLayoutPanel5.Controls.Add(label6, 0, 4);
            tableLayoutPanel5.Controls.Add(label7, 0, 3);
            tableLayoutPanel5.Controls.Add(label8, 0, 2);
            tableLayoutPanel5.Controls.Add(label12, 0, 5);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel8, 1, 5);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 19);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 6;
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.Size = new Size(458, 186);
            tableLayoutPanel5.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Location = new Point(3, 36);
            label4.Margin = new Padding(3, 5, 3, 3);
            label4.MinimumSize = new Size(0, 23);
            label4.Name = "label4";
            label4.Size = new Size(154, 23);
            label4.TabIndex = 9;
            label4.Text = "Format pliku wyjściowego";
            // 
            // comboBoxOutputFileFormat
            // 
            comboBoxOutputFileFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOutputFileFormat.FormattingEnabled = true;
            comboBoxOutputFileFormat.Location = new Point(163, 34);
            comboBoxOutputFileFormat.Name = "comboBoxOutputFileFormat";
            comboBoxOutputFileFormat.Size = new Size(60, 23);
            comboBoxOutputFileFormat.TabIndex = 8;
            // 
            // buttonOutputFolder
            // 
            buttonOutputFolder.Dock = DockStyle.Top;
            buttonOutputFolder.Location = new Point(411, 3);
            buttonOutputFolder.Name = "buttonOutputFolder";
            buttonOutputFolder.Size = new Size(44, 23);
            buttonOutputFolder.TabIndex = 6;
            buttonOutputFolder.Text = "...";
            buttonOutputFolder.UseVisualStyleBackColor = true;
            buttonOutputFolder.Click += ButtonFolder_Click;
            // 
            // textBoxOutputFolder
            // 
            textBoxOutputFolder.Dock = DockStyle.Top;
            textBoxOutputFolder.Location = new Point(163, 3);
            textBoxOutputFolder.Name = "textBoxOutputFolder";
            textBoxOutputFolder.Size = new Size(242, 23);
            textBoxOutputFolder.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Top;
            label5.Location = new Point(3, 5);
            label5.Margin = new Padding(3, 5, 3, 3);
            label5.MinimumSize = new Size(0, 23);
            label5.Name = "label5";
            label5.Size = new Size(154, 23);
            label5.TabIndex = 1;
            label5.Text = "Folder wyjściowy";
            // 
            // comboBoxEncoder
            // 
            comboBoxEncoder.Dock = DockStyle.Top;
            comboBoxEncoder.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEncoder.FormattingEnabled = true;
            comboBoxEncoder.Location = new Point(163, 127);
            comboBoxEncoder.Name = "comboBoxEncoder";
            comboBoxEncoder.Size = new Size(242, 23);
            comboBoxEncoder.TabIndex = 7;
            // 
            // comboBoxFrameRate
            // 
            comboBoxFrameRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFrameRate.FormattingEnabled = true;
            comboBoxFrameRate.Location = new Point(163, 96);
            comboBoxFrameRate.Name = "comboBoxFrameRate";
            comboBoxFrameRate.Size = new Size(60, 23);
            comboBoxFrameRate.TabIndex = 4;
            // 
            // comboBoxResolution
            // 
            comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxResolution.FormattingEnabled = true;
            comboBoxResolution.Location = new Point(163, 65);
            comboBoxResolution.Name = "comboBoxResolution";
            comboBoxResolution.Size = new Size(60, 23);
            comboBoxResolution.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 129);
            label6.Margin = new Padding(3, 5, 3, 3);
            label6.MinimumSize = new Size(0, 23);
            label6.Name = "label6";
            label6.Size = new Size(89, 23);
            label6.TabIndex = 6;
            label6.Text = "Enkoder obrazu";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 98);
            label7.Margin = new Padding(3, 5, 3, 3);
            label7.MinimumSize = new Size(0, 23);
            label7.Name = "label7";
            label7.Size = new Size(137, 23);
            label7.TabIndex = 3;
            label7.Text = "Liczba klatek na sekundę";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 67);
            label8.Margin = new Padding(3, 5, 3, 3);
            label8.MinimumSize = new Size(0, 23);
            label8.Name = "label8";
            label8.Size = new Size(79, 23);
            label8.TabIndex = 0;
            label8.Text = "Rozdzielczość";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(3, 160);
            label12.Margin = new Padding(3, 5, 3, 3);
            label12.MinimumSize = new Size(0, 23);
            label12.Name = "label12";
            label12.Size = new Size(110, 23);
            label12.TabIndex = 10;
            label12.Text = "Limit bitrate obrazu";
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.AutoSize = true;
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Controls.Add(label13, 1, 0);
            tableLayoutPanel8.Controls.Add(numericUpDownMaxVideoBitrate, 0, 0);
            tableLayoutPanel8.Dock = DockStyle.Top;
            tableLayoutPanel8.Location = new Point(160, 155);
            tableLayoutPanel8.Margin = new Padding(0);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Size = new Size(248, 31);
            tableLayoutPanel8.TabIndex = 12;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(69, 5);
            label13.Margin = new Padding(3, 5, 3, 3);
            label13.MinimumSize = new Size(0, 23);
            label13.Name = "label13";
            label13.Size = new Size(37, 23);
            label13.TabIndex = 12;
            label13.Text = "kbit/s";
            // 
            // numericUpDownMaxVideoBitrate
            // 
            numericUpDownMaxVideoBitrate.Location = new Point(3, 3);
            numericUpDownMaxVideoBitrate.Maximum = new decimal(new int[] { 12000, 0, 0, 0 });
            numericUpDownMaxVideoBitrate.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownMaxVideoBitrate.Name = "numericUpDownMaxVideoBitrate";
            numericUpDownMaxVideoBitrate.Size = new Size(60, 23);
            numericUpDownMaxVideoBitrate.TabIndex = 11;
            numericUpDownMaxVideoBitrate.TextAlign = HorizontalAlignment.Right;
            numericUpDownMaxVideoBitrate.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // groupBox4
            // 
            groupBox4.AutoSize = true;
            groupBox4.Controls.Add(tableLayoutPanel7);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(10, 398);
            groupBox4.Margin = new Padding(10, 5, 10, 10);
            groupBox4.MinimumSize = new Size(350, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(464, 84);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Ustawienia Discorda";
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.AutoSize = true;
            tableLayoutPanel7.ColumnCount = 3;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanel7.Controls.Add(buttonShowDiscordWebhook, 2, 0);
            tableLayoutPanel7.Controls.Add(comboBoxDiscordMode, 1, 1);
            tableLayoutPanel7.Controls.Add(label9, 0, 0);
            tableLayoutPanel7.Controls.Add(textBoxDiscordWebhook, 1, 0);
            tableLayoutPanel7.Controls.Add(label10, 0, 1);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(3, 19);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle());
            tableLayoutPanel7.RowStyles.Add(new RowStyle());
            tableLayoutPanel7.Size = new Size(458, 62);
            tableLayoutPanel7.TabIndex = 8;
            // 
            // buttonShowDiscordWebhook
            // 
            buttonShowDiscordWebhook.Dock = DockStyle.Top;
            buttonShowDiscordWebhook.Location = new Point(391, 3);
            buttonShowDiscordWebhook.Name = "buttonShowDiscordWebhook";
            buttonShowDiscordWebhook.Size = new Size(64, 23);
            buttonShowDiscordWebhook.TabIndex = 10;
            buttonShowDiscordWebhook.Text = "Pokaż";
            buttonShowDiscordWebhook.UseVisualStyleBackColor = true;
            buttonShowDiscordWebhook.Click += ButtonShowDiscordWebhook_Click;
            // 
            // comboBoxDiscordMode
            // 
            comboBoxDiscordMode.Dock = DockStyle.Top;
            comboBoxDiscordMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDiscordMode.FormattingEnabled = true;
            comboBoxDiscordMode.Items.AddRange(new object[] { "Nie wysyłaj klipów", "Wysyłaj wszystkie klipy" });
            comboBoxDiscordMode.Location = new Point(163, 34);
            comboBoxDiscordMode.Name = "comboBoxDiscordMode";
            comboBoxDiscordMode.Size = new Size(222, 23);
            comboBoxDiscordMode.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Top;
            label9.Location = new Point(3, 5);
            label9.Margin = new Padding(3, 5, 3, 3);
            label9.MinimumSize = new Size(0, 23);
            label9.Name = "label9";
            label9.Size = new Size(154, 23);
            label9.TabIndex = 0;
            label9.Text = "Adres URL webhooka";
            // 
            // textBoxDiscordWebhook
            // 
            textBoxDiscordWebhook.Dock = DockStyle.Top;
            textBoxDiscordWebhook.Location = new Point(163, 3);
            textBoxDiscordWebhook.Name = "textBoxDiscordWebhook";
            textBoxDiscordWebhook.Size = new Size(222, 23);
            textBoxDiscordWebhook.TabIndex = 3;
            textBoxDiscordWebhook.UseSystemPasswordChar = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Top;
            label10.Location = new Point(3, 36);
            label10.Margin = new Padding(3, 5, 3, 3);
            label10.MinimumSize = new Size(0, 23);
            label10.Name = "label10";
            label10.Size = new Size(154, 23);
            label10.TabIndex = 1;
            label10.Text = "Tryb wysyłania klipów";
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(10, 10);
            groupBox1.Margin = new Padding(10);
            groupBox1.MinimumSize = new Size(350, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(464, 51);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ustawienia Aplikacji";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(comboBoxColorMode, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(458, 29);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(3, 5);
            label1.Margin = new Padding(3, 5, 3, 0);
            label1.MinimumSize = new Size(0, 23);
            label1.Name = "label1";
            label1.Size = new Size(154, 23);
            label1.TabIndex = 9;
            label1.Text = "Tryb kolorów";
            // 
            // comboBoxColorMode
            // 
            comboBoxColorMode.AutoCompleteCustomSource.AddRange(new string[] { "Jasny", "Ciemny" });
            comboBoxColorMode.FormattingEnabled = true;
            comboBoxColorMode.Items.AddRange(new object[] { "Jasny", "Ciemny", "Systemowy" });
            comboBoxColorMode.Location = new Point(163, 3);
            comboBoxColorMode.Name = "comboBoxColorMode";
            comboBoxColorMode.Size = new Size(121, 23);
            comboBoxColorMode.TabIndex = 8;
            comboBoxColorMode.SelectedIndexChanged += ComboBoxColorMode_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(tableLayoutPanel3);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(10, 76);
            groupBox2.Margin = new Padding(10, 5, 10, 10);
            groupBox2.MinimumSize = new Size(350, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(464, 84);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ustawienia Wejścia";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.Controls.Add(label2, 0, 1);
            tableLayoutPanel3.Controls.Add(comboBoxInputFileFormat, 1, 1);
            tableLayoutPanel3.Controls.Add(label3, 0, 0);
            tableLayoutPanel3.Controls.Add(buttonInputFolder, 2, 0);
            tableLayoutPanel3.Controls.Add(textBoxInputFolder, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(458, 62);
            tableLayoutPanel3.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(3, 36);
            label2.Margin = new Padding(3, 5, 3, 3);
            label2.MinimumSize = new Size(0, 23);
            label2.Name = "label2";
            label2.Size = new Size(154, 23);
            label2.TabIndex = 8;
            label2.Text = "Format pliku wyjściowego";
            // 
            // comboBoxInputFileFormat
            // 
            comboBoxInputFileFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxInputFileFormat.FormattingEnabled = true;
            comboBoxInputFileFormat.Location = new Point(163, 34);
            comboBoxInputFileFormat.Name = "comboBoxInputFileFormat";
            comboBoxInputFileFormat.Size = new Size(60, 23);
            comboBoxInputFileFormat.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Location = new Point(3, 5);
            label3.Margin = new Padding(3, 5, 3, 3);
            label3.MinimumSize = new Size(0, 23);
            label3.Name = "label3";
            label3.Size = new Size(154, 23);
            label3.TabIndex = 0;
            label3.Text = "Folder wejściowy";
            // 
            // buttonInputFolder
            // 
            buttonInputFolder.Dock = DockStyle.Top;
            buttonInputFolder.Location = new Point(411, 3);
            buttonInputFolder.Name = "buttonInputFolder";
            buttonInputFolder.Size = new Size(44, 23);
            buttonInputFolder.TabIndex = 5;
            buttonInputFolder.Text = "...";
            buttonInputFolder.UseVisualStyleBackColor = true;
            buttonInputFolder.Click += ButtonFolder_Click;
            // 
            // textBoxInputFolder
            // 
            textBoxInputFolder.Dock = DockStyle.Top;
            textBoxInputFolder.Location = new Point(163, 3);
            textBoxInputFolder.Name = "textBoxInputFolder";
            textBoxInputFolder.Size = new Size(242, 23);
            textBoxInputFolder.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(buttonAccept, 1, 0);
            tableLayoutPanel4.Controls.Add(buttonCancel, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 492);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(484, 69);
            tableLayoutPanel4.TabIndex = 6;
            // 
            // buttonAccept
            // 
            buttonAccept.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonAccept.Location = new Point(406, 43);
            buttonAccept.Name = "buttonAccept";
            buttonAccept.Size = new Size(75, 23);
            buttonAccept.TabIndex = 8;
            buttonAccept.Text = "Zastosuj";
            buttonAccept.UseVisualStyleBackColor = true;
            buttonAccept.Click += ButtonAccept_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(325, 43);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Anuluj";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // FormSettings
            // 
            AcceptButton = buttonAccept;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(484, 561);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(500, 600);
            Name = "FormSettings";
            Text = "Ustawienia";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMaxVideoBitrate).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label4;
        private Button buttonOutputFolder;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private GroupBox groupBox4;
        private TableLayoutPanel tableLayoutPanel7;
        private Button buttonShowDiscordWebhook;
        private ComboBox comboBoxDiscordMode;
        private Label label9;
        private TextBox textBoxDiscordWebhook;
        private Label label10;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label2;
        private Label label3;
        private Button buttonInputFolder;
        private TableLayoutPanel tableLayoutPanel4;
        private Button buttonAccept;
        private Button buttonCancel;
        private ComboBox comboBoxResolution;
        private ComboBox comboBoxOutputFileFormat;
        private TextBox textBoxOutputFolder;
        private ComboBox comboBoxEncoder;
        private ComboBox comboBoxFrameRate;
        private ComboBox comboBoxColorMode;
        private ComboBox comboBoxInputFileFormat;
        private TextBox textBoxInputFolder;
        private Label label12;
        private NumericUpDown numericUpDownMaxVideoBitrate;
        private TableLayoutPanel tableLayoutPanel8;
        private Label label13;
    }
}