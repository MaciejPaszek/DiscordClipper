using System.Net.Http.Headers;
using static DiscordClipper.FFmpeg;
using static DiscordClipper.FormConsole;

namespace DiscordClipper
{
    internal class Discord
    {
        public event EventHandler<ConsoleLineEventArgs>? ConsoleLine;
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

        protected virtual void OnConsoleLine(ConsoleLineEventArgs e)
        {
            ConsoleLine?.Invoke(this, e);
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
        /// <summary>
        /// Flaga
        /// </summary>
        private bool DiscordProcessActive = false;

        public string WebhookURL = string.Empty;

        public Discord()
        {
            DiscordQueueClipAdded += Discord_VideoQueueClipAdded;
        }

        public void AddClip(int clipID, string clipFilePath)
        {
            // Dodaj nowy klip do kolejki
            DiscordQueue.Enqueue(new Clip(clipID, clipFilePath));

            // Obudź kolejkę
            Task.Run(() => OnVideoQueueClipAdded(new EventArgs()));
        }

        private void Discord_VideoQueueClipAdded(object? sender, EventArgs e)
        {
            if (DiscordProcessActive)
            {
                // Jeśli proces jest uruchomiony, nie uruchamiaj kolejnego
                return;
            }

            DiscordProcessActive = true;

            // Wyczyść całą kolejkę
            while (DiscordQueue.Count > 0)
            {
                OnConsoleLine(new ConsoleLineEventArgs($"Kolejka DiscordQueue ma {DiscordQueue.Count} elementy.", Priority.Info));

                Clip clip;

                try
                {
                    clip = DiscordQueue.Dequeue();
                }

                catch
                {
                    continue;
                }

                SendVideo(clip);
            }

            OnConsoleLine(new ConsoleLineEventArgs($"Kolejka DiscordQueue jest pusta.", Priority.Info));

            DiscordProcessActive = false;
        }

        public async void SendVideo(Clip clip)
        {
            if (WebhookURL == null || WebhookURL == string.Empty)
            {
                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                OnConsoleLine(new ConsoleLineEventArgs($"Webhook jest pusty.", Priority.Error));

                return;
            }

            try
            {
                if (!File.Exists(clip.FilePath))
                {
                    OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                    OnConsoleLine(new ConsoleLineEventArgs($"Klip nie istnieje.", Priority.Error));
                    return;
                }

                using HttpClient client = new();

                using MultipartFormDataContent form = new();

                // Wiadomość
                form.Add(new StringContent(Path.GetFileName(clip.FilePath)), "content");

                // Plik
                byte[] fileBytes = await File.ReadAllBytesAsync(clip.FilePath);

                ByteArrayContent fileContent = new(fileBytes);
                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue("video/mp4");

                form.Add(fileContent, "files[0]", Path.GetFileName(clip.FilePath));

                HttpResponseMessage response =
                    await client.PostAsync(WebhookURL, form);

                if (response.IsSuccessStatusCode)
                {
                    OnClipSent(new ClipSentEventArgs(clip.ClipID, clip.FilePath));

                    OnConsoleLine(new ConsoleLineEventArgs($"Klip {Path.GetFileName(clip.FilePath)} został wysłany."));

                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();

                    OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                    OnConsoleLine(new ConsoleLineEventArgs($"Klip {Path.GetFileName(clip.FilePath)} nie został wysłany - HTTP {response.StatusCode}: {error}.", Priority.Error));
                }
            }
            catch (Exception ex)
            {
                OnDiscordError(new DiscordErrorEventArgs(clip.ClipID));
                OnConsoleLine(new ConsoleLineEventArgs($"Klip {Path.GetFileName(clip.FilePath)} - {ex.Message}.", Priority.Error));
            }
        }
    }
}
