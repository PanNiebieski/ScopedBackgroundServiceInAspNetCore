var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<TimedHostedService>();

builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
