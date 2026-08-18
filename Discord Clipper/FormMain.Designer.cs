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
            buttonConsole = new Button();
            buttonSettings = new Button();
            buttonActivate = new Button();
            splitContainerOutput = new SplitContainer();
            groupBoxConsole = new GroupBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            buttonAddClips = new Button();
            dataGridViewClips = new DataGridView();
            Thumbnail = new DataGridViewImageColumn();
            ClipName = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            ColumnSend = new DataGridViewButtonColumn();
            menuStrip1 = new MenuStrip();
            oknaToolStripMenuItem = new ToolStripMenuItem();
            ustawieniaToolStripMenuItem = new ToolStripMenuItem();
            otwórzFolderAppDataToolStripMenuItem = new ToolStripMenuItem();
            pomocToolStripMenuItem = new ToolStripMenuItem();
            instrukcjaToolStripMenuItem = new ToolStripMenuItem();
            zgłośBłądToolStripMenuItem = new ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)dataGridViewClips).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxOutput
            // 
            groupBoxOutput.Controls.Add(tableLayoutPanel1);
            groupBoxOutput.Dock = DockStyle.Fill;
            groupBoxOutput.Location = new Point(0, 0);
            groupBoxOutput.Margin = new Padding(10, 5, 10, 10);
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.Size = new Size(604, 407);
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
            tableLayoutPanel1.Size = new Size(598, 385);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // pictureBoxThumbnail
            // 
            pictureBoxThumbnail.BackgroundImage = (Image)resources.GetObject("pictureBoxThumbnail.BackgroundImage");
            pictureBoxThumbnail.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxThumbnail.Dock = DockStyle.Fill;
            pictureBoxThumbnail.Location = new Point(3, 3);
            pictureBoxThumbnail.Name = "pictureBoxThumbnail";
            pictureBoxThumbnail.Size = new Size(592, 335);
            pictureBoxThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxThumbnail.TabIndex = 0;
            pictureBoxThumbnail.TabStop = false;
            // 
            // progressBarOutput
            // 
            progressBarOutput.Dock = DockStyle.Top;
            progressBarOutput.Location = new Point(3, 359);
            progressBarOutput.Name = "progressBarOutput";
            progressBarOutput.Size = new Size(592, 23);
            progressBarOutput.TabIndex = 1;
            // 
            // labelFileName
            // 
            labelFileName.AutoSize = true;
            labelFileName.Dock = DockStyle.Top;
            labelFileName.Location = new Point(3, 341);
            labelFileName.Name = "labelFileName";
            labelFileName.Size = new Size(592, 15);
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
            tableLayoutPanelMain.Location = new Point(0, 24);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(624, 1017);
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
            groupBox2.Size = new Size(604, 109);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Aplikacja";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(buttonConsole, 0, 2);
            tableLayoutPanel2.Controls.Add(buttonSettings, 0, 0);
            tableLayoutPanel2.Controls.Add(buttonActivate, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(598, 87);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonConsole
            // 
            buttonConsole.Dock = DockStyle.Top;
            buttonConsole.Location = new Point(3, 61);
            buttonConsole.Name = "buttonConsole";
            buttonConsole.Size = new Size(592, 23);
            buttonConsole.TabIndex = 8;
            buttonConsole.Text = "Konsola";
            buttonConsole.UseVisualStyleBackColor = true;
            buttonConsole.Click += buttonConsole_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Dock = DockStyle.Top;
            buttonSettings.Location = new Point(3, 3);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(592, 23);
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
            buttonActivate.Size = new Size(592, 23);
            buttonActivate.TabIndex = 7;
            buttonActivate.Text = "Rozpocznij monitorowanie";
            buttonActivate.UseVisualStyleBackColor = true;
            buttonActivate.Click += ButtonActivate_Click;
            // 
            // splitContainerOutput
            // 
            splitContainerOutput.Dock = DockStyle.Fill;
            splitContainerOutput.FixedPanel = FixedPanel.Panel2;
            splitContainerOutput.Location = new Point(10, 134);
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
            splitContainerOutput.Size = new Size(604, 873);
            splitContainerOutput.SplitterDistance = 462;
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
            groupBoxConsole.Size = new Size(604, 462);
            groupBoxConsole.TabIndex = 3;
            groupBoxConsole.TabStop = false;
            groupBoxConsole.Text = "Kolejka";
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(buttonAddClips, 0, 1);
            tableLayoutPanel6.Controls.Add(dataGridViewClips, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(7, 23);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(590, 432);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // buttonAddClips
            // 
            buttonAddClips.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddClips.Location = new Point(487, 406);
            buttonAddClips.Name = "buttonAddClips";
            buttonAddClips.Size = new Size(100, 23);
            buttonAddClips.TabIndex = 15;
            buttonAddClips.Text = "Dodaj klipy...";
            buttonAddClips.UseVisualStyleBackColor = true;
            buttonAddClips.Click += ButtonAddClips_Click;
            // 
            // dataGridViewClips
            // 
            dataGridViewClips.AllowUserToAddRows = false;
            dataGridViewClips.AllowUserToDeleteRows = false;
            dataGridViewClips.Columns.AddRange(new DataGridViewColumn[] { Thumbnail, ClipName, Status, ColumnSend });
            dataGridViewClips.Dock = DockStyle.Fill;
            dataGridViewClips.Location = new Point(3, 3);
            dataGridViewClips.Name = "dataGridViewClips";
            dataGridViewClips.ReadOnly = true;
            dataGridViewClips.RowHeadersVisible = false;
            dataGridViewClips.RowTemplate.Height = 90;
            dataGridViewClips.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClips.Size = new Size(584, 397);
            dataGridViewClips.TabIndex = 16;
            // 
            // Thumbnail
            // 
            Thumbnail.HeaderText = "Miniatura";
            Thumbnail.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Thumbnail.Name = "Thumbnail";
            Thumbnail.ReadOnly = true;
            Thumbnail.Width = 160;
            // 
            // ClipName
            // 
            ClipName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ClipName.HeaderText = "Nazwa pliku";
            ClipName.Name = "ClipName";
            ClipName.ReadOnly = true;
            ClipName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnSend
            // 
            ColumnSend.HeaderText = "Discord";
            ColumnSend.MinimumWidth = 100;
            ColumnSend.Name = "ColumnSend";
            ColumnSend.ReadOnly = true;
            ColumnSend.Text = "Wyślij";
            ColumnSend.UseColumnTextForButtonValue = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { oknaToolStripMenuItem, pomocToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(624, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // oknaToolStripMenuItem
            // 
            oknaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ustawieniaToolStripMenuItem, otwórzFolderAppDataToolStripMenuItem });
            oknaToolStripMenuItem.Name = "oknaToolStripMenuItem";
            oknaToolStripMenuItem.Size = new Size(100, 20);
            oknaToolStripMenuItem.Text = "Discord Clipper";
            // 
            // ustawieniaToolStripMenuItem
            // 
            ustawieniaToolStripMenuItem.Name = "ustawieniaToolStripMenuItem";
            ustawieniaToolStripMenuItem.Size = new Size(195, 22);
            ustawieniaToolStripMenuItem.Text = "Ustawienia";
            // 
            // otwórzFolderAppDataToolStripMenuItem
            // 
            otwórzFolderAppDataToolStripMenuItem.Name = "otwórzFolderAppDataToolStripMenuItem";
            otwórzFolderAppDataToolStripMenuItem.Size = new Size(195, 22);
            otwórzFolderAppDataToolStripMenuItem.Text = "Otwórz folder AppData";
            otwórzFolderAppDataToolStripMenuItem.Click += otwórzFolderAppDataToolStripMenuItem_Click;
            // 
            // pomocToolStripMenuItem
            // 
            pomocToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { instrukcjaToolStripMenuItem, zgłośBłądToolStripMenuItem });
            pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            pomocToolStripMenuItem.Size = new Size(57, 20);
            pomocToolStripMenuItem.Text = "GitHub";
            // 
            // instrukcjaToolStripMenuItem
            // 
            instrukcjaToolStripMenuItem.Name = "instrukcjaToolStripMenuItem";
            instrukcjaToolStripMenuItem.Size = new Size(210, 22);
            instrukcjaToolStripMenuItem.Text = "Discord Clipper Wiki";
            instrukcjaToolStripMenuItem.Click += instrukcjaToolStripMenuItem_Click;
            // 
            // zgłośBłądToolStripMenuItem
            // 
            zgłośBłądToolStripMenuItem.Name = "zgłośBłądToolStripMenuItem";
            zgłośBłądToolStripMenuItem.Size = new Size(210, 22);
            zgłośBłądToolStripMenuItem.Text = "Zgłoś błąd lub propozycję";
            zgłośBłądToolStripMenuItem.Click += zgłośBłądToolStripMenuItem_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 1041);
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
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
            ((System.ComponentModel.ISupportInitialize)dataGridViewClips).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBoxOutput;
        private PictureBox pictureBoxThumbnail;
        private Label labelFileName;
        private ProgressBar progressBarOutput;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainerOutput;
        private GroupBox groupBoxConsole;
        private TableLayoutPanel tableLayoutPanel6;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private Button buttonSettings;
        private Button buttonActivate;
        private Button buttonAddClips;
        private Button buttonConsole;
        private DataGridView dataGridViewClips;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem oknaToolStripMenuItem;
        private ToolStripMenuItem ustawieniaToolStripMenuItem;
        private ToolStripMenuItem otwórzFolderAppDataToolStripMenuItem;
        private ToolStripMenuItem pomocToolStripMenuItem;
        private ToolStripMenuItem instrukcjaToolStripMenuItem;
        private ToolStripMenuItem zgłośBłądToolStripMenuItem;
        private DataGridViewImageColumn Thumbnail;
        private DataGridViewTextBoxColumn ClipName;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewButtonColumn ColumnSend;
    }
}
