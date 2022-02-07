using System.Text.Encodings.Web;

namespace ScopedBackgroundService.ScopedExample
{
    public class ScopedEmojiService : IScopedProcessingService
    {
        private Random random = new Random();

        private static List<string> _happySymbols = new()
        {
            "🔥",
            "😂",
            "😁",
            "🙏",
            "😎",
            "💪",
            "😘",
            "😍",
            "🤩",
            "🥰",
            "😉",
            "👍",
            "🥳"
        };

        private static List<string> _sadSymbols = new()
        {
            "😓",
            "😰",
            "😭",
            "😖",
            "😣",
            "😞",
            "😓",
            "😩",
            "😫",
            "😱",
            "😬",
            "👎",
            "🤢"
        };

        private static List<string> _emojis = new() { "🗣" };

        public string HTML => string.Join(
            string.Empty, _emojis.Select(e => HtmlEncoder.Default.Encode(e))
        );

        private string Output => string.Join(string.Empty, _emojis);

        private void AddEmoji(string emoji) => _emojis.Add(emoji);

        public void AddHappyEmoji()
        {
            int index = random.Next(0, _happySymbols.Count());
            AddEmoji(_happySymbols[index]);
        }
        public void AddSadEmoji()
        {
            int index = random.Next(0, _sadSymbols.Count());
            AddEmoji(_sadSymbols[index]);
        }


        public Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine($"{DateTime.Now:u}: Hello, {Output}");
                    await Task.Delay(1000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}
