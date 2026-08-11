namespace DiscordClipper
{
    public partial class FormSettings : Form
    {
        // Pokaż okno dialogowe jeden raz
        private bool ShowColorModeMessageBox = true;

        public Settings? Settings { get; private set; }

        public FormSettings(Settings settings)
        {
            InitializeComponent();
            
            // PRzyjmij ustawienia razem z nazwą profilu
            Settings = settings;
            
            // Opcje list rozwijanych

            foreach (Option option in FFmpeg.InputFileFormats)
            {
                comboBoxInputFileFormat.Items.Add(option.Name);
            }

            foreach (Option option in FFmpeg.OutputFileFormats)
            {
                comboBoxOutputFileFormat.Items.Add(option.Name);
            }

            foreach (Option option in FFmpeg.Resolutions)
            {
                comboBoxResolution.Items.Add(option.Name);
            }

            foreach (Option option in FFmpeg.FrameRates)
            {
                comboBoxFrameRate.Items.Add(option.Name);
            }

            foreach (Option option in FFmpeg.Encoders)
            {
                comboBoxEncoder.Items.Add(option.Name);
            }

            // Ładowanie wartości do kontrolek

            comboBoxColorMode.SelectedIndex = settings.ColorMode;

            textBoxInputFolder.Text = settings.InputFolder;
            comboBoxInputFileFormat.SelectedIndex = settings.InputFileFormat;

            textBoxOutputFolder.Text = settings.OutputFolder;
            comboBoxOutputFileFormat.SelectedIndex = settings.OutputFileFormat;
            comboBoxResolution.SelectedIndex = settings.Resolution;
            comboBoxFrameRate.SelectedIndex = settings.FrameRate;
            comboBoxEncoder.SelectedIndex = settings.Encoder;
            numericUpDownMaxVideoBitrate.Value = settings.MaxVideoBitrate;

            textBoxDiscordWebhook.Text = settings.DiscordWebhook;
            comboBoxDiscordMode.SelectedIndex = settings.DiscordMode;
            textBoxDiscordShortcut.Text = settings.DiscordShortcut;
        }

        private void ComboBoxColorMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxColorMode.IsHandleCreated && ShowColorModeMessageBox)
            {
                MessageBox.Show("Uruchom ponownie, aby zastosować zmiany.", "Tryb kolorów", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowColorModeMessageBox = false;
            }
        }

        private void ButtonFolder_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Equals(buttonInputFolder))
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Wybierz folder wejściowy";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxInputFolder.Text = folderBrowserDialog.SelectedPath;
                }
            }

            if (button.Equals(buttonOutputFolder))
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Wybierz folder wyjściowy";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxOutputFolder.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void ButtonShowDiscordWebhook_Click(object sender, EventArgs e)
        {
            if (textBoxDiscordWebhook.UseSystemPasswordChar == true)
            {
                textBoxDiscordWebhook.UseSystemPasswordChar = false;
                buttonShowDiscordWebhook.Text = "Ukryj";
            }
            else
            {
                textBoxDiscordWebhook.UseSystemPasswordChar = true;
                buttonShowDiscordWebhook.Text = "Pokaż";
            }
        }
        private void ButtonClearShortcut_Click(object sender, EventArgs e)
        {
            checkBoxShortcutControl.Checked = false;
            checkBoxShortcutShift.Checked = false;
            checkBoxShortcutAlt.Checked = false;

            textBoxDiscordShortcut.Text = string.Empty;
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            if(Settings == null)
            {
                return;
            }

            Settings.ColorMode = comboBoxColorMode.SelectedIndex;

            Settings.InputFolder = textBoxInputFolder.Text;
            Settings.InputFileFormat = comboBoxInputFileFormat.SelectedIndex;

            Settings.OutputFolder = textBoxOutputFolder.Text;
            Settings.OutputFileFormat = comboBoxOutputFileFormat.SelectedIndex;
            Settings.Resolution = comboBoxResolution.SelectedIndex;
            Settings.FrameRate = comboBoxFrameRate.SelectedIndex;
            Settings.Encoder = comboBoxEncoder.SelectedIndex;
            Settings.MaxVideoBitrate = (int) numericUpDownMaxVideoBitrate.Value;

            Settings.DiscordWebhook = textBoxDiscordWebhook.Text;
            Settings.DiscordMode = comboBoxDiscordMode.SelectedIndex;
            Settings.DiscordShortcut = textBoxDiscordShortcut.Text;

            DialogResult = DialogResult.OK;

            Close();
        }
    }
}