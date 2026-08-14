using DiscordClipper.Properties;
using System.Text.RegularExpressions;

namespace DiscordClipper
{
    public partial class FormMain : Form
    {
        /*******************************************************
         * Obiekty Globalne
         *******************************************************/

        // Okno ustawień
        FormSettings? FormSettings;

        // Okno ustawień
        FormConsole? FormConsole;

        // Monitorowanie folderu
        FileSystemWatcher? FileSystemWatcher;

        // Czy monitorowanie plików jest aktywne
        private bool isWatcherActive = false;

        // Klasa Settings
        Settings? Settings;

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
            FFmpeg.Console += FFmpeg_Console;
            FFmpeg.FFmpegError += FFmpeg_FFmpegError;
            FFmpeg.ConversionStarted += FFmpeg_ConversionStarted;
            FFmpeg.ThumbnailCreated += FFmpeg_ThumbnailCreated;
            FFmpeg.ProgressChanged += FFmpeg_ProgressChanged;
            FFmpeg.VideoCreated += FFmpeg_VideoCreated;

            // Utwórz obiekt klasy Discord
            Discord = new Discord();
            Discord.ClipSent += Discord_ClipSent;
            Discord.DiscordError += Discord_DiscordError;

            // Utwórz obiekt klasy Settings zawierający ustawienia domyślne
            string profileName = "settings";
            Settings = new Settings(profileName);

            // Załaduj ustawienia z pliku settings.txt
            Settings.Load();

            // Zastosuj tryb kolorów (tylko raz na początku)
            ApplyColorMode(Settings.ColorMode);

            // Zastosuj ustawienia do obiektów FFmpeg i Discord
            ApplySettings(Settings);

            // Utwórz formularz ustawień wykorzystując wczytane ustawienia
            FormSettings = new FormSettings(Settings);

            FormConsole = new FormConsole();
            FormConsole.FormClosed += FormConsole_FormClosed;

            FormConsole.ConsoleEventArgs e = new FormConsole.ConsoleEventArgs("Test");

            e.Sender = "XDD";
            e.MessagePriority = FormConsole.ConsoleEventArgs.Priority.Warning;

            FormConsole.WriteLine(e);
        }

        private void buttonConsole_Click(object sender, EventArgs e)
        {
            if (FormConsole == null)
            {
                return;
            }

            if (FormConsole.Visible)
            {
                buttonConsole.Text = "Pokaż konsolę";
                FormConsole.Hide();
            }
            else
            {
                buttonConsole.Text = "Ukryj konsolę";
                FormConsole.Show();
            }
        }

        private void FormConsole_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (FormConsole == null)
            {
                return;
            }

