var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<WorkerEmoji>();


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
