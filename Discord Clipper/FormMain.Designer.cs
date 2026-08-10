namespace DiscordClipper
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            groupBoxOutput = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBoxThumbnail = new PictureBox();
            progressBarOutput = new ProgressBar();
            labelFileName = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            groupBox2 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            buttonSettings = new Button();
            buttonActivate = new Button();
            splitContainerOutput = new SplitContainer();
            groupBoxConsole = new GroupBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            buttonClearConsole = new Button();
            numericUpDownConsoleFontSize = new NumericUpDown();
            labelFontSize = new Label();
            richTextBoxConsole = new RichTextBox();
            groupBoxOutput.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxThumbnail).BeginInit();
            tableLayoutPanelMain.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerOutput).BeginInit();
            splitContainerOutput.Panel1.SuspendLayout();
            splitContainerOutput.Panel2.SuspendLayout();
            splitContainerOutput.SuspendLayout();
            groupBoxConsole.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownConsoleFontSize).BeginInit();
            SuspendLayout();
            // 
            // groupBoxOutput
            // 
            groupBoxOutput.Controls.Add(tableLayoutPanel1);
            groupBoxOutput.Dock = DockStyle.Fill;
            groupBoxOutput.Location = new Point(0, 0);
            groupBoxOutput.Margin = new Padding(10, 5, 10, 10);
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.Size = new Size(364, 272);
            groupBoxOutput.TabIndex = 1;
            groupBoxOutput.TabStop = false;
            groupBoxOutput.Text = "Podgląd";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBoxThumbnail, 0, 0);
            tableLayoutPanel1.Controls.Add(progressBarOutput, 0, 2);
            tableLayoutPanel1.Controls.Add(labelFileName, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(358, 250);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // pictureBoxThumbnail
            // 
            pictureBoxThumbnail.BackgroundImage = (Image)resources.GetObject("pictureBoxThumbnail.BackgroundImage");
            pictureBoxThumbnail.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxThumbnail.Dock = DockStyle.Fill;
            pictureBoxThumbnail.Location = new Point(3, 3);
            pictureBoxThumbnail.Name = "pictureBoxThumbnail";
            pictureBoxThumbnail.Size = new Size(352, 200);
            pictureBoxThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxThumbnail.TabIndex = 0;
            pictureBoxThumbnail.TabStop = false;
            // 
            // progressBarOutput
            // 
            progressBarOutput.Dock = DockStyle.Top;
            progressBarOutput.Location = new Point(3, 224);
            progressBarOutput.Name = "progressBarOutput";
            progressBarOutput.Size = new Size(352, 23);
            progressBarOutput.TabIndex = 1;
            // 
            // labelFileName
            // 
            labelFileName.AutoSize = true;
            labelFileName.Dock = DockStyle.Top;
            labelFileName.Location = new Point(3, 206);
            labelFileName.Name = "labelFileName";
            labelFileName.Size = new Size(352, 15);
            labelFileName.TabIndex = 2;
            labelFileName.Text = "Nazwa pliku";
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(groupBox2, 0, 0);
            tableLayoutPanelMain.Controls.Add(splitContainerOutput, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(384, 561);
            tableLayoutPanelMain.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(tableLayoutPanel2);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(10, 10);
            groupBox2.Margin = new Padding(10);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(364, 80);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Aplikacja";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(buttonSettings, 0, 0);
            tableLayoutPanel2.Controls.Add(buttonActivate, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(358, 58);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonSettings
            // 
            buttonSettings.Dock = DockStyle.Top;
            buttonSettings.Location = new Point(3, 3);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(352, 23);
            buttonSettings.TabIndex = 0;
            buttonSettings.Text = "Ustawienia";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += ButtonSettings_Click;
            // 
            // buttonActivate
            // 
            buttonActivate.Dock = DockStyle.Top;
            buttonActivate.Location = new Point(3, 32);
            buttonActivate.Name = "buttonActivate";
            buttonActivate.Size = new Size(352, 23);
            buttonActivate.TabIndex = 7;
            buttonActivate.Text = "Rozpocznij monitorowanie";
            buttonActivate.UseVisualStyleBackColor = true;
            buttonActivate.Click += ButtonActivate_Click;
            // 
            // splitContainerOutput
            // 
            splitContainerOutput.Dock = DockStyle.Fill;
            splitContainerOutput.FixedPanel = FixedPanel.Panel2;
            splitContainerOutput.Location = new Point(10, 105);
            splitContainerOutput.Margin = new Padding(10, 5, 10, 10);
            splitContainerOutput.Name = "splitContainerOutput";
            splitContainerOutput.Orientation = Orientation.Horizontal;
            // 
            // splitContainerOutput.Panel1
            // 
            splitContainerOutput.Panel1.Controls.Add(groupBoxConsole);
            // 
            // splitContainerOutput.Panel2
            // 
            splitContainerOutput.Panel2.Controls.Add(groupBoxOutput);
            splitContainerOutput.Size = new Size(364, 446);
            splitContainerOutput.SplitterDistance = 170;
            splitContainerOutput.TabIndex = 3;
            // 
            // groupBoxConsole
            // 
            groupBoxConsole.Controls.Add(tableLayoutPanel6);
            groupBoxConsole.Dock = DockStyle.Fill;
            groupBoxConsole.Location = new Point(0, 0);
            groupBoxConsole.Margin = new Padding(10, 5, 10, 10);
            groupBoxConsole.Name = "groupBoxConsole";
            groupBoxConsole.Padding = new Padding(7);
            groupBoxConsole.Size = new Size(364, 170);
            groupBoxConsole.TabIndex = 3;
            groupBoxConsole.TabStop = false;
            groupBoxConsole.Text = "Konsola";
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel8, 0, 1);
            tableLayoutPanel6.Controls.Add(richTextBoxConsole, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(7, 23);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.Size = new Size(350, 140);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.AutoSize = true;
            tableLayoutPanel8.ColumnCount = 3;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.Controls.Add(buttonClearConsole, 2, 0);
            tableLayoutPanel8.Controls.Add(numericUpDownConsoleFontSize, 1, 0);
            tableLayoutPanel8.Controls.Add(labelFontSize, 0, 0);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(0, 111);
            tableLayoutPanel8.Margin = new Padding(0);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle());
            tableLayoutPanel8.Size = new Size(350, 29);
            tableLayoutPanel8.TabIndex = 9;
            // 
            // buttonClearConsole
            // 
            buttonClearConsole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClearConsole.Location = new Point(247, 3);
            buttonClearConsole.Name = "buttonClearConsole";
            buttonClearConsole.Size = new Size(100, 23);
            buttonClearConsole.TabIndex = 14;
            buttonClearConsole.Text = "Wyczyść";
            buttonClearConsole.UseVisualStyleBackColor = true;
            buttonClearConsole.Click += ButtonClearConsole_Click;
            // 
            // numericUpDownConsoleFontSize
            // 
            numericUpDownConsoleFontSize.Location = new Point(153, 3);
            numericUpDownConsoleFontSize.Maximum = new decimal(new int[] { 36, 0, 0, 0 });
            numericUpDownConsoleFontSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownConsoleFontSize.Name = "numericUpDownConsoleFontSize";
            numericUpDownConsoleFontSize.Size = new Size(60, 23);
            numericUpDownConsoleFontSize.TabIndex = 11;
            numericUpDownConsoleFontSize.Value = new decimal(new int[] { 9, 0, 0, 0 });
            numericUpDownConsoleFontSize.ValueChanged += NumericUpDownConsoleFontSize_ValueChanged;
            // 
            // labelFontSize
            // 
            labelFontSize.AutoSize = true;
            labelFontSize.Dock = DockStyle.Top;
            labelFontSize.Location = new Point(3, 5);
            labelFontSize.Margin = new Padding(3, 5, 3, 0);
            labelFontSize.MinimumSize = new Size(0, 23);
            labelFontSize.Name = "labelFontSize";
            labelFontSize.Size = new Size(144, 23);
            labelFontSize.TabIndex = 10;
            labelFontSize.Text = "Rozmiar czcionki konsoli";
            // 
            // richTextBoxConsole
            // 
            richTextBoxConsole.BorderStyle = BorderStyle.None;
            richTextBoxConsole.Dock = DockStyle.Fill;
            richTextBoxConsole.Font = new Font("Consolas", 9F);
            richTextBoxConsole.Location = new Point(10, 10);
            richTextBoxConsole.Margin = new Padding(10);
            richTextBoxConsole.Name = "richTextBoxConsole";
            richTextBoxConsole.ReadOnly = true;
            richTextBoxConsole.Size = new Size(330, 91);
            richTextBoxConsole.TabIndex = 1;
            richTextBoxConsole.Text = "";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 561);
            Controls.Add(tableLayoutPanelMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(400, 600);
            Name = "FormMain";
            Text = "Discord Clipper";
            groupBoxOutput.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxThumbnail).EndInit();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            splitContainerOutput.Panel1.ResumeLayout(false);
            splitContainerOutput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerOutput).EndInit();
            splitContainerOutput.ResumeLayout(false);
            groupBoxConsole.ResumeLayout(false);
            groupBoxConsole.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownConsoleFontSize).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBoxOutput;
        private PictureBox pictureBoxThumbnail;
        private Label labelFileName;
        private ProgressBar progressBarOutput;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelFontSize;
        private NumericUpDown numericUpDownConsoleFontSize;
        private SplitContainer splitContainerOutput;
        private GroupBox groupBoxConsole;
        private TableLayoutPanel tableLayoutPanel6;
        private RichTextBox richTextBoxConsole;
        private TableLayoutPanel tableLayoutPanel8;
        private Button buttonClearConsole;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private Button buttonSettings;
        private Button buttonActivate;
    }
}
