﻿



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
            await Task.Delay(2000);
            Console.WriteLine("==================");
            Console.WriteLine("R-E-S-E-T");
            Console.WriteLine("==================");
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

            for (int i = 0; i < 5; i++)
            {
                foreach (var scopedProcessingService in scopedProcessingServices)
                {
                    await scopedProcessingService.DoWork(stoppingToken);
                }
                await Task.Delay(2000);
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