using System.Net.Http.Headers;
using static DiscordClipper.FFmpeg;

namespace DiscordClipper
{
    internal class Discord
    {
        public Discord()
        {

        }

        public event EventHandler<ClipSentEventArgs>? ClipSent;

        public class ClipSentEventArgs : EventArgs
        {
            public int ClipID { get; set; } = 0;
            public string ClipFileName { get; set; } = string.Empty;
        }

        protected virtual void OnClipSent(ClipSentEventArgs e)
        {
            ClipSent?.Invoke(this, e);
        }

        public event EventHandler<DiscordErrorEventArgs>? DiscordError;

        public class DiscordErrorEventArgs : EventArgs
        {
            public string ClipFileName { get; set; } = string.Empty;
            public string ErrorMessage { get; set; } = string.Empty;
        }

        protected virtual void OnDiscordError(DiscordErrorEventArgs e)
        {
            DiscordError?.Invoke(this, e);
        }

        public string WebhookURL = string.Empty;

        public async void Send(string clipFileName, string outputFilePath, string outputFileName)
        {
            if (WebhookURL == null || WebhookURL == string.Empty)
            {
                DiscordErrorEventArgs err = new DiscordErrorEventArgs();
                err.ClipFileName = clipFileName;
                err.ErrorMessage = "Discord: Webhook jest pusty.";
                OnDiscordError(err);
                return;
            }

            try
            {
                if (!File.Exists(outputFilePath))
                {
                    DiscordErrorEventArgs err = new DiscordErrorEventArgs();
                    err.ClipFileName = clipFileName;
                    err.ErrorMessage = "Discord: Klip nie istnieje.";
                    OnDiscordError(err);
                    return;
                }

                using HttpClient client = new();

                using MultipartFormDataContent form = new();

                // Wiadomość
                //form.Add(new StringContent("Film wysłany z aplikacji Windows Forms"), "content");

                // Plik
                byte[] fileBytes = await File.ReadAllBytesAsync(outputFilePath);

                ByteArrayContent fileContent = new(fileBytes);
                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue("video/mp4");

                form.Add(fileContent, "files[0]", outputFileName);

                HttpResponseMessage response =
                    await client.PostAsync(WebhookURL, form);

                if (response.IsSuccessStatusCode)
                {
                    ClipSentEventArgs e = new ClipSentEventArgs();
                    e.ClipFileName = clipFileName;
                    OnClipSent(e);
                    //MessageBox.Show("Film został wysłany.");
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();

                    DiscordErrorEventArgs err = new DiscordErrorEventArgs();
                    err.ClipFileName = clipFileName;
                    err.ErrorMessage = "HTTP {response.StatusCode}: {error}.";
                    OnDiscordError(err);

                    //MessageBox.Show(
                    //    $"Błąd:\n{response.StatusCode}\n\n{error}");
                }
            }
            catch (Exception ex)
            {
                DiscordErrorEventArgs err = new DiscordErrorEventArgs();
                err.ClipFileName = clipFileName;
                err.ErrorMessage = ex.Message;
                OnDiscordError(err);
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
