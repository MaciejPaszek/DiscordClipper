using System.Diagnostics.Eventing.Reader;
using static DiscordClipper.Logger;

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

            //
            Logger.LogAdded += Logger_LogAdded;

            // Wczystaj poprzednie logi
            ReadLogger();
        }

        private void Logger_LogAdded(object? sender, LogData e)
        {
            WriteLine(e);
        }

        private void ReadLogger()
        {
            Logger.LogData[] logDataArray = Logger.GetLogs();

            foreach (Logger.LogData logData in logDataArray)
            {
                WriteLine(logData);
            }
        }

        /// <summary>
        /// Publiczna metoda do pisania po konsoli
        /// </summary>
        /// <param name="e"></param>
        private void WriteLine(Logger.LogData consoleLineEventArgs)
        {
            switch (consoleLineEventArgs.Priority)
            {
                case Logger.Priority.Command:
                    WriteCommand(consoleLineEventArgs.ToString());
                    break;

                case Logger.Priority.Output:
                    WriteOutput(consoleLineEventArgs.ToString());
                    break;

                case Logger.Priority.Error:
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
            if (richTextBoxConsole == null)
            {
                return;
            }

            if (richTextBoxConsole.InvokeRequired)
            {
                richTextBoxConsole.Invoke(new Action<string>(Write), text);
            }
            else
            {
                richTextBoxConsole.AppendText(text);

                if (checkBoxAutoscroll.Checked)
                {
                    richTextBoxConsole.ScrollToCaret();
                }
            }
        }

        /// <summary>
        /// Prywatna metoda do pisania po konsoli w kolorze
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void Write(string text, Color color)
        {
            if (richTextBoxConsole == null)
            {
                return;
            }

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
            if (Application.ColorMode == SystemColorMode.Classic)
            {
                WriteLine(text, Color.Blue);
            }
            else
            {
                WriteLine(text, Color.LightBlue);
            }
        }

        /// <summary>
        /// Predefiniowana funkcja dla ostrzerzeń
        /// </summary>
        /// <param name="text"></param>
        private void WriteOutput(string text)
        {
            if (Application.ColorMode == SystemColorMode.Classic)
            {
                WriteLine(text, Color.Gray);
            }
            else
            {
                WriteLine(text, Color.DarkGray);
            }
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

        /// <summary>
        /// Kopiowanie zawartości konsoli do schowka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            string? text = richTextBoxConsole.Text;

            if (text == null)
            {
                return;
            }

            Clipboard.SetText(text, TextDataFormat.Text);
        }

        private void FormConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.LogAdded -= Logger_LogAdded;
        }

        private void RichTextBoxConsole_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get all lines
                var lines = richTextBoxConsole.Lines;

                // If over limit, remove oldest lines
                if (lines.Length > Logger.QueueSize)
                {
                    // Keep only the last MaxLines lines
                    lines = lines.Skip(lines.Length - Logger.QueueSize).ToArray();

                    // Temporarily detach event to avoid recursion
                    richTextBoxConsole.TextChanged -= RichTextBoxConsole_TextChanged;
                    richTextBoxConsole.Lines = lines;
                    richTextBoxConsole.SelectionStart = richTextBoxConsole.Text.Length; // Move caret to end
                    richTextBoxConsole.ScrollToCaret();
                    richTextBoxConsole.TextChanged += RichTextBoxConsole_TextChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error limiting lines: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
