using System.Diagnostics;
using static DiscordClipper.FormConsole;
using static System.Windows.Forms.LinkLabel;

namespace DiscordClipper
{
    /// <summary>
    /// Struktura do listowania opcji w kontrolkach oi odpowiadających im wartości
    /// name 
    /// </summary>
    struct Option
    {
        public string Name;
        public string Value;
        public Option(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    internal class FFmpeg
    {
        /// <summary>
        /// Opcje FFmpeg
        /// </summary>
        public static readonly Option[] InputFileFormats = {
            new Option("*.mkv", "mkv"),
            new Option("*.mp4", "mp4")
        };

        public static readonly Option[] OutputFileFormats = {
            new Option("*.mp4", "mp4")
        };

        public static readonly Option[] Resolutions = {
            new Option( "144p",   "256x144"),
            new Option( "240p",   "427x240"),
            new Option( "360p",   "640x360"),
            new Option( "480p",   "854x480"),
            new Option( "720p",  "1280x720"),
            new Option("1080p", "1920x1080"),
        };

        public static readonly Option[] FrameRates = {
            new Option("24 FPS", "24"),
            new Option("30 FPS", "30"),
            new Option("60 FPS", "60")
        };

        public static readonly Option[] Encoders = {
            new Option("x264", "libx264"),
            new Option("AMD AMF H.264", "h264_amf"),
            new Option("Intel Quick Sync Video H.264", "h264_qsv"),
            new Option("NVIDIA NVENC H.264", "h264_nvenc")
        };

        // Pola publiczne opcji FFmpeg
        public string OutputFolder = string.Empty;
        public string Resolution = string.Empty;
        public string ThumbnailResolution = string.Empty;
        public string ResolutionName = string.Empty;
        public string FrameRate = string.Empty;
        public string Encoder = string.Empty;
        public string MaxVideoBitrate = string.Empty;

        /// <summary>
        /// Zdarzenia
        /// </summary>
        public event EventHandler<ConsoleLineEventArgs>? ConsoleLine;
        public event EventHandler<FFmpegErrorEventArgs>? FFmpegError;
        private event EventHandler? ThumnbailQueueClipAdded;
        public event EventHandler<ThumbnailCreatedEventArgs>? ThumbnailCreated;
        private event EventHandler? VideoQueueClipAdded;
        public event EventHandler<ConversionStartedEventArgs>? ConversionStarted;
        public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
        public event EventHandler<VideoCreatedEventArgs>? VideoCreated;

        /// <summary>
        /// Argumenty zdarzeń
        /// </summary>
        public class FFmpegErrorEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;

            public FFmpegErrorEventArgs(int clipID)
            {
                ClipID = clipID;
            }
        }

        public class ThumbnailCreatedEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public string ThumbnailFilePath { get; set; } = string.Empty;
            public ThumbnailCreatedEventArgs(int clipID, string thumbnailFilePath)
            {
                ClipID = clipID;
                ThumbnailFilePath = thumbnailFilePath;
            }
        }

        public class ConversionStartedEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public string ClipFileName { get; private set;  } = string.Empty;
            public string ThumbnailFilePath { get; private set;  } = string.Empty;
            public ConversionStartedEventArgs(int clipId, string clipFileName, string thumbnailFilePath)
            {
                ClipID = clipId;
                ClipFileName = clipFileName;
                ThumbnailFilePath = thumbnailFilePath;
            }
        }

        public class ProgressChangedEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public int Frame { get; set; }
            public int FrameCount { get; set; }

