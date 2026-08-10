using System.Text.RegularExpressions;

namespace DiscordClipper
{
    public partial class FormMain : Form
    {
        /*******************************************************
         * Obiekty Globalne
         *******************************************************/

        private readonly string SettingsFilePath = Application.UserAppDataPath + "\\settings.txt";

        // Okno ustawień
        FormSettings? FormSettings;

        // Monitorowanie folderu
        FileSystemWatcher? FileSystemWatcher;

        // Czy monitorowanie plików jest aktywne
        private bool isWatcherActive = false;

        // Klasa FFmpeg
        FFmpeg? FFmpeg;

        // Klasa Discord
        Discord? Discord;

        /*******************************************************
         * FormMain 
         *******************************************************/

        public FormMain()
        {
            InitializeComponent();

            // Utwóz obiekt klasy FFmpeg i dodaj obsługę zdarzeń
            FFmpeg = new FFmpeg();
            FFmpeg.FFmpegError       += FFmpeg_FFmpegError;
            FFmpeg.ConversionStarted += FFmpeg_ConversionStarted;
            FFmpeg.ThumbnailCreated  += FFmpeg_ThumbnailCreated;
            FFmpeg.ProgressChanged   += FFmpeg_ProgressChanged;
            FFmpeg.VideoCreated      += FFmpeg_VideoCreated;

            // Utwórz obiekt klasy Discord
            Discord = new Discord();
            Discord.ClipSent += Discord_ClipSent;
            Discord.DiscordError += Discord_DiscordError;

            // Załaduj ustawienia z pliku settings.txt
            Settings settings = LoadSettings(SettingsFilePath);

            // Utwórz formularz ustawień
            FormSettings = new FormSettings(settings);
        }



        /*******************************************************
         * Ustawienia
         *******************************************************/

        private Settings LoadSettings(string settingsFilePath)
        {
            // Ustawienia domyślne
            Settings settings = new Settings();

            // Jeśli plik ustawień nie istnieje, zwróć ustawienia domyślne
            if (!File.Exists(settingsFilePath))
            {
                return settings;
            }

            StreamReader streamReader = File.OpenText(settingsFilePath);

            if(streamReader == null)
            {
                return settings;
            }

            // Numer linii
            int i = 0;

            // Tekst linii
            string line = "";

            // Czytaj koljene linie, aż do końca pliku
            while ((line = streamReader.ReadLine()) != null)
            {
                try
                {
                    if (i == 0) { settings.ColorMode = Convert.ToInt32(line); }

                    if (i == 1) { settings.InputFolder = line; }
                    if (i == 2) { settings.InputFileFormat = Convert.ToInt32(line); }

                    if (i == 3) { settings.OutputFolder = line; }
                    if (i == 4) { settings.OutputFileFormat = Convert.ToInt32(line); }
                    if (i == 5) { settings.Resolution = Convert.ToInt32(line); }
                    if (i == 6) { settings.FrameRate = Convert.ToInt32(line); }
                    if (i == 7) { settings.Encoder = Convert.ToInt32(line); }

                    if (i == 8) { settings.DiscordWebhook = line; }
                    if (i == 9) { settings.DiscordMode = Convert.ToInt32(line); }
                    if (i == 10) { settings.DiscordShortcut = line; }

                    if (i == 11) { settings.ConsoleFontSize = Convert.ToInt32(line); }
                }
                catch
                {

                }

                i++;
            }

            // Zamykanie pliku
            streamReader.Close();
            streamReader.Dispose();

            switch (settings.ColorMode)
            {
                case 0:
                    Application.SetColorMode(SystemColorMode.Classic);
                    break;

                case 1:
                    Application.SetColorMode(SystemColorMode.Dark);
                    break;

                default:
                    Application.SetColorMode(SystemColorMode.System);
                    break;
            }

            // Ustawianie czcionki
            numericUpDownConsoleFontSize.Value = settings.ConsoleFontSize;
            UpdateConsoleFontSize();

            ApplySettings(settings);

            return settings;
        }

        /*******************************************************
         * Zdarzenia FormMain
         *******************************************************/

