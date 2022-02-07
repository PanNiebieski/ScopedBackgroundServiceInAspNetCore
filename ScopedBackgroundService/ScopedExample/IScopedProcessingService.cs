
internal interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}