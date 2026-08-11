using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordClipper
{
    public class Settings
    {
        public string profileName = "settings";

        public int ColorMode = 0;

        public string InputFolder = string.Empty;
        public int InputFileFormat = 0;

        public string OutputFolder = string.Empty;
        public int OutputFileFormat = 1;
        public int Resolution = 2;
        public int FrameRate = 1;
        public int Encoder = 1;
        public int MaxVideoBitrate = 2600;

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

            // Jeśli plik ustawień nie istnieje, zwróć ustawienia domyślne
            if (!File.Exists(settingsFilePath))
            {
                return false;
            }

            StreamReader streamReader = File.OpenText(settingsFilePath);

            if (streamReader == null)
            {
                return false;
            }

            // Numer linii
            int i = 0;

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

                    if (name == "DiscordWebhook") { DiscordWebhook = value; }
                    if (name == "DiscordMode") { DiscordMode = Convert.ToInt32(value); }
                    if (name == "DiscordShortcut") { DiscordShortcut = value; }
                }
                catch
                {

                }

                i++;
            }

            // Zamykanie pliku
            streamReader.Close();
            streamReader.Dispose();

            return true;
        }
        public void Save()
        {
            string settingsFilePath = $"{Application.UserAppDataPath}\\{profileName}.txt";

            StreamWriter streamWriter = File.CreateText(settingsFilePath);

            streamWriter.WriteLine($"ColorMode        = {ColorMode.ToString()}");

            streamWriter.WriteLine($"InputFolder      = {InputFolder.ToString()}");
            streamWriter.WriteLine($"InputFileFormat  = {InputFileFormat.ToString()}");

            streamWriter.WriteLine($"OutputFolder     = {OutputFolder.ToString()}");
            streamWriter.WriteLine($"OutputFileFormat = {OutputFileFormat.ToString()}");
            streamWriter.WriteLine($"Resolution       = {Resolution.ToString()}");
            streamWriter.WriteLine($"FrameRate        = {FrameRate.ToString()}");
            streamWriter.WriteLine($"Encoder          = {Encoder.ToString()}");
            streamWriter.WriteLine($"MaxVideoBitrate  = {MaxVideoBitrate.ToString()}");

            streamWriter.WriteLine($"DiscordWebhook   = {DiscordWebhook.ToString()}");
            streamWriter.WriteLine($"DiscordMode      = {DiscordMode.ToString()}");
            streamWriter.WriteLine($"DiscordShortcut  = {DiscordShortcut.ToString()}");

            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
