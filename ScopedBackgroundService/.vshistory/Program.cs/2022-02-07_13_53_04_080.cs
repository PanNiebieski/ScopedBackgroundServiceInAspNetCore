using ScopedBackgroundService.ScopedExample;
using ScopedBackgroundService.SingletonBCService;
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

app.MapGet("/", ( HttpContext context) => 
{
    var worker = 
    context.RequestServices.GetServices<IHostedService>()
    .OfType<WorkerEmoji>().FirstOrDefault();

    context.Response.ContentType = "text/html";
    return worker?.HTML;
});

app.MapGet("/happy", (HttpContext context) =>
{
    var worker =
    context.RequestServices.GetServices<IHostedService>()
    .OfType<WorkerEmoji>().FirstOrDefault();

    if (worker is not null)
    {
        worker.AddHappyEmoji();
        context.Response.StatusCode = (int)HttpStatusCode.NoContent;
        return "";
    }
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    return "";
});

app.MapGet("/happy", (HttpContext context) =>
{
    var worker =
    context.RequestServices.GetServices<IHostedService>()
    .OfType<WorkerEmoji>().FirstOrDefault();

    if (worker is not null)
    {
        worker.AddHappyEmoji();
        context.Response.StatusCode = (int)HttpStatusCode.NoContent;
        return "";
    }
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    return "";
});

app.MapGet("/", (IScopedProcessingService service) =>
{
    var worker = context.RequestServices.GetServices<IHostedService>()
    .OfType<WorkerEmoji>().FirstOrDefault();
});

app.Run();