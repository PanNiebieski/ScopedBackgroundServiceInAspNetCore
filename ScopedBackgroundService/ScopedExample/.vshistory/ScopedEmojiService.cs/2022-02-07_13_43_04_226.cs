namespace ScopedBackgroundService.ScopedExample
{
    public class ScopedEmojiService : IScopedProcessingService
    {
        public Task DoWork(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
