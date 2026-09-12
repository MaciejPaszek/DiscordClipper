using static DiscordClipper.Logger;

namespace DiscordClipper
{
    public class Settings
    {
        public string profileName = "settings";

        public int ColorMode = 0;

        public string InputFolder = string.Empty;
        public int InputFileFormat = 0;

        public string OutputFolder = string.Empty;
        public int OutputFileFormat = 0;
        public int Resolution = 4;
        public int FrameRate = 1;
        public int Encoder = 3;
        public int VideoBitrateLimit = 2500;
        public bool VideoBitrateLimitEnabled = true;

        public string DiscordWebhook = string.Empty;
        public int DiscordMode = 0;
        public string DiscordShortcut = string.Empty;

        public Settings(string profileName)
        {
            this.profileName = profileName;
        }

        public bool Load()
        {
            string settingsFilePath = $"{Application.UserAppDataPath}\\{profileName}.txt";

            Logger.WriteLine($"Wczytywanie ustawień z pliku {settingsFilePath}...", Priority.Info);

            // Jeśli plik ustawień nie istnieje, zwróć ustawienia domyślne
            if (!File.Exists(settingsFilePath))
            {
                Logger.WriteLine($"Plik ustawień {settingsFilePath} nie istnieje.", Priority.Info);
                return false;
            }

            StreamReader streamReader = File.OpenText(settingsFilePath);

            if (streamReader == null)
            {
                Logger.WriteLine($"Obiekt streamReader ma wartość null.", Priority.Error);

                return false;
            }

            // Tekst linii
            string? line;

            // Czytaj koljene linie, aż do końca pliku
            while ((line = streamReader.ReadLine()) != null)
            {
                try
                {
                    string[] parts = line.Split('=');

                    // Pomiń błędne linie
                    if (parts.Length != 2)
                    {
                        Logger.WriteLine($"Błędna linia w pliku ustawień: {line}.", Priority.Warning);

                        continue;
                    }

                    string name = parts[0].Trim();
                    string value = parts[1].Trim();

                    if (name == "ColorMode") { ColorMode = Convert.ToInt32(value); }

                    if (name == "InputFolder") { InputFolder = value; }
                    if (name == "InputFileFormat") { InputFileFormat = Convert.ToInt32(value); }

                    if (name == "OutputFolder") { OutputFolder = value; }
                    if (name == "OutputFileFormat") { OutputFileFormat = Convert.ToInt32(value); }
                    if (name == "Resolution") { Resolution = Convert.ToInt32(value); }
                    if (name == "FrameRate") { FrameRate = Convert.ToInt32(value); }
                    if (name == "Encoder") { Encoder = Convert.ToInt32(value); }
                    if (name == "VideoBitrateLimit") { VideoBitrateLimit = Convert.ToInt32(value); }
                    if (name == "VideoBitrateLimitEnabled") { VideoBitrateLimitEnabled = Convert.ToBoolean(value); }

                    if (name == "DiscordWebhook") { DiscordWebhook = value; }
                    if (name == "DiscordMode") { DiscordMode = Convert.ToInt32(value); }
                    if (name == "DiscordShortcut") { DiscordShortcut = value; }
                }
                catch
                {
                    Logger.WriteLine($"Błąd konwersji linii ustawień: {line}.", Priority.Warning);
                }
            }

            // Zamykanie pliku
            streamReader.Close();
            streamReader.Dispose();

            return true;
        }
        public void Save()
        {
            string settingsFilePath = $"{Application.UserAppDataPath}\\{profileName}.txt";

            Logger.WriteLine($"Zapisywanie ustawień do pliku {settingsFilePath}...", Priority.Info);

            try
            {
                StreamWriter streamWriter = File.CreateText(settingsFilePath);

                streamWriter.WriteLine($"ColorMode        = {ColorMode.ToString()}");

                streamWriter.WriteLine($"InputFolder      = {InputFolder.ToString()}");
                streamWriter.WriteLine($"InputFileFormat  = {InputFileFormat.ToString()}");

                streamWriter.WriteLine($"OutputFolder     = {OutputFolder.ToString()}");
                streamWriter.WriteLine($"OutputFileFormat = {OutputFileFormat.ToString()}");
                streamWriter.WriteLine($"Resolution       = {Resolution.ToString()}");
                streamWriter.WriteLine($"FrameRate        = {FrameRate.ToString()}");
                streamWriter.WriteLine($"Encoder          = {Encoder.ToString()}");

                streamWriter.WriteLine($"VideoBitrateLimit         = {VideoBitrateLimit.ToString()}");
                streamWriter.WriteLine($"VideoBitrateLimitEnabled  = {VideoBitrateLimitEnabled.ToString()}");

                streamWriter.WriteLine($"DiscordWebhook   = {DiscordWebhook.ToString()}");
                streamWriter.WriteLine($"DiscordMode      = {DiscordMode.ToString()}");
                streamWriter.WriteLine($"DiscordShortcut  = {DiscordShortcut.ToString()}");

                streamWriter.Close();
                streamWriter.Dispose();
            }
            catch(Exception ex)
            {
                Logger.WriteLine($"Błąd podczas zapisywania ustawień do pliku {settingsFilePath}: {ex.Message}", Priority.Error);
            }
        }
    }
}
