using BackgroundServiceExample;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<WorkerEmoji>();

var app = builder.Build();


app.MapGet("/", (HttpContext context) =>
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

app.MapGet("/sad", (HttpContext context) =>
{
    var worker =
    context.RequestServices.GetServices<IHostedService>()
    .OfType<WorkerEmoji>().FirstOrDefault();

    if (worker is not null)
    {
        worker.AddSadEmoji();
        context.Response.StatusCode = (int)HttpStatusCode.NoContent;
        return "";
    }
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    return "";
});

app.Run();
