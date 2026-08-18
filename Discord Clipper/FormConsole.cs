namespace DiscordClipper
{
    public partial class FormConsole : Form
    {
        /// <summary>
        /// Priorytet
        /// </summary>
        public enum Priority
        {
            Info,
            Command,
            Output,
            Error
        }

        public class ConsoleLineEventArgs : EventArgs
        {
            public DateTime DateTime = DateTime.Now;
            public string Sender = string.Empty;
            public string Message = string.Empty;
            public Priority Priority = Priority.Info;
            public ConsoleLineEventArgs(string message)
            {
                Message = message;
            }

            public ConsoleLineEventArgs(string message, Priority priority)
            {
                Message = message;
                Priority = priority;
            }

            public override string ToString()
            {
                if(Sender == null || Sender == string.Empty)
                {
                    return $"[{DateTime:yyyy-MM-dd HH:mm:ss}] {Message}";
                }

                return $"[{DateTime:yyyy-MM-dd HH:mm:ss}] {Sender} {Message}";
            }
        }

        /// <summary>
        /// Formularz okna konsoli
        /// </summary>
        public FormConsole()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Publiczna metoda do pisania po konsoli
        /// </summary>
        /// <param name="e"></param>
        public void WriteLine(ConsoleLineEventArgs consoleLineEventArgs)
        {
            switch (consoleLineEventArgs.Priority)
            {
                case Priority.Command:
                    WriteCommand(consoleLineEventArgs.ToString());
                    break;

                case Priority.Output:
                    WriteOutput(consoleLineEventArgs.ToString());
                    break;

                case Priority.Error:
                    WriteError(consoleLineEventArgs.ToString());
                    break;

                default:
                    WriteLine(consoleLineEventArgs.ToString());
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
        /// Predefiniowana funkcja dla ostrzerzeń
        /// </summary>
        /// <param name="text"></param>
        private void WriteCommand(string text)
        {
            WriteLine(text, Color.Blue);
        }

        /// <summary>
        /// Predefiniowana funkcja dla ostrzerzeń
        /// </summary>
        /// <param name="text"></param>
        private void WriteOutput(string text)
        {
            WriteLine(text, Color.DarkBlue);
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

        private void FormConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
