using System.Net.Http.Headers;
using static DiscordClipper.FFmpeg;
using static DiscordClipper.FormConsole;
using static DiscordClipper.Logger;

namespace DiscordClipper
{
    internal class Discord
    {
        public event EventHandler? DiscordQueueClipAdded;
        public event EventHandler<ClipSentEventArgs>? ClipSent;
        public event EventHandler<DiscordErrorEventArgs>? DiscordError;

        public class ClipSentEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public string ClipFileName { get; set; } = string.Empty;
            public ClipSentEventArgs(int clipID, string clipFileName)
            {
                ClipID = clipID;
                ClipFileName = clipFileName;
            }
        }

        public class DiscordErrorEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public string ErrorMessage { get; set; } = string.Empty;

            public DiscordErrorEventArgs(int clipID)
            {
                ClipID = clipID;
            }
        }

        protected virtual void OnVideoQueueClipAdded(EventArgs e)
        {
            DiscordQueueClipAdded?.Invoke(this, e);
        }

        protected virtual void OnClipSent(ClipSentEventArgs e)
        {
            ClipSent?.Invoke(this, e);
        }

        protected virtual void OnDiscordError(DiscordErrorEventArgs e)
        {
            DiscordError?.Invoke(this, e);
        }

        private Queue<Clip> DiscordQueue = new Queue<Clip>();
        private Mutex DiscordQueueMutex = new Mutex();

        private bool DiscordQueueAwake = false;
        private Mutex DiscordQueueAwakeMutex = new Mutex();


        public string WebhookURL = string.Empty;

        public Discord()
        {
            DiscordQueueClipAdded += Discord_VideoQueueClipAdded;
        }

        public void AddClip(int clipID, string clipFilePath)
        {
            Logger.WriteLine($"Dodawanie klipu \"{clipFilePath}\" do kolejki DiscordQueue...", Priority.Info);

            // Dodaj nowy klip do kolejki
            DiscordQueueMutex.WaitOne();
            DiscordQueue.Enqueue(new Clip(clipID, clipFilePath));
            DiscordQueueMutex.ReleaseMutex();

            // Obudź kolejkę
            Task.Run(() => OnVideoQueueClipAdded(new EventArgs()));

            Logger.WriteLine($"Dodawanie klipu \"{clipFilePath}\" do kolejki DiscordQueue...", Priority.Info);
        }

        private void Discord_VideoQueueClipAdded(object? sender, EventArgs e)
        {
            // Sprawdź, czy kolejka jest obudzona
            DiscordQueueAwakeMutex.WaitOne();
            if (DiscordQueueAwake)
            {
                // Kolejka jest już obudzona, zwolnij mutex i wyjdź z metody
                DiscordQueueAwakeMutex.ReleaseMutex();
                Logger.WriteLine($"Kolejka DiscordQueue jest już aktywna.", Priority.Info);
                return;
            }

            // Kolejka się budzi
            DiscordQueueAwake = true;
            DiscordQueueAwakeMutex.ReleaseMutex();

            Logger.WriteLine($"Kolejka DiscordQueue została aktywowana.", Priority.Info);

            // Liczba elementów w kolejce
            DiscordQueueMutex.WaitOne();
            int discordQueueCount = DiscordQueue.Count;
            DiscordQueueMutex.ReleaseMutex();

            while (discordQueueCount > 0)
            {
                Logger.WriteLine($"Liczba elementów w kolejce DiscordQueue: {discordQueueCount}", Priority.Info);

                DiscordQueueMutex.WaitOne();
                Clip clip = DiscordQueue.Dequeue();
                DiscordQueueMutex.ReleaseMutex();

                SendVideo(clip);

                DiscordQueueMutex.WaitOne();
                discordQueueCount = DiscordQueue.Count;
                DiscordQueueMutex.ReleaseMutex();
            }

            Logger.WriteLine($"Kolejka DiscordQueue jest pusta.", Priority.Info);

            // Kolejka jest wyłączana
            DiscordQueueAwakeMutex.WaitOne();
            DiscordQueueAwake = false;
            DiscordQueueAwakeMutex.ReleaseMutex();

            Logger.WriteLine($"Kolejka DiscordQueue została wyłączona.", Priority.Info);
        }

        public async void SendVideo(Clip clip)
        {
            if (WebhookURL == null || WebhookURL == string.Empty)
            {
                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));

                Logger.WriteLine($"Obiekt Webhook ma wartość null.", Priority.Error);

                return;
            }

            if (!File.Exists(clip.FilePath))
            {
                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));

                Logger.WriteLine($"Klip \"{clip.FilePath}\" nie istnieje.", Priority.Error);

                return;
            }

            using HttpClient client = new HttpClient();

            using MultipartFormDataContent form = new MultipartFormDataContent();

            // Wiadomość
            form.Add(new StringContent($":clapper: **{Path.GetFileName(clip.FilePath)}**"), "content");

            // Plik

            byte[] fileBytes;

            try
            {
                fileBytes = await File.ReadAllBytesAsync(clip.FilePath);
            }
            catch(Exception ex)
            {
                Logger.WriteLine($"Błąd podczas odczytywania pliku \"{clip.FilePath}\": {ex.Message}", Priority.Error);
                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                return;
            }

            ByteArrayContent fileContent = new ByteArrayContent(fileBytes);

            fileContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");

            form.Add(fileContent, "files[0]", Path.GetFileName(clip.FilePath));

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync(WebhookURL, form);
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Błąd podczas wysyłania pliku \"{clip.FilePath}\" do Discord: {ex.Message}", Priority.Error);
                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                OnClipSent(new ClipSentEventArgs(clip.ClipID, clip.FilePath));

                Logger.WriteLine($"Klip \"{clip.FilePath}\" został wysłany.", Priority.Info);
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();

                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                Logger.WriteLine($"Plik \"{clip.FilePath}\" nie został wysłany - HTTP {response.StatusCode}: {error}", Priority.Error);
            }
        }
    }
}
