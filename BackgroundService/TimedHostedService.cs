



public class TimedHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer _timer = null!;

    public TimedHostedService(ILogger<TimedHostedService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}


public class WorkerHostedService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stopToken)
    {
        //Twój kod startujący zaczyna się tu
        while (!stopToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            Console.WriteLine("TEST");
        }
        //Posprzątaj po kodzie gdy przestanie działać
    }
}

public class WorkerHostedService2 : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stopToken)
    {
        //Twój kod startujący zaczyna się tu
        while (!stopToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, stopToken);
                Console.WriteLine("1000");
            }
            catch (OperationCanceledException)
            {
                return;
            }

        }
        //Posprzątaj po kodzie gdy przestanie działać
    }
}