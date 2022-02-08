using Microsoft.Extensions.Hosting;
using ScopedBackgroundService.ScopedExample;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IScopedProcessingService, ScopedSimpleTextService>();
builder.Services.AddScoped<IScopedProcessingService, ScopedEmojiService>();

builder.Services.AddScoped<ScopedEmojiService>
    (serviceprovider => 
    {   return serviceprovider.GetServices<IHostedService>()
        .OfType<ConsumeScopedServiceHostedService>().FirstOrDefault()
        .EmojiService; ;
    });

builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();

var app = builder.Build();


app.MapGet("/", (ScopedEmojiService emojiService, HttpContext context) =>
{
    if (emojiService != null)
    {
        context.Response.ContentType = "text/html";
        return emojiService.HTML;
    }
    return "";
});

app.MapGet("/happy", (ScopedEmojiService emojiService) =>
{
    if (emojiService != null)
        emojiService.AddHappyEmoji();
});

app.MapGet("/sad", (ScopedEmojiService emojiService) =>
{
    if (emojiService != null)
        emojiService.AddSadEmoji();
});

app.Run();
