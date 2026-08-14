namespace DiscordClipper
{
    public partial class FormConsole : Form
    {
        /// <summary>
        /// Formularz okna konsoli
        /// </summary>
        public FormConsole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Klasa argumentów zdarzenia dla konsoli
        /// </summary>
        public class ConsoleEventArgs : EventArgs
        {
            public DateTime DateTime { get; set; } = DateTime.Now;
            public string Sender { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public Priority MessagePriority { get; set; } = Priority.Info;

            public enum Priority
            {
                Info,
                Warning,
                Error
            }

            public ConsoleEventArgs(string message)
            {
                Message = message;
            }

            public ConsoleEventArgs(string message, Priority messagePriority)
            {
                Message = message;
                MessagePriority = messagePriority;
            }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(Sender))
                {
                    return $"[{DateTime:yyyy-MM-dd HH:mm:ss}] {Message}";
                }

                return $"[{DateTime:yyyy-MM-dd HH:mm:ss}] {Sender}: {Message}";
            }
        }

        /// <summary>
        /// Publiczna metoda do pisania po konsoli
        /// </summary>
        /// <param name="e"></param>
        public void WriteLine(ConsoleEventArgs e)
        {
            switch (e.MessagePriority)
            {
                case ConsoleEventArgs.Priority.Warning:
                    WriteWarning(e.ToString());
                    break;

                case ConsoleEventArgs.Priority.Error:
                    WriteError(e.ToString());
                    break;

                default:
                    WriteLine(e.ToString());
                    break;
            }
        }

        /// <summary>
        /// Prywatna metoda do pisania po konsoli
        /// </summary>
        /// <param name="text"></param>
        private void Write(string text)
        {
            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action<string>(Write), text);
            }
            else
            {
                richTextBoxConsole.AppendText(text);

                richTextBoxConsole.ScrollToCaret();
            }
        }

        /// <summary>
        /// Prywatna metoda do pisania po konsoli w kolorze
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void Write(string text, Color color)
        {
            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action<string, Color>(Write), text, color);
            }
            else
            {
                // Początek zaznaczenia
                int selectionStart = richTextBoxConsole.TextLength;

                // Wstawianie tekstu
                Write(text);

                // Koniec zaznaczenia
                int selectionEnd = richTextBoxConsole.TextLength;

                richTextBoxConsole.Select(selectionStart, selectionEnd);

                // Stosowanie koloru
                richTextBoxConsole.SelectionColor = color;

                // Odznaczenie całego tekstu
                richTextBoxConsole.DeselectAll();
            }
        }

        /// <summary>
        /// Prywatna metoda do pisania po konsoli
        /// </summary>
        /// <param name="text"></param>
        private void WriteLine(string text)
        {
            Write(text + Environment.NewLine);
        }

        /// <summary>
        /// Prywatna metoda do pisania po konsoli w kolorze
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void WriteLine(string text, Color color)
        {
            Write(text + Environment.NewLine, color);
        }

        /// <summary>
        /// Predefiniowana funkcja dla błędów
        /// </summary>
        /// <param name="text"></param>
        private void WriteError(string text)
        {
            WriteLine(text, Color.Red);
        }

        /// <summary>
        /// Predefiniowana funkcja dla ostrzerzeń
        /// </summary>
        /// <param name="text"></param>
        private void WriteWarning(string text)
        {
            WriteLine(text, Color.Orange);
        }

        /// <summary>
        /// Czyszczenie konsoli formularza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearConsole_Click(object sender, EventArgs e)
        {
            // Czyszczenie tekstu
            richTextBoxConsole.Clear();
        }

        /// <summary>
        /// Zmiana rozmiaru czcionki formularza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownConsoleFontSize_ValueChanged(object sender, EventArgs e)
        {
            // Zaznaczanie całego tekstu
            richTextBoxConsole.SelectAll();

            // Stosowanie koloru
            richTextBoxConsole.SelectionFont = new Font(richTextBoxConsole.Font.FontFamily, (float)numericUpDownConsoleFontSize.Value);

            // Odznaczenie całego tekstu
            richTextBoxConsole.DeselectAll();
        }
    }
}