            buttonConsole.Text = "Pokaż konsolę";
            FormConsole.Hide();
        }

        private void FFmpeg_Console(object? sender, FormConsole.ConsoleEventArgs e)
        {
            if(FormConsole == null)
            {
                return;
            }

            if (sender != null)
            {
                string senderName = sender.GetType().Name;
                e.Sender = senderName;
            }

            FormConsole.WriteLine(e);
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

            if (FormSettings.DialogResult != DialogResult.OK)
            {
                return;
            }

            Settings = FormSettings.Settings;

            if (Settings == null)
            {
                return;
            }

            Settings.Save();

            ApplySettings(Settings);
        }

        private void ApplyColorMode(int colorMode)
        {
            switch (colorMode)
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
        }

        private void ApplySettings(Settings settings)
        {
            if (FFmpeg == null)
            {
                return;
            }

            FFmpeg.OutputFolder = settings.OutputFolder;

            FFmpeg.FrameRate = FFmpeg.FrameRates[settings.FrameRate].Value;
            FFmpeg.ResolutionName = FFmpeg.Resolutions[settings.Resolution].Name;
            FFmpeg.Resolution = FFmpeg.Resolutions[settings.Resolution].Value;
            FFmpeg.Encoder = FFmpeg.Encoders[settings.Encoder].Value;
            FFmpeg.MaxVideoBitrate = settings.MaxVideoBitrate.ToString();

            if (Discord == null)
            {
                return;
            }

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

        private void ButtonClearConsole_Click(object sender, EventArgs e)
        {
            //richTextBoxConsole.Clear();
        }

        //*******************************************************
        // FileSystemWatcher
        //*******************************************************

        private bool InitalizeFileSystemWatcher()
        {
            if (Settings == null)
            {
                return false;
            }

            // Walidacja pola InputFolder
            if (Settings.InputFolder == null || Settings.InputFolder == string.Empty)
            {
                MessageBox.Show("Ustaw folder wejściowy.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(Settings.InputFolder))
            {
                MessageBox.Show($"Wybrany folder wejściowy nie istnieje.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Walidacja pola OutputFolder
            if (Settings.OutputFolder == null || Settings.OutputFolder == string.Empty)
            {
                MessageBox.Show("Nie określono folderu wyjściowego.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(Settings.OutputFolder))
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
            FileSystemWatcher = new FileSystemWatcher(Settings.InputFolder);
            FileSystemWatcher.Created += FileSystemWatcher_Created;
            FileSystemWatcher.Filter = FFmpeg.InputFileFormats[Settings.InputFileFormat].Name;
            FileSystemWatcher.IncludeSubdirectories = false;
            FileSystemWatcher.EnableRaisingEvents = true;

            return true;
        }

        

        //*******************************************************
        // Funkcje pomocnicze formularza
        //*******************************************************

        /// <summary>
        /// Funkcja pomocnicza do ustawiania etykiety nazwy pliku
        /// </summary>
        /// <param name="fileName"></param>
        private void SetLabelFileName(string fileName)
        {
            if (labelFileName.InvokeRequired)
            {
                labelFileName.Invoke(new Action(() => { SetLabelFileName(fileName); }));
            }
            else
            {
                labelFileName.Text = fileName;
            }
        }

        /// <summary>
        /// Funkcja pomocnicza do ustawiania miniatury podglądu
        /// </summary>
        /// <param name="bitmap"></param>
        private void ClearPictureBox()
        {
            if (pictureBoxThumbnail.InvokeRequired)
            {
                pictureBoxThumbnail.Invoke(new Action(() => ClearPictureBox()));
            }
            else
            {
                if (pictureBoxThumbnail.Image != null)
                {
                    pictureBoxThumbnail.Image.Dispose();
                    pictureBoxThumbnail.Image = null;
                }
            }
        }

        /// <summary>
        /// Funkcja pomocnicza do ustawiania wartości paska postępu
        /// </summary>
        /// <param name="value"></param>
        private void SetProgressBar(int value)
        {
            if (progressBarOutput.InvokeRequired)
            {
                progressBarOutput.Invoke(new Action(() => SetProgressBar(value)));
            }
            else
            {
                progressBarOutput.Value = value;
            }
        }

        /// <summary>
        /// Funkcja pomocnicza do ustawiania wartości i zakresu paska postępu
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maximum"></param>
        private void SetProgressBar(int value, int maximum)
        {
            if (progressBarOutput.InvokeRequired)
            {
                progressBarOutput.Invoke(new Action(() => SetProgressBar(value, maximum)));
            }
            else
            {
                progressBarOutput.Value = value;
                progressBarOutput.Maximum = maximum;
            }
        }

        /// <summary>
        /// Funkcja pomocnicza do ustawiania miniatury podglądu
        /// </summary>
        /// <param name="bitmap"></param>
        private void SetPictureBoxImage(Bitmap bitmap)
        {
            if (pictureBoxThumbnail.InvokeRequired)
            {
                pictureBoxThumbnail.Invoke(new Action(() => SetPictureBoxImage(bitmap)));
            }
            else
            {
                pictureBoxThumbnail.Image = bitmap;
            }
        }

        private int AddDataGridViewClip(string clipName)
        {
            if (dataGridViewClips.InvokeRequired)
            {
                dataGridViewClips.Invoke(new Action(() => AddDataGridViewClip(clipName)));
            }
            else
            {
                // Znajdź wiersz i dodaj obraz
                dataGridViewClips.Rows.Add(new object[] { Resources.Replay, clipName, "Znaleziony" });
                return dataGridViewClips.Rows.Count - 1;
            }

            return -1;
        }


        /// <summary>
        /// Funkcja pomocnicza do ustawiania miniatury podglądu
        /// </summary>
        /// <param name="bitmap"></param>
        private void SetDataGridViewStatus(int rowIndex, string status)
        {
            if (dataGridViewClips.InvokeRequired)
            {
                dataGridViewClips.Invoke(new Action(() => SetDataGridViewStatus(rowIndex, status)));
            }
            else
            {
                // Znajdź wiersz i dodaj obraz
                dataGridViewClips.Rows[rowIndex].Cells[2].Value = status;
            }
        }
        /// <summary>
        /// Funkcja pomocnicza do ustawiania miniatury podglądu
        /// </summary>
        /// <param name="bitmap"></param>
        private void SetDataGridViewImage(int rowIndex, Bitmap bitmap)
        {
            if (dataGridViewClips.InvokeRequired)
            {
                dataGridViewClips.Invoke(new Action(() => SetDataGridViewImage(rowIndex, bitmap)));
            }
            else
            {
                // Znajdź wiersz i dodaj obraz
                dataGridViewClips.Rows[rowIndex].Cells[0].Value = bitmap;
            }
        }

        //*******************************************************
        // Zdarzenia
        //*******************************************************

        /// <summary>
        /// Zdarzenie utworzenia nowgo pliku w folderze wejściowym (kolor szary)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            // Sprawdzenie, czy nazwa nie jest pusta
            if (e.Name == null || e.Name == string.Empty)
            {
                return;
            }

            // Ustaw etykietę nazwy pliku
            SetLabelFileName(e.Name);

            // Dodaj plik na listę RichText
            AddDataGridViewClip(e.Name);

            // Dodawanie pliku do kolejki FFmpeg w osobnym wątku
            if (FFmpeg == null)
            {
                return;
            }

            Task AddClipTask = new Task(() => FFmpeg.AddClip(e.FullPath, e.Name));
            AddClipTask.Start();

            return;
        }

        /// <summary>
        /// Zdarzenie rozpoczęcia konwersji klipu (kolor niebieski)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpeg_ConversionStarted(object? sender, FFmpeg.ConversionStartedEventArgs e)
        {
            // Ustaw nazwę nowego klipu 
            SetLabelFileName(e.ClipFileName);

            // Ustaw status 
            SetDataGridViewStatus(0, "Konwersja rozpoczęta");

            // Ustaw pasek postępu na 0
            SetProgressBar(0);
        }

        /// <summary>
        /// Zdarzenie utworzenia miniatury klipu (kolor niebieski)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpeg_ThumbnailCreated(object? sender, FFmpeg.ThumbnailCreatedEventArgs e)
        {
            Bitmap bitmap = new Bitmap(e.ThumbnailFilePath);

            SetPictureBoxImage(bitmap);

            SetDataGridViewStatus(0, "Utworzono miniaturę");

            SetDataGridViewImage(0, bitmap);


        }

        /// <summary>
        /// Zdarzenie zmiany postępu konwersji klipu (kolor niebieski)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpeg_ProgressChanged(object? sender, FFmpeg.ProgressChangedEventArgs e)
        {
            SetProgressBar(e.Frame, e.FrameCount);
        }

        /// <summary>
        /// Zdarzenie zakończenia konwersji klipu (kolor zielony)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpeg_VideoCreated(object? sender, FFmpeg.VideoCreatedEventArgs e)
        {
            SetDataGridViewStatus(0, "Zakończono konwersję");

            SetProgressBar(e.FrameCount, e.FrameCount);

            if (Settings == null)
            {
                return;
            }

            if (Discord == null)
            {
                return;
            }

            if (Settings.DiscordMode == 1)
            {
                Discord.Send(e.ClipFileName, e.OutputFilePath, e.OutputFileName);
            }
        }

        private void Discord_ClipSent(object? sender, Discord.ClipSentEventArgs e)
        {

        }

        private void FFmpeg_FFmpegError(object? sender, FFmpeg.FFmpegErrorEventArgs e)
        {
            FormConsole.WriteLine(new FormConsole.ConsoleEventArgs($"Błąd FFmpeg: {e.ErrorMessage}") { Sender = "FFmpeg", MessagePriority = FormConsole.ConsoleEventArgs.Priority.Error });
        }

        private void Discord_DiscordError(object? sender, Discord.DiscordErrorEventArgs e)
        {

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