namespace DiscordClipper
{
    internal static class Logger
    {
        public enum Priority
        {
            Error = 1,
            Warning = 2,
            Info = 3,
            Command = 4,
            Output = 5
        }

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

        private static Queue<LogData> LogQueue = new Queue<LogData>();

        public static int QueueSize = 500;

        private static string LogFilePath = $"{Application.UserAppDataPath}\\Discord Clipper.log";

        public static bool LogToFile = true;
        private static StreamWriter? LogFileStreamWriter;
        public static event EventHandler<LogData>? LogAdded;

        private static void OnLogAdded(LogData logData)
        {
            LogAdded?.Invoke(null, logData);    
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
            //StreamWriter writer = new StreamWriter(LogFilePath, true);
            //writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{priority}] {message}");
            //writer.Close();
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
