using System.Text.Encodings.Web;

namespace ScopedBackgroundService.SingletonBCService
{
    public class WorkerEmoji : BackgroundService
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
            "👍"
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
            "👎"
        };

        private static List<string> _emojis = new() { "🗣" };

        public string HTML => string.Join(
            string.Empty, _emojis.Select(e => HtmlEncoder.Default.Encode(e))
        );

        private string Output => string.Join(string.Empty, _emojis);

        public void AddEmoji(string emoji) => _emojis.Add(emoji);

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



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