        private void ButtonSettings_Click(object? sender, EventArgs e)
        {
            if (FormSettings == null)
            {
                return;
            }

            FormSettings.ShowDialog();

            if (FormSettings.DialogResult == DialogResult.OK)
            {
                FormSettings.Settings.ConsoleFontSize = (int)numericUpDownConsoleFontSize.Value;
                SaveSettings(FormSettings.Settings, SettingsFilePath);
                ApplySettings(FormSettings.Settings);
            }
        }

        private void SaveSettings(Settings settings, string settingsFilePath)
        {
            StreamWriter streamWriter = File.CreateText(settingsFilePath);

            streamWriter.WriteLine(settings.ColorMode.ToString());

            streamWriter.WriteLine(settings.InputFolder.ToString());
            streamWriter.WriteLine(settings.InputFileFormat.ToString());

            streamWriter.WriteLine(settings.OutputFolder.ToString());
            streamWriter.WriteLine(settings.OutputFileFormat.ToString());
            streamWriter.WriteLine(settings.Resolution.ToString());
            streamWriter.WriteLine(settings.FrameRate.ToString());
            streamWriter.WriteLine(settings.Encoder.ToString());

            streamWriter.WriteLine(settings.DiscordWebhook.ToString());
            streamWriter.WriteLine(settings.DiscordMode.ToString());
            streamWriter.WriteLine(settings.DiscordShortcut.ToString());

            streamWriter.WriteLine(settings.ConsoleFontSize.ToString());

            streamWriter.Close();
            streamWriter.Dispose();
        }
        private void ApplySettings(Settings settings)
        {
            FFmpeg.OutputFolder = settings.OutputFolder;
            
            FFmpeg.FrameRate      = FFmpeg.FrameRates[settings.FrameRate].Value;
            FFmpeg.ResolutionName = FFmpeg.Resolutions[settings.Resolution].Name;
            FFmpeg.Resolution     = FFmpeg.Resolutions[settings.Resolution].Value;
            FFmpeg.Encoder        = FFmpeg.Encoders[settings.Encoder].Value;

            Discord.WebhookURL = settings.DiscordWebhook;
        }
        
        private void ButtonActivate_Click(object sender, EventArgs e)
        {
            if (!isWatcherActive)
            {
                if (!InitalizeFileSystemWatcher())
                {
                    return;
                }

                buttonSettings.Enabled = false;
                buttonActivate.Text = "Zatrzymaj monitorowanie";
                isWatcherActive = true;
            }
            else
            {
                buttonSettings.Enabled = true;
                buttonActivate.Text = "Rozpocznij monitorowanie";
                isWatcherActive = false;
            }
        }

        private void NumericUpDownConsoleFontSize_ValueChanged(object sender, EventArgs e)
        {
            UpdateConsoleFontSize();
        }

        void UpdateConsoleFontSize()
        {
            richTextBoxConsole.Font = new Font(richTextBoxConsole.Font.FontFamily, (float)numericUpDownConsoleFontSize.Value, richTextBoxConsole.Font.Style);
            groupBoxConsole.Invalidate();
        }

        private void ButtonClearConsole_Click(object sender, EventArgs e)
        {
            richTextBoxConsole.Clear();
        }

        /*******************************************************
         * Konsola
         *******************************************************/

        private int ConsoleWriteLine(string message)
        {
            string line = string.Empty;
            
            if (richTextBoxConsole.Text == string.Empty)
            {
                line = message;
            }
            else
            {
                line = Environment.NewLine + message;
            }

            richTextBoxConsole.AppendText(line);
            richTextBoxConsole.ScrollToCaret();

            return line.Length;
        }

        private void ConsoleWriteLine(string message, Color color, FontStyle fontStyle)
        {
            richTextBoxConsole.SelectAll();
            int messageStart = richTextBoxConsole.SelectionLength;

            int messageLength = ConsoleWriteLine(message);

            richTextBoxConsole.Select(messageStart, messageLength);

            richTextBoxConsole.SelectionColor = color;

            if(richTextBoxConsole.SelectionFont == null)
            {
                return;
            }

            richTextBoxConsole.SelectionFont = new Font(richTextBoxConsole.SelectionFont, fontStyle);
        }

