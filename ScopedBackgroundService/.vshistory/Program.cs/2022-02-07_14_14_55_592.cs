using ScopedBackgroundService.ScopedExample;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHostedService<WorkerEmoji>();

builder.Services.AddScoped<IScopedProcessingService, ScopedSimpleTextService>();
builder.Services.AddScoped<IScopedProcessingService, ScopedEmojiService>();

builder.Services.AddScoped<ScopedEmojiService>
    (serviceprovider => 
    {   return serviceprovider.GetServices<IScopedProcessingService>()
        .OfType<ScopedEmojiService>().FirstOrDefault(); ;
    });

builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();

var app = builder.Build();



app.MapGet("/", (ScopedEmojiService emojiService, HttpContext context) =>
{
    context.Response.ContentType = "text/html";
    return emojiService.HTML;
});

app.MapGet("/happy", (ScopedEmojiService emojiService) =>
{
    emojiService.AddHappyEmoji();
});

app.MapGet("/sad", (ScopedEmojiService emojiService) =>
{
    emojiService.AddSadEmoji();
});

app.Run();
