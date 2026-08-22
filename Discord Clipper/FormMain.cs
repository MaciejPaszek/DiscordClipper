using DiscordClipper.Properties;
using System.Diagnostics;
using static DiscordClipper.FFmpeg;
using static DiscordClipper.FormConsole;

namespace DiscordClipper
{
    public partial class FormMain : Form
    {
        /*******************************************************
         * Obiekty Globalne
         *******************************************************/

        List<Clip> OutputClips = new List<Clip>();

        // Okno ustawień
        FormSettings? FormSettings;

        // Okno konsoli
        FormConsole? FormConsole;
        List<ConsoleLineEventArgs> ListConsoleLineEventArgs = new List<ConsoleLineEventArgs>();

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

        /// <summary>
        /// Konstruktor klasy FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // Utwóz formularz konsoli
            //FormConsole = new FormConsole();

            // Rozwiązanie tymczasowe na crash consoli
            //FormConsole.Show();
            //FormConsole.Hide();

            // Utwórz obiekt klasy FFmpeg i dodaj obsługę zdarzeń
            FFmpeg = new FFmpeg();
            FFmpeg.ConsoleLine += ConsoleLine;
            FFmpeg.FFmpegError += FFmpeg_FFmpegError;
            FFmpeg.ConversionStarted += FFmpeg_ConversionStarted;
            FFmpeg.ThumbnailCreated += FFmpeg_ThumbnailCreated;
            FFmpeg.ProgressChanged += FFmpeg_ProgressChanged;
            FFmpeg.VideoCreated += FFmpeg_VideoCreated;

            // Utwórz obiekt klasy Discord
            Discord = new Discord();
            Discord.ConsoleLine += ConsoleLine;
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
        }

        /*******************************************************
         * Okno Konsoli 
         *******************************************************/

        /// <summary>
        /// OBsługa przycisku do tworzenia okna konsoli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConsole_Click(object sender, EventArgs e)
        {
            if (FormConsole == null)
            {
                // Okno konsoli jest zamknięte, otwórz je

                FormConsole = new FormConsole(ListConsoleLineEventArgs);
                FormConsole.FormClosed += FormConsole_FormClosed;
                FormConsole.Show();

                buttonConsole.Text = "Ukryj konsolę";

            }
            else
            {
                // Okno konsoli jest otwarte, zamknij je
                FormConsole.Close();
            }

            return;
        }

        /// <summary>
        /// Zwalnianie konsoli po jej zamknięciu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormConsole_FormClosed(object? sender, FormClosedEventArgs e)
        {
            FormConsole = null;
            buttonConsole.Text = "Pokaż konsolę";
        }

        /// <summary>
        /// Funkcja do pisania konsoli przez inne okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleLine(object? sender, ConsoleLineEventArgs e)
        {
            // Konsola jest ukryta, dodaj do kolejki
            ListConsoleLineEventArgs.Add(e);

            // JEśli okno konsoli jest zamknięte, to koniec
            if (FormConsole == null)
            {
                return;
            }

            // Jeśli okno konsoli jest otwarte, to dodaj nowy
            FormConsole.WriteLine(e);

            return;
        }

        /// <summary>
        /// Przełączanie widoczności konsoli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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

        private void ButtonAddClips_Click(object sender, EventArgs e)
        {
            //FileBrowserDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wybierz folder wejściowy";
            openFileDialog.Filter = "Matroska (*.mkv)|*.mkv|MPEG-4(*.mp4)|*.mp4";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    AddClip(filePath);
                }
            }

            return;
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

        private void AddClip(string filePath)
        {
            ConsoleLine(this.ToString(), new ConsoleLineEventArgs($"Dodawanie pliku \"{filePath}\" do kolejki..."));

            // Dodaj plik na listę
            int clipID = AddDataGridViewClip(Path.GetFileName(filePath));

            // Dodawanie pliku do kolejki FFmpeg w osobnym wątku
            if (FFmpeg == null)
            {
                return;
            }

            FFmpeg.AddClip(clipID, filePath);

            ConsoleLine(this.ToString(), new ConsoleLineEventArgs($"Dodano plik \"{filePath}\" do kolejki."));

            return;
        }

        private void SendToDiscord(int clipID, string filePath)
        {
            if (Discord == null)
            {
                return;
            }

            if (Application.ColorMode == SystemColorMode.Classic)
            {
                SetDataGridViewStatus(clipID, "Wysyłanie...", Color.Purple);
            }
            else
            {
                SetDataGridViewStatus(clipID, "Wysyłanie...", Color.MediumPurple);
            }

            Discord.AddClip(clipID, filePath);
          
            return;
        }

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
                dataGridViewClips.Rows.Add(new object[] { Resources.Replay, clipName, "Oczekiwanie", "Oczekiwanie" });
                dataGridViewClips.FirstDisplayedScrollingRowIndex = dataGridViewClips.Rows.Count - 1;
            }

            // Zwróć numer klipu
            return dataGridViewClips.Rows.Count - 1;
        }


        /// <summary>
        /// Funkcja pomocnicza do ustawiania miniatury podglądu
        /// </summary>
        /// <param name="bitmap"></param>
        private void SetDataGridViewStatus(int rowIndex, string status, Color color)
        {
            if (dataGridViewClips.InvokeRequired)
            {
                dataGridViewClips.Invoke(new Action(() => SetDataGridViewStatus(rowIndex, status, color)));
            }
            else
            {
                // Znajdź wiersz i dodaj obraz
                dataGridViewClips.Rows[rowIndex].Cells[2].Value = status;
                dataGridViewClips.Rows[rowIndex].Cells[1].Style.ForeColor = color;
                dataGridViewClips.Rows[rowIndex].Cells[2].Style.ForeColor = color;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="status"></param>
        /// <param name="color"></param>
        private void SetDataGridViewButton(int rowIndex, string value)
        {
            if (dataGridViewClips.InvokeRequired)
            {
                dataGridViewClips.Invoke(new Action(() => SetDataGridViewButton(rowIndex, value)));
            }
            else
            {
                dataGridViewClips.Rows[rowIndex].Cells[3].Value = value;
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
            // Dodaj klip do kolejki FFmpeg
            AddClip(e.FullPath);

            return;
        }

        /// <summary>
        /// Zdarzenie utworzenia miniatury klipu (kolor niebieski)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpeg_ThumbnailCreated(object? sender, FFmpeg.ThumbnailCreatedEventArgs e)
        {
            SetDataGridViewImage(e.ClipID, new Bitmap(e.ThumbnailFilePath));
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

            SetPictureBoxImage(new Bitmap(e.ThumbnailFilePath));

            // Ustaw status
            if (Application.ColorMode == SystemColorMode.Classic)
            {
                SetDataGridViewStatus(e.ClipID, "Rozpoczęto", Color.Blue);
            }
            else
            {
                SetDataGridViewStatus(e.ClipID, "Rozpoczęto", Color.LightBlue);
            }

            // Ustaw pasek postępu na 0
            SetProgressBar(0);
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
            if (Application.ColorMode == SystemColorMode.Classic)
            {
                SetDataGridViewStatus(e.ClipID, "Ukończono", Color.Green);
            }
            else
            {
                SetDataGridViewStatus(e.ClipID, "Ukończono", Color.LightGreen);
            }

            SetProgressBar(e.FrameCount, e.FrameCount);

            OutputClips.Add(new Clip(e.ClipID, e.OutputFilePath));

            if (Settings == null)
            {
                return;
            }

            if (Settings.DiscordMode == 0)
            {
                // Nie wysyłaj klipów automatycznie
                SetDataGridViewButton(e.ClipID, "Wyślij");
            }
            else
            {
                // Wysyłaj klipy automatycznie
                SendToDiscord(e.ClipID, e.OutputFilePath);
            }
        }

        private void Discord_ClipSent(object? sender, Discord.ClipSentEventArgs e)
        {
            if (Application.ColorMode == SystemColorMode.Classic)
            {
                SetDataGridViewStatus(e.ClipID, "Wysłano", Color.Purple);
            }
            else
            {
                SetDataGridViewStatus(e.ClipID, "Wysłano", Color.MediumPurple);
            }

            SetDataGridViewButton(e.ClipID, "Wysłano");
        }

        private void FFmpeg_FFmpegError(object? sender, FFmpeg.FFmpegErrorEventArgs e)
        {
            SetDataGridViewStatus(e.ClipID, "FFmpeg error", Color.Red);
        }

        private void Discord_DiscordError(object? sender, Discord.DiscordErrorEventArgs e)
        {
            SetDataGridViewStatus(e.ClipID, "Discord error", Color.Red);
        }

        private void instrukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://github.com/MaciejPaszek/DiscordClipper/wiki");
        }

        private void zgłośBłądToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://github.com/MaciejPaszek/DiscordClipper/issues");

        }

        private void OpenBrowser(string url)
        {
            Process browserProcess = new Process();
            browserProcess.StartInfo.FileName = url;
            browserProcess.StartInfo.UseShellExecute = true;

            browserProcess.Start();

        }

        private void otwórzFolderAppDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.UserAppDataPath);
        }

        private void dataGridViewClips_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewClips.ClearSelection();
        }

        private void dataGridViewClips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 3)
            {
                // Nie jest to kolumna przycisków.
                return;
            }

            string? value = (string?) dataGridViewClips.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (value == null)
            {
                return;
            }

            if (value == "Oczekiwanie")
            {
                return;
            }

            if (value == "Wysłano")
            {
                return;
            }

            Clip clip = OutputClips.Find(x => x.ClipID == e.RowIndex);

            if(!File.Exists(clip.FilePath))
            {
                return;
            }

            SendToDiscord(clip.ClipID, clip.FilePath);
        }
    }
}