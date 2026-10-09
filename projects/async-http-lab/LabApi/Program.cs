var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("slow", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["SlowService:BaseUrl"]
        ?? throw new InvalidOperationException("Missing SlowService:BaseUrl configuration."));
});

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { service = "LabApi" }));

// M1: Replace this placeholder with your async HTTP handler.
// Requirements and run commands: ../README.md
app.MapGet("/fetch", () => Results.StatusCode(StatusCodes.Status501NotImplemented));

app.Run();
