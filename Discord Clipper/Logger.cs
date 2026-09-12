namespace DiscordClipper
{
    internal static class Logger
    {
        /// <summary>
        /// Priorytet logu
        /// </summary>
        public enum Priority
        {
            Error = 1,
            Warning = 2,
            Info = 3,
            Command = 4,
            Output = 5
        }

        /// <summary>
        /// Pojedynczy wpis logu
        /// </summary>
        public class LogData
        {
            public DateTime DateTime { get; set; }
            public string Message { get; set; }            
            public Priority Priority { get; set; }

            public LogData(string message, Priority priority)
            {
                DateTime = DateTime.Now;
                Message = message;
                Priority = priority;
            }

            public override string ToString()
            {
                return $"[{DateTime:yyyy-MM-dd HH:mm:ss.fff}] [{Priority}] {Message}";
            }
        }

        /// <summary>
        /// Kolejka przechowująca logi
        /// </summary>
        private static Queue<LogData> LogQueue = new Queue<LogData>();

        /// <summary>
        /// Rozmiar kolejki logów
        /// </summary>
        public static int QueueSize = 500;

        /// <summary>
        /// Ścieżka do pliku logów
        /// </summary>
        private static readonly string LogFilePath = $"{Application.UserAppDataPath}\\Discord Clipper.log";

        private static StreamWriter? LogFileStreamWriter;

        public static event EventHandler<LogData>? LogAdded;

        private static void OnLogAdded(LogData logData)
        {
            LogAdded?.Invoke(null, logData);    
        }


        public static void InitializeLogFile()
        {
            string productVersion = Application.ProductVersion;
            
            string[] versionParts = productVersion.Split('+');

            if (versionParts.Length > 1)
            {
                productVersion = versionParts[0];
            }

            StreamWriter streamWriter = new StreamWriter(LogFilePath, false);
            streamWriter.WriteLine($"**************************************************");
            streamWriter.WriteLine($"* Discord Clipper v{productVersion,-30}*");
            streamWriter.WriteLine($"* {DateTime.Now,-47:yyyy-MM-dd HH:mm:ss}*");
            streamWriter.WriteLine($"**************************************************");

            streamWriter.Flush();
            streamWriter.Close();
        }

        private static void AppendLogToFile(LogData logData)
        {
            StreamWriter streamWriter = new StreamWriter(LogFilePath, true);
            streamWriter.WriteLine(logData.ToString());

            streamWriter.Flush();
            streamWriter.Close();
        }

        /// <summary>
        /// Dodaje nowy log do kolejki. Jeśli kolejka osiągnie maksymalny rozmiar, najstarszy log zostanie usunięty.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="priority"></param>
        public static void WriteLine(string message, Priority priority)
        {
            // Usuń z kolejki nadmiarowe logi
            while (LogQueue.Count >= QueueSize)
            {
                LogQueue.Dequeue();
            }

            LogData logData = new LogData(message, priority);

            // Dodaj nowy log do kolejki
            LogQueue.Enqueue(logData);

            OnLogAdded(logData);

            // Dodaj nowy log do pliku
            AppendLogToFile(logData);
        }

        /// <summary>
        /// Pobiera wszystkie logi w kolejce jako tablicę LogData.
        /// </summary>
        /// <returns></returns>
        public static LogData[] GetLogs()
        {
            return LogQueue.ToArray();
        }

        public static void Clear()
        {
            LogQueue.Clear();
        }
    }
}