        private void ConsoleWriteError(string clipFileName, string errorMessage)
        {
            Regex regex = new Regex(clipFileName);
            Match match = regex.Match(richTextBoxConsole.Text);

            if (match.Success)
            {
                richTextBoxConsole.Select(match.Index, match.Length);
                richTextBoxConsole.SelectionColor = Color.Red;
                richTextBoxConsole.SelectedText = $"{clipFileName} - {errorMessage}";
            }
        }

        private void ConsoleSetColor(string clipFileName, Color color, FontStyle fontStyle)
        {
            Regex regex = new Regex(clipFileName);
            Match match = regex.Match(richTextBoxConsole.Text);

            if (match.Success)
            {
                richTextBoxConsole.Select(match.Index, match.Length);
                richTextBoxConsole.SelectionFont = new Font(richTextBoxConsole.Font, fontStyle);
                richTextBoxConsole.SelectionColor = color;
            }
        }

        /*******************************************************
         * FileSystemWatcher
         *******************************************************/

        private bool InitalizeFileSystemWatcher()
        {
            if (FormSettings == null)
            {
                return false;
            }

            // Walidacja pola InputFolder
            if (FormSettings.Settings.InputFolder == null || FormSettings.Settings.InputFolder == string.Empty)
            {
                MessageBox.Show("Ustaw folder wejściowy.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(FormSettings.Settings.InputFolder))
            {
                MessageBox.Show($"Wybrany folder wejściowy nie istnieje.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Walidacja pola OutputFolder
            if (FormSettings.Settings.OutputFolder == null || FormSettings.Settings.OutputFolder == string.Empty)
            {
                MessageBox.Show("Nie określono folderu wyjściowego.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(FormSettings.Settings.OutputFolder))
            {
                MessageBox.Show($"Wybrany folder wyjściowy nie istnieje.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Wyłącz poprzedniego FileSystemWatchera
            if (FileSystemWatcher != null)
            {
                FileSystemWatcher.Dispose();
            }

            // Nowy FileSystemWatcher
            FileSystemWatcher = new FileSystemWatcher(FormSettings.Settings.InputFolder);
            FileSystemWatcher.Created += FileSystemWatcher_Created;
            FileSystemWatcher.Filter = FFmpeg.FileFormats[FormSettings.Settings.InputFileFormat].Name;
            FileSystemWatcher.IncludeSubdirectories = false;
            FileSystemWatcher.EnableRaisingEvents = true;

            return true;
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            // Dodaj plik na listę RichText
            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action(() =>
                {
                    ConsoleWriteLine(e.Name, Color.Gray, FontStyle.Regular);
                }));
            }
            else
            {
                ConsoleWriteLine(e.Name, Color.Gray, FontStyle.Regular);
            }

            // Dodaj plik do kolejki FFMpeg
            Task AddClipTask = new Task(() => FFmpeg.AddClip(e.FullPath, e.Name));
            AddClipTask.Start();

            return;
        }
        /*******************************************************
         * Zdarzenia FFmpeg
         *******************************************************/

        private void FFmpeg_FFmpegError(object? sender, FFmpeg.FFmpegErrorEventArgs e)
        {
            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action(() =>
                {
                    ConsoleWriteError(e.ClipFileName, e.ErrorMessage);
                }));
            }
            else
            {
                ConsoleWriteError(e.ClipFileName, e.ErrorMessage);
            }
        }

        private void FFmpeg_ConversionStarted(object? sender, FFmpeg.ConversionStartedEventArgs e)
        {
            // Ustaw nazwę nowego klipu 
            if (labelFileName.InvokeRequired)
            {
                labelFileName.Invoke(new Action(() => { labelFileName.Text = e.ClipFileName; }));
            }
            else
            {
                labelFileName.Text = e.ClipFileName;
            }

            if (progressBarOutput.InvokeRequired)
            {
                progressBarOutput.Invoke(new Action(() =>
                {
                    // możliwe, że warto dać też maksimum
                    progressBarOutput.Value = 0;
                }));
            }
            else
            {
                progressBarOutput.Value = 0;
            }
        }

        private void FFmpeg_ThumbnailCreated(object? sender, FFmpeg.ThumbnailCreatedEventArgs e)
        {
            if (labelFileName.InvokeRequired)
            {
                labelFileName.Invoke(new Action(() =>
                {
                    labelFileName.Text = e.ClipFileName;
                }));
            }
            else
            {
                labelFileName.Text = e.ClipFileName;
            }

            if (pictureBoxThumbnail.InvokeRequired)
            {
                pictureBoxThumbnail.Invoke(new Action(() =>
                {
                    pictureBoxThumbnail.Image = new Bitmap(e.ThumbnailFilePath);
                }));
            }
            else
            {
                pictureBoxThumbnail.Image = new Bitmap(e.ThumbnailFilePath);
            }

            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action(() =>
                {
                    ConsoleSetColor(e.ClipFileName, Color.Blue, FontStyle.Bold);
                }));
            }
            else
            {
                ConsoleSetColor(e.ClipFileName, Color.Blue, FontStyle.Bold);
            }
        }

        private void FFmpeg_ProgressChanged(object? sender, FFmpeg.ProgressChangedEventArgs e)
        {
            if (progressBarOutput.InvokeRequired)
            {
                progressBarOutput.Invoke(new Action(() =>
                {
                    progressBarOutput.Maximum = e.FrameCount;
                    progressBarOutput.Value = e.Frame;
                }));
            }
            else
            {
                progressBarOutput.Maximum = e.FrameCount;
                progressBarOutput.Value = e.Frame;
            }
        }

        private void FFmpeg_VideoCreated(object? sender, FFmpeg.VideoCreatedEventArgs e)
        {
            if (progressBarOutput.InvokeRequired)
            {
                progressBarOutput.Invoke(new Action(() =>
                {
                    progressBarOutput.Maximum = e.FrameCount;
                    progressBarOutput.Value = e.FrameCount;
                }));
            }
            else
            {
                progressBarOutput.Maximum = e.FrameCount;
                progressBarOutput.Value = e.FrameCount;
            }

            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action(() =>
                {
                    ConsoleSetColor(e.ClipFileName, Color.Green, FontStyle.Bold);
                }));
            }
            else
            {
                ConsoleSetColor(e.ClipFileName, Color.Green, FontStyle.Bold);
            }

            if(FormSettings == null)
            {
                return;
            }

            if (Discord == null)
            {
                return;
            }

            if (FormSettings.Settings.DiscordMode == 1)
            {
                Discord.Send(e.ClipFileName, e.OutputFilePath, e.OutputFileName);
            }
        }

        private void Discord_ClipSent(object? sender, Discord.ClipSentEventArgs e)
        {
            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action(() =>
                {
                    ConsoleSetColor(e.ClipFileName, Color.Purple, FontStyle.Bold);
                }));
            }
            else
            {
                ConsoleSetColor(e.ClipFileName, Color.Purple, FontStyle.Bold);
            }
        }

        private void Discord_DiscordError(object? sender, Discord.DiscordErrorEventArgs e)
        {
            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action(() =>
                {
                    ConsoleWriteError(e.ClipFileName, e.ErrorMessage);
                }));
            }
            else
            {
                ConsoleWriteError(e.ClipFileName, e.ErrorMessage);
            }
        }

        /*******************************************************
         * Skróty Klawiszowe
         *******************************************************/

        Keys ShortcutKey = new Keys();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys shortcut = ShortcutKey;

            //if (checkBoxControl.Checked) { shortcut |= Keys.Control; }
            //if (checkBoxShift.Checked) { shortcut |= Keys.Shift; }
            //if (checkBoxAlt.Checked) { shortcut |= Keys.Alt; }

            //if (textBoxShortcut.Focused == false)
            //{
            //    if (keyData == ShortcutKey)
            //    {
            //        MessageBox.Show("Shortcut");
            //        return true;
            //    }
            //}


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textBoxShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            //checkBoxControl.Checked = e.Control;
            //checkBoxShift.Checked = e.Shift;
            //checkBoxAlt.Checked = e.Alt;

            //ShortcutKey = e.KeyData;

            //textBoxShortcut.Text = string.Empty;
            //textBoxShortcut.Text = e.KeyData.ToString();

        }

        
    }
}