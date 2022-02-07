var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<VideosWatcher>();}

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