            public ProgressChangedEventArgs(int clipID, int frame, int frameCount)
            {
                ClipID = clipID;
                Frame = frame;
                FrameCount = frameCount;
            }
        }

        public class VideoCreatedEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public string OutputFilePath { get; set; } = string.Empty;
            public int FrameCount { get; set; }

            public VideoCreatedEventArgs(int clipID, string outputFilePath, int frameCount)
            {
                ClipID = clipID;
                OutputFilePath = outputFilePath;
                FrameCount = frameCount;
            }
        }

        // Metody zapalania zdarzeń
        protected virtual void OnConsoleLine(ConsoleLineEventArgs e)
        {
            ConsoleLine?.Invoke(this, e);
        }
        protected virtual void OnThumbnailQueueClipAdded(EventArgs e)
        {
            ThumnbailQueueClipAdded?.Invoke(this, e);
        }
        protected virtual void OnVideoQueueClipAdded(EventArgs e)
        {
            VideoQueueClipAdded?.Invoke(this, e);
        }
        protected virtual void OnFFmpegError(FFmpegErrorEventArgs e)
        {
            FFmpegError?.Invoke(this, e);
        }
        protected virtual void OnConversionStarted(ConversionStartedEventArgs e)
        {
            ConversionStarted?.Invoke(this, e);
        }
        protected virtual void OnThumbnailCreated(ThumbnailCreatedEventArgs e)
        {
            ThumbnailCreated?.Invoke(this, e);
        }
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
        protected virtual void OnVideoCreated(VideoCreatedEventArgs e)
        {
            VideoCreated?.Invoke(this, e);
        }

        /// <summary>
        /// Struktura do przechowywania informacji o klipie w kolejce
        /// </summary>
        private struct Clip
        {
            public int ClipID;
            public string FilePath;

            public Clip(int clipID, string filePath)
            {
                ClipID = clipID;
                FilePath = filePath;
            }
        };

        /// <summary>
        /// Kolejka dla miniatur
        /// </summary>
        private Queue<Clip> ThumbnailQueue = new Queue<Clip>();

        /// <summary>
        /// Kolejka klipów
        /// </summary>
        private Queue<Clip> VideoQueue = new Queue<Clip>();
        /// <summary>
        /// Flaga
        /// </summary>
        private bool VideoProcessActive = false;
        private bool ThumbnailProcessActive = false;

        /// <summary>
        /// Konstruktor klasy FFmpeg
        /// </summary>
        public FFmpeg()
        {
            ThumnbailQueueClipAdded += FFmpeg_ThumnailQueueClipAdded;
            VideoQueueClipAdded += FFmpeg_VideoQueueClipAdded;
        }

        public int CheckVersion()
        {
            Process versionProcess = new Process();

            versionProcess.StartInfo.FileName = "ffmpeg.exe";
            versionProcess.StartInfo.Arguments = "-version";
            versionProcess.StartInfo.UseShellExecute = false;
            versionProcess.StartInfo.CreateNoWindow = false;
            versionProcess.StartInfo.RedirectStandardOutput = true;

            OnConsoleLine(new ConsoleLineEventArgs($"{versionProcess.StartInfo.FileName} {versionProcess.StartInfo.Arguments}", Priority.Command));

            // Rozpoczęcie procesu
            versionProcess.Start();

            string? line;

            // Czytaj aż do końca strumienia
            while (versionProcess.StandardOutput.EndOfStream == false)
            {
                line = versionProcess.StandardOutput.ReadLine();

                if (line == null)
                {
                    continue;
                }

                OnConsoleLine(new ConsoleLineEventArgs(line, Priority.Output));
            }

            // Oczekiwanie na zakończenie procesu
            versionProcess.WaitForExit();

            int exitCode = versionProcess.ExitCode;

            // Zamykanie procesu
            versionProcess.Close();

            // Sprawdzenie, czy nastąpił błąd
            if (exitCode != 0)
            {
                return exitCode;
            }

            return 0;
        }

        /// <summary>
        /// Publiczna metoda dodawania klipów do kolejki
        /// </summary>
        /// <param name="clipFilePath"></param>
        /// <param name="clipFileName"></param>
        public void AddClip(int clipID, string clipFilePath)
        {
            // Dodaj nowy klip do kolejki
            ThumbnailQueue.Enqueue(new Clip(clipID, clipFilePath));

            // Obudź kolejkę
            Task.Run(() => OnThumbnailQueueClipAdded(new EventArgs()));
        }

        private void FFmpeg_ThumnailQueueClipAdded(object? sender, EventArgs e)
        {

            if (ThumbnailProcessActive)
            {
                // Jeśli proces jest uruchomiony, nie uruchamiaj kolejnego
                return;
            }

            // Zaznacz, że proces jest aktywny
            ThumbnailProcessActive = true;

            // Wyczyść całą kolejkę
            while (ThumbnailQueue.Count > 0)
            {
                OnConsoleLine(new ConsoleLineEventArgs($"Kolejka ThumbnailQueue ma {ThumbnailQueue.Count} elementy.", Priority.Info));

                Clip clip;

                try
                {
                    clip = ThumbnailQueue.Dequeue();
                }
                catch
                {
                    continue;
                }

                CreateThumbnail(clip);
            }

            OnConsoleLine(new ConsoleLineEventArgs($"Kolejka ThumbnailQueue jest pusta.", Priority.Info));

            ThumbnailProcessActive = false;
            
        }

        /// <summary>
        /// Metoda do czyszczenia kolejki ThumbnailQueue
        /// </summary>
        /// <param name="clip"></param>
        private void CreateThumbnail(Clip clip)
        {
            // Konsola
            OnConsoleLine(new ConsoleLineEventArgs($"Tworzenie miniatury dla pliku \"{clip.FilePath}\"..."));

            // Ścieżka do miniatury (w folderze wyjściowym)
            string thumbnailFilePath = $"{OutputFolder}\\{Path.GetFileNameWithoutExtension(clip.FilePath)}.png";

            int exitCode = FFmpegCreateThumbnail(clip.ClipID, clip.FilePath, thumbnailFilePath);

            if (exitCode != 0)
            {
                // Informacja dla okna głównego
                OnFFmpegError(new FFmpegErrorEventArgs(clip.ClipID));

                // Konsola
                OnConsoleLine(new ConsoleLineEventArgs($"Nie można utworzyć miniatury dla pliku \"{clip.FilePath}\" (FFmpeg exit code: {exitCode})", Priority.Error));

                return;
            }

            OnConsoleLine(new ConsoleLineEventArgs($"Utworzono miniaturę dla pliku \"{clip.FilePath}\"."));

            // Informacja dla okna głównego
            OnThumbnailCreated(new ThumbnailCreatedEventArgs(clip.ClipID, thumbnailFilePath));

            // Dodaj do kolejki video
            VideoQueue.Enqueue(clip);

            // Obudź kolejkę VideoQueue
            Task.Run(() => OnVideoQueueClipAdded(new EventArgs()));
            
        }

        public int FFmpegCreateThumbnail(int clipID, string inputFilePath, string thumbnailFilePath)
        {
            // Proces FFmpeg do tworzenia miniatury
            Process thumbnailProcess = new Process();

            string command = $"-y -i \"{inputFilePath}\" -s 640x360 -frames:v 1 -update true \"{thumbnailFilePath}\"";

            thumbnailProcess.StartInfo.FileName = "ffmpeg.exe";
            thumbnailProcess.StartInfo.Arguments = command;
            thumbnailProcess.StartInfo.UseShellExecute = false;
            thumbnailProcess.StartInfo.CreateNoWindow = true;
            thumbnailProcess.StartInfo.RedirectStandardOutput = true;

            // Wysłanie komendy do konsoli
            OnConsoleLine(new ConsoleLineEventArgs($"{thumbnailProcess.StartInfo.FileName} {thumbnailProcess.StartInfo.Arguments}", Priority.Command));

            // Rozpoczęcie procesu
            thumbnailProcess.Start();

            // Odczytania linia
            string? line;

            // Czytaj aż do końca strumienia
            while (thumbnailProcess.StandardOutput.EndOfStream == false)
            {
                line = thumbnailProcess.StandardOutput.ReadLine();

                if(line == null)
                {
                    continue;
                }

                OnConsoleLine(new ConsoleLineEventArgs(line, Priority.Output));
            }

            // Oczekiwanie na zakończenie procesu
            thumbnailProcess.WaitForExit();

            int exitCode = thumbnailProcess.ExitCode;

            // Zamykanie procesu
            thumbnailProcess.Close();

            // Sprawdzenie, czy nastąpił błąd
            if (exitCode != 0)
            {
                return exitCode;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpeg_VideoQueueClipAdded(object? sender, EventArgs e)
        {
            if (VideoProcessActive)
            {
                // Jeśli proces jest uruchomiony, nie uruchamiaj kolejnego
                return;
            }

            VideoProcessActive = true;

            // Wyczyść całą kolejkę
            while (VideoQueue.Count > 0)
            {
                OnConsoleLine(new ConsoleLineEventArgs($"Kolejka VideoQueue ma {VideoQueue.Count} elementy.", Priority.Info));

                Clip clip;

                try
                {
                    clip = VideoQueue.Dequeue();
                }

                catch
                {
                    continue;
                }

                CreateVideo(clip);
            }

            OnConsoleLine(new ConsoleLineEventArgs($"Kolejka VideoQueue jest pusta.", Priority.Info));

            VideoProcessActive = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipID"></param>
        /// <param name="clipFilePath"></param>
        private void CreateVideo(Clip clip)
        { 
            if (clip.ClipID < 0)
            {
                return;
            }

            if (clip.FilePath == null || clip.FilePath == string.Empty)
            {
                return;
            }

            OnConsoleLine(new ConsoleLineEventArgs($"Tworzenie video dla pliku \"{clip.FilePath}\"..."));

            

            // Nazwa pliku po zmianie rozszerzenia na ".mp4"
            string clipFileNameWihoutExtension = Path.GetFileNameWithoutExtension(clip.FilePath);

            string outputFileName = $"{clipFileNameWihoutExtension} ({ResolutionName}, {FrameRate} FPS).mp4";
            string outputFilePath = $"{OutputFolder}\\{outputFileName}";
            string thumbnailFilePath = $"{OutputFolder}\\{clipFileNameWihoutExtension}.png";

            // Dla okna głównego
            OnConversionStarted(new ConversionStartedEventArgs(clip.ClipID, Path.GetFileName(clip.FilePath), thumbnailFilePath));

            // Kod
            int exitCode = 0;

            // Liczba ramek
            int frameCount;

            // Zliacznie ramek
            exitCode = FFprobeCountFrames(clip.FilePath, out frameCount);

            if(exitCode != 0)
            {
                OnFFmpegError(new FFmpegErrorEventArgs(clip.ClipID));

                OnConsoleLine(new ConsoleLineEventArgs($"Nie można określić liczby ramek pliku \"{clip.FilePath}\" (FFprobe exit code: {exitCode})", Priority.Error));

                return;
            }

            // Tworzenie video
            exitCode = FFmpegCreateVideo(clip.ClipID, clip.FilePath, outputFilePath, frameCount);

            if (exitCode != 0)
            {
                OnFFmpegError(new FFmpegErrorEventArgs(clip.ClipID));

                OnConsoleLine(new ConsoleLineEventArgs($"Nie można utworzyć video dla pliku \"{clip.FilePath}\" (FFmpeg exit code: {exitCode})", Priority.Error));

                return;
            }

            OnConsoleLine(new ConsoleLineEventArgs($"Utworzono video dla pliku \"{clip.FilePath}\"."));

            OnVideoCreated(new VideoCreatedEventArgs(clip.ClipID, outputFilePath, frameCount));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="frameCount"></param>
        /// <returns></returns>
        public int FFprobeCountFrames(string inputFile, out int frameCount)
        {
            // Proces FFprobe do liczenia klatek filmu
            Process countProcess = new Process();
            
            string command = $"-v error -select_streams v:0 -count_packets -show_entries stream=nb_read_packets -of csv=p=0 \"{inputFile}\"";

            countProcess.StartInfo.FileName = "ffprobe.exe";
            countProcess.StartInfo.Arguments = command;
            countProcess.StartInfo.UseShellExecute = false;
            countProcess.StartInfo.CreateNoWindow = true;

            // Włącz logowanie wyjścia
            countProcess.StartInfo.RedirectStandardOutput = true;

            // Wysłanie komendy do konsoli
            OnConsoleLine(new ConsoleLineEventArgs($"{countProcess.StartInfo.FileName} {countProcess.StartInfo.Arguments}", Priority.Command));

            // Rozpoczęcie procesu
            countProcess.Start();

            // Odczytania linia
            string? line;

            // Liczba klatek
            frameCount = 0;

            // Czytaj aż do końca strumienia
            while (countProcess.StandardOutput.EndOfStream == false)
            {
                line = countProcess.StandardOutput.ReadLine();

                if (line == null)
                {
                    continue;
                }

                OnConsoleLine(new ConsoleLineEventArgs(line, Priority.Output));

                try
                {
                    frameCount = Convert.ToInt32(line);
                }
                catch
                {
                    return -1;
                }

            }
            
            // Oczekiwanie na zakończenie procesu
            countProcess.WaitForExit();

            int exitCode = countProcess.ExitCode;

            // Zamykanie procesu
            countProcess.Close();

            // Sprawdzenie, czy nastąpił błąd
            if (exitCode != 0)
            {
                return exitCode;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipID"></param>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="frameCount"></param>
        /// <returns></returns>
        public int FFmpegCreateVideo(int clipID, string inputFilePath, string outputFilePath, int frameCount)
        {
            // Proces FFmpeg
            Process videoProcess = new Process();

            string command = $"-y -progress pipe:1 -i \"{inputFilePath}\" -r {FrameRate} -s {Resolution} -c:v {Encoder} -maxrate {MaxVideoBitrate}k -c:a copy \"{outputFilePath}\"";

            videoProcess.StartInfo.FileName = "ffmpeg.exe";
            videoProcess.StartInfo.Arguments = command;
            videoProcess.StartInfo.UseShellExecute = false;
            videoProcess.StartInfo.CreateNoWindow = true;
            videoProcess.StartInfo.RedirectStandardOutput = true;

            // Wysłanie komendy do konsoli
            OnConsoleLine(new ConsoleLineEventArgs($"{videoProcess.StartInfo.FileName} {videoProcess.StartInfo.Arguments}", Priority.Command));

            // Rozpoczęcie procesu
            videoProcess.Start();

            // Odczytania linia
            string? line;

            // Czytaj aż do końca strumienia
            while (videoProcess.StandardOutput.EndOfStream == false)
            {
                line = videoProcess.StandardOutput.ReadLine();

                if (line == null)
                {
                    continue;
                }

                //OnConsoleLine(new ConsoleLineEventArgs(line, Priority.Output));

                // Rodziel nazwę i wartość
                string[] strings = line.Split('=');

                // Jeśli istnieje nazwa i wartość
                if (strings.Length == 2)
                {
                    // Odczytaj nawzę parametru
                    string name = strings[0];

                    // Jeśli parametr to numer klatki
                    if (name == "frame")
                    {
                        // Odczytaj wartość parametru
                        string value = strings[1];

                        // Zmienna na numer klatki
                        int frame = 0;

                        // Spróbuj przekowertować tekst na liczbę
                        try
                        {
                            frame = Convert.ToInt32(value);
                        }
                        catch
                        {
                            return -1;
                        }

                        // Zapal Event Progress
                        OnProgressChanged(new ProgressChangedEventArgs(clipID, frame, frameCount));
                    }
                }
            }

            // Oczekiwanie na zakończenie procesu
            videoProcess.WaitForExit();

            int exitCode = videoProcess.ExitCode;

            // Zamykanie procesu
            videoProcess.Close();

            // Sprawdzenie, czy nastąpił błąd
            if (exitCode != 0)
            {
                return exitCode;
            }

            return 0;
        }
    }
}