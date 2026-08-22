namespace DiscordClipper
{
    partial class FormConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsole));
            tableLayoutPanel6 = new TableLayoutPanel();
            richTextBoxConsole = new RichTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label12 = new Label();
            numericUpDownConsoleFontSize = new NumericUpDown();
            buttonClearConsole = new Button();
            checkBoxAutoscroll = new CheckBox();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownConsoleFontSize).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(richTextBoxConsole, 0, 0);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel1, 0, 1);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.Size = new Size(1264, 681);
            tableLayoutPanel6.TabIndex = 1;
            // 
            // richTextBoxConsole
            // 
            richTextBoxConsole.BackColor = SystemColors.Control;
            richTextBoxConsole.BorderStyle = BorderStyle.None;
            richTextBoxConsole.Dock = DockStyle.Fill;
            richTextBoxConsole.Font = new Font("Consolas", 9F);
            richTextBoxConsole.Location = new Point(3, 3);
            richTextBoxConsole.Name = "richTextBoxConsole";
            richTextBoxConsole.ReadOnly = true;
            richTextBoxConsole.Size = new Size(1258, 644);
            richTextBoxConsole.TabIndex = 1;
            richTextBoxConsole.Text = "";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label12, 0, 0);
            tableLayoutPanel1.Controls.Add(numericUpDownConsoleFontSize, 1, 0);
            tableLayoutPanel1.Controls.Add(buttonClearConsole, 3, 0);
            tableLayoutPanel1.Controls.Add(checkBoxAutoscroll, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 650);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1264, 31);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(3, 5);
            label12.Margin = new Padding(3, 5, 3, 3);
            label12.MinimumSize = new Size(0, 23);
            label12.Name = "label12";
            label12.Size = new Size(137, 23);
            label12.TabIndex = 18;
            label12.Text = "Rozmiar czcionki konsoli";
            // 
            // numericUpDownConsoleFontSize
            // 
            numericUpDownConsoleFontSize.Location = new Point(146, 3);
            numericUpDownConsoleFontSize.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            numericUpDownConsoleFontSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownConsoleFontSize.Name = "numericUpDownConsoleFontSize";
            numericUpDownConsoleFontSize.Size = new Size(60, 23);
            numericUpDownConsoleFontSize.TabIndex = 17;
            numericUpDownConsoleFontSize.TextAlign = HorizontalAlignment.Right;
            numericUpDownConsoleFontSize.Value = new decimal(new int[] { 9, 0, 0, 0 });
            numericUpDownConsoleFontSize.ValueChanged += numericUpDownConsoleFontSize_ValueChanged;
            // 
            // buttonClearConsole
            // 
            buttonClearConsole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClearConsole.Location = new Point(1161, 3);
            buttonClearConsole.Name = "buttonClearConsole";
            buttonClearConsole.Size = new Size(100, 23);
            buttonClearConsole.TabIndex = 16;
            buttonClearConsole.Text = "Wyczyść";
            buttonClearConsole.UseVisualStyleBackColor = true;
            buttonClearConsole.Click += buttonClearConsole_Click;
            // 
            // checkBoxAutoscroll
            // 
            checkBoxAutoscroll.AutoSize = true;
            checkBoxAutoscroll.Checked = true;
            checkBoxAutoscroll.CheckState = CheckState.Checked;
            checkBoxAutoscroll.Location = new Point(219, 5);
            checkBoxAutoscroll.Margin = new Padding(10, 5, 3, 3);
            checkBoxAutoscroll.Name = "checkBoxAutoscroll";
            checkBoxAutoscroll.Size = new Size(80, 19);
            checkBoxAutoscroll.TabIndex = 19;
            checkBoxAutoscroll.Text = "Autoscroll";
            checkBoxAutoscroll.UseVisualStyleBackColor = true;
            // 
            // FormConsole
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(tableLayoutPanel6);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(400, 200);
            Name = "FormConsole";
            Text = "Konsola";
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownConsoleFontSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel6;
        private RichTextBox richTextBoxConsole;
        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonClearConsole;
        private NumericUpDown numericUpDownConsoleFontSize;
        private Label label12;
        private CheckBox checkBoxAutoscroll;
    }
}