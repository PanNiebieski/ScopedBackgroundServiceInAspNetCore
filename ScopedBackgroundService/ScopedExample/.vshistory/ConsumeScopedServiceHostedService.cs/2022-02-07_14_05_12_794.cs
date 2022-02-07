



using Microsoft.Extensions.DependencyInjection;

public class ConsumeScopedServiceHostedService : BackgroundService
{
    private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

    public ConsumeScopedServiceHostedService(IServiceProvider services,
        ILogger<ConsumeScopedServiceHostedService> logger)
    {
        Services = services;
        _logger = logger;
    }

    public IServiceProvider Services { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service running.");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWork(stoppingToken);
            await Task.Delay(10000);
            Console.WriteLine("R-E-S-E-T");
        }

    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is working.");

        using (var scope = Services.CreateScope())
        {
            var scopedProcessingServices =
                scope.ServiceProvider
                    .GetServices<IScopedProcessingService>();

            foreach (var scopedProcessingService in scopedProcessingServices)
            {
                await scopedProcessingService.DoWork(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}