using DiscordClipper.Properties;
using System.Diagnostics;
using static DiscordClipper.FFmpeg;
using static DiscordClipper.Logger;

namespace DiscordClipper
{
    public partial class FormMain : Form
    {
        /*******************************************************
         * Obiekty Globalne
         *******************************************************/

        /// <summary>
        /// Lista przetworzonych klipów przenaczonych do sysłania na Discorda
        /// </summary>
        List<Clip> OutputClips = new List<Clip>();

        /// <summary>
        /// Lista miniatur utworzonych przez FFmpeg, które należy usunąć po zamknięciu programu
        /// </summary>
        List<string> Thumbnails = new List<string>();

        /// <summary>
        /// Formularz ustawień
        /// </summary>
        FormSettings? FormSettings;

        /// <summary>
        /// Formularz konsoli
        /// </summary>
        FormConsole? FormConsole;

        /// <summary>
        /// Monitorowanie folderu
        /// </summary>
        FileSystemWatcher? FileSystemWatcher;

        /// <summary>
        /// Informacja o aktywności FileSystemWatcher'a
        /// </summary>
        private bool IsFileSystemWatcherActive = false;

        /// <summary>
        /// Obiekt klasy Settings do przechowywania ustawień programu
        /// </summary>
        Settings? Settings;

        /// <summary>
        /// Obiekt klasy FFmpeg
        /// </summary>
        FFmpeg? FFmpeg;

        /// <summary>
        /// Obiekt klasy Discord
        /// </summary>
        Discord? Discord;

        /*******************************************************
         * FormMain 
         *******************************************************/

        /// <summary>
        /// Konstruktor klasy FormMain
        /// </summary>
        public FormMain()
        {
            Logger.WriteLine("Inicjalizacja okna głównego...", Priority.Info);
            InitializeComponent();

            // Utwórz obiekt klasy FFmpeg i dodaj obsługę zdarzeń
            Logger.WriteLine("Tworzenie obiektu klasy FFmpeg...", Priority.Info);
            FFmpeg = new FFmpeg();
            FFmpeg.FFmpegError += FFmpeg_FFmpegError;
            FFmpeg.ConversionStarted += FFmpeg_ConversionStarted;
            FFmpeg.ThumbnailCreated += FFmpeg_ThumbnailCreated;
            FFmpeg.ProgressChanged += FFmpeg_ProgressChanged;
            FFmpeg.VideoCreated += FFmpeg_VideoCreated;

            // Utwórz obiekt klasy Discord
            Logger.WriteLine("Tworzenie obiektu klasy Discord...", Priority.Info);
            Discord = new Discord();
            Discord.ClipSent += Discord_ClipSent;
            Discord.DiscordError += Discord_DiscordError;

            // Utwórz obiekt klasy Settings zawierający ustawienia domyślne
            Logger.WriteLine("Tworzenie obiektu klasy Settings...", Priority.Info);
            string profileName = "settings";
            Settings = new Settings(profileName);

            // Załaduj ustawienia z pliku settings.txt
            Settings.Load();

            // Zastosuj ustawienia do obiektów FFmpeg i Discord
            ApplySettings(Settings);

            // Zastosuj tryb kolorów (tylko raz na początku)
            ApplyColorMode(Settings.ColorMode);

            // Ustaw proporcje miniatury podglądu
            SetPictureBoxProportions();

            // Utwórz formularz ustawień wykorzystując wczytane ustawienia
            // Może lepiej zrobić tak jak z console Window, i tworzyć go za każydym razem od nowa
            Logger.WriteLine("Tworzenie obiektu klasy FormSettings...", Priority.Info);
            FormSettings = new FormSettings(Settings);
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Zwolnij wszystkie obrazy z dataGridViewClips
            if (dataGridViewClips.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewClips.Rows)
                {
                    if (row.Cells[0].Value is Bitmap bitmap)
                    {
                        bitmap.Dispose();
                        bitmap = null;
                    }
                }
            }

            // Zwolnij obraz z pictureBoxThumbnail
            if (pictureBoxThumbnail.Image != null)
            {
                pictureBoxThumbnail.Image.Dispose();
                pictureBoxThumbnail.Image = null;
            }

            // Usuń wsystkie utworzone pliki bitmap
            foreach (string thumbnailFilePath in Thumbnails)
            {
                if (File.Exists(thumbnailFilePath))
                {
                    try
                    {
                        File.Delete(thumbnailFilePath);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine($"Nie można usunąć pliku miniatury: {thumbnailFilePath}: {ex.Message}", Priority.Error);
                        MessageBox.Show($"Nie można usunąć pliku miniatury: {thumbnailFilePath}: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /*******************************************************
         * MenuStrip 
         *******************************************************/

        private void toolStripMenuItemVersion_Click(object sender, EventArgs e)
        {
            string productVersion = Application.ProductVersion;

            string[] versionParts = productVersion.Split('+');

            if (versionParts.Length > 1)
            {
                productVersion = versionParts[0];
            }

            MessageBox.Show($"Discord Clipper v{productVersion}", "Informacja o wersji", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemOpenAppData_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.UserAppDataPath);
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
            Logger.WriteLine($"Otwieranie adresu \"{url}\" w przeglądarce...", Priority.Info);

            Process browserProcess = new Process();
            browserProcess.StartInfo.FileName = url;
            browserProcess.StartInfo.UseShellExecute = true;

            try
            {
                browserProcess.Start();
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Błąd otwierania przeglądarki: {ex.Message}", Priority.Error);
            }
        }

        private void toolStripMenuItemCheckFFmpeg_Click(object sender, EventArgs e)
        {
            if (FFmpeg == null)
            {
                Logger.WriteLine("Obiekt FFmpeg ma wartość null.", Priority.Error);
                return;
            }

            string? ffmpegVersion = FFmpeg.Version();

            if (ffmpegVersion != null)
            {
                MessageBox.Show($"Program FFmpeg jest zainstalowany. \n\n {ffmpegVersion}", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Program FFmpeg nie jest zainstalowany.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult result = MessageBox.Show("Czy chcesz przejść do strony z instrukcją instalacji programu FFmpeg?", "Instalacja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    OpenBrowser("https://github.com/MaciejPaszek/DiscordClipper/wiki/Instalacja");
                }
            }
        }

        /*******************************************************
         * FormConsole
         *******************************************************/

        private void toolStripMenuItemOpenConsole_Click(object sender, EventArgs e)
        {
            if (FormConsole == null)
            {
                // Okno konsoli jest zamknięte, otwórz je
                FormConsole = new FormConsole();
                FormConsole.FormClosed += FormConsole_FormClosed;
                FormConsole.Show();
            }
            else
            {
                // Okno konsoli jest otwarte, pokaż je na wierzchu
                FormConsole.Activate();
            }
        }

        /// <summary>
        /// Zwalnianie konsoli po jej zamknięciu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormConsole_FormClosed(object? sender, FormClosedEventArgs e)
        {
            FormConsole = null;
        }

        /*******************************************************
         * FormSettings
         *******************************************************/

        private void ButtonSettings_Click(object? sender, EventArgs e)
        {
            if (FormSettings == null)
            {
                Logger.WriteLine("Obiekt FormSettings ma wartość null.", Priority.Error);
                return;
            }

            Logger.WriteLine("Otwieranie okna ustawień...", Priority.Info);
            try
            {
                FormSettings.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Błąd otwierania okna ustawień: {ex.Message}", Priority.Error);
            }

            if (FormSettings.DialogResult != DialogResult.OK)
            {
                Logger.WriteLine("Anulowano zmiany w ustawieniach.", Priority.Info);
                return;
            }

            // Pobierz ustawienia z formularza ustawień
            Settings = FormSettings.Settings;

            if (Settings == null)
            {
                Logger.WriteLine("Obiekt Settings ma wartość null.", Priority.Error);
                return;
            }

            Settings.Save();

            ApplySettings(Settings);
        }

        /*******************************************************
         * Dodawanie Klipów Przyciskiem
         *******************************************************/

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

        /*******************************************************
         * FileSystemWatcher
         *******************************************************/

        private void ButtonActivate_Click(object sender, EventArgs e)
        {
            if (!IsFileSystemWatcherActive)
            {
                if (!InitalizeFileSystemWatcher())
                {
                    Logger.WriteLine("Nie można aktywować FileSystemWatcher'a.", Priority.Error);
                    return;
                }

                buttonSettings.Enabled = false;
                buttonActivate.Text = "Zatrzymaj monitorowanie";
                IsFileSystemWatcherActive = true;
            }
            else
            {
                Logger.WriteLine("Zakończono monitorowanie klipów.", Priority.Info);

                buttonSettings.Enabled = true;
                buttonActivate.Text = "Rozpocznij monitorowanie";
                IsFileSystemWatcherActive = false;
            }
        }
        private bool InitalizeFileSystemWatcher()
        {
            Logger.WriteLine("Inicjalizacja nowego obiektu FileSystemWatcher...", Priority.Info);

            if (Settings == null)
            {
                Logger.WriteLine("Obiekt Settings ma wartość null.", Priority.Error);
                return false;
            }

            // Walidacja pola InputFolder
            if (Settings.InputFolder == null || Settings.InputFolder == string.Empty)
            {
                Logger.WriteLine("Folder wejściowy ma wartość null.", Priority.Error);

                MessageBox.Show("Nie wybrano folderu wejściowego do monitorowania klipów.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(Settings.InputFolder))
            {
                Logger.WriteLine($"Folder wejściowy \"{Settings.InputFolder}\" nie istnieje.", Priority.Error);

                MessageBox.Show($"Folder wejściowy \"{Settings.InputFolder}\" nie istnieje.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Walidacja pola OutputFolder
            if (Settings.OutputFolder == null || Settings.OutputFolder == string.Empty)
            {
                Logger.WriteLine("Folder wyjściowy ma wartość null.", Priority.Error);

                MessageBox.Show("Nie wybrano folderu wyjściowego do zapisywania klipów.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(Settings.OutputFolder))
            {
                Logger.WriteLine($"Folder wyjściowy \"{Settings.OutputFolder}\" nie istnieje.", Priority.Error);

                MessageBox.Show($"Folder wyjściowy \"{Settings.OutputFolder}\" nie istnieje.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Wyłącz poprzedniego FileSystemWatchera
            if (FileSystemWatcher != null)
            {
                Logger.WriteLine("Deaktywacja poprzedniego obiektu FileSystemWatcher...", Priority.Info);

                FileSystemWatcher.Dispose();
            }

            // Nowy FileSystemWatcher
            Logger.WriteLine("Aktywacja nowego obiektu FileSystemWatcher...", Priority.Info);

            FileSystemWatcher = new FileSystemWatcher(Settings.InputFolder);
            FileSystemWatcher.Created += FileSystemWatcher_Created;
            FileSystemWatcher.Filter = FFmpeg.InputFileFormats[Settings.InputFileFormat].Name;
            FileSystemWatcher.IncludeSubdirectories = false;
            FileSystemWatcher.EnableRaisingEvents = true;

            return true;
        }

        /*******************************************************
         * Funkcje Pomocnicze Formularza Głównego
         *******************************************************/

        private void ApplyColorMode(int colorMode)
        {
            switch (colorMode)
            {
                case 0:
                    Logger.WriteLine("Stosowanie jasnego trybu kolorów...", Priority.Info);
                    Application.SetColorMode(SystemColorMode.Classic);
                    break;
                case 1:
                    Logger.WriteLine("Stosowanie ciemnego trybu kolorów...", Priority.Info);
                    Application.SetColorMode(SystemColorMode.Dark);
                    break;
                default:
                    Logger.WriteLine("Stosowanie systemowego trybu kolorów...", Priority.Info);
                    Application.SetColorMode(SystemColorMode.System);
                    break;
            }
        }

        private bool ApplySettings(Settings settings)
        {
            Logger.WriteLine("Stosowanie ustawień...", Priority.Info);

            if (FFmpeg == null)
            {
                Logger.WriteLine("Obiekt FFmpeg ma wartość null.", Priority.Error);
                return false;
            }

            FFmpeg.OutputFolder = settings.OutputFolder;
            FFmpeg.FrameRate = FFmpeg.FrameRates[settings.FrameRate].Value;
            FFmpeg.ResolutionName = FFmpeg.Resolutions[settings.Resolution].Name;
            FFmpeg.Resolution = FFmpeg.Resolutions[settings.Resolution].Value;
            FFmpeg.Encoder = FFmpeg.Encoders[settings.Encoder].Value;
            FFmpeg.MaxVideoBitrate = settings.MaxVideoBitrate.ToString();

            if (Discord == null)
            {
                Logger.WriteLine("Obiekt Discord ma wartość null.", Priority.Error);
                return false;
            }

            Discord.WebhookURL = settings.DiscordWebhook;

            return true;
        }

        private bool AddClip(string filePath)
        {
            Logger.WriteLine($"Dodawanie klipu {filePath} do kolejki...", Priority.Info);

            if (Settings == null)
            {
                Logger.WriteLine("Obiekt Settings ma wartość null.", Priority.Error);
                return false;
            }

            // Walidacja pola OutputFolder
            if (Settings.OutputFolder == null || Settings.OutputFolder == string.Empty)
            {
                Logger.WriteLine("Folder wyjściowy ma wartość null.", Priority.Error);

                MessageBox.Show("Nie wybrano folderu wyjściowego do zapisywania klipów.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(Settings.OutputFolder))
            {
                Logger.WriteLine($"Folder wyjściowy \"{Settings.OutputFolder}\" nie istnieje.", Priority.Error);

                MessageBox.Show($"Folder wyjściowy \"{Settings.OutputFolder}\" nie istnieje.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Dodaj plik na listę
            int clipID = AddDataGridViewClip(Path.GetFileName(filePath));

            // Dodawanie pliku do kolejki FFmpeg w osobnym wątku
            if (FFmpeg == null)
            {
                Logger.WriteLine("Obiekt FFmpeg ma wartość null.", Priority.Error);
                return false;
            }

            FFmpeg.AddClip(clipID, filePath);

            return true;
        }

        private bool SendToDiscord(int clipID, string filePath)
        {
            if (Discord == null)
            {
                Logger.WriteLine("Obiekt Discord ma wartość null.", Priority.Error);
                return false;
            }

            if (Settings == null)
            {
                Logger.WriteLine("Obiekt Settings ma wartość null.", Priority.Error);
                return false;
            }

            // Walidacja pola DiscordWebhook
            if (Settings.DiscordWebhook == null || Settings.DiscordWebhook == string.Empty)
            {
                Logger.WriteLine("Obiekt DiscordWebhook ma wartość null.", Priority.Error);

                MessageBox.Show("Nie określono adresu URL Webhooka kanału na Discordzie.", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

            return true;
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

        private void splitContainerOutput_DoubleClick(object sender, EventArgs e)
        {
            SetPictureBoxProportions();
        }

        /// <summary>
        /// Funkcja pomocnicza do ustawiania proporcji miniatury podglądu
        /// </summary>
        private void SetPictureBoxProportions()
        {
            Logger.WriteLine("Ustawianie proporcji miniatury podglądu...", Priority.Info);

            // Szerokość obrazu bez paddingu i marginu
            int imageWidth = pictureBoxThumbnail.ClientSize.Width
                - pictureBoxThumbnail.Margin.Left - pictureBoxThumbnail.Margin.Right
                - pictureBoxThumbnail.Padding.Left - pictureBoxThumbnail.Padding.Right;

            // Wysokość obrazu bez paddingu i marginu
            int imageHeight = pictureBoxThumbnail.ClientSize.Height
                - pictureBoxThumbnail.Margin.Top - pictureBoxThumbnail.Margin.Bottom
                - pictureBoxThumbnail.Padding.Top - pictureBoxThumbnail.Padding.Bottom;

            // Tyle, ile jest vs tyle, ile powinno być
            int heightDifference = imageHeight - imageWidth * 9 / 16;

            // Zastsuj przesunięcie, oblicz nową pozycję
            int newSplitterDistance = splitContainerOutput.SplitterDistance + heightDifference;

            // Ograniczenie z dołu
            if (newSplitterDistance < 0)
            {
                newSplitterDistance = 0;
            }

            // Ograniczenie z góry
            if (newSplitterDistance > splitContainerOutput.ClientSize.Height)
            {
                newSplitterDistance = splitContainerOutput.ClientSize.Height;
            }

            // Zastosuj nową pozycję
            try
            {
                splitContainerOutput.SplitterDistance = newSplitterDistance;

            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Błąd podczas ustawiania proporcji miniatury podglądu: {ex.Message}", Priority.Error);
            }
        }

        /*******************************************************
         * Zdarzenia Klipów
         *******************************************************/

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

            Thumbnails.Add(e.ThumbnailFilePath);

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
                Logger.WriteLine("Obiekt Settings ma wartość null.", Priority.Error);
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

        // Inne


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

            string? value = (string?)dataGridViewClips.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

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

            if (!File.Exists(clip.FilePath))
            {
                return;
            }

            SendToDiscord(clip.ClipID, clip.FilePath);
        }
    }
}