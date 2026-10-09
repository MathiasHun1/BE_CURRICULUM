var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = false);

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { service = "SlowService" }));

// Mentor-provided test fixture. The learner implements the caller in LabApi.
app.MapGet("/slow", async (int? delayMs, HttpContext context, CancellationToken cancellationToken) =>
{
    var delay = delayMs ?? 3000;
    if (delay is < 0 or > 10000)
    {
        return Results.BadRequest(new { error = "delayMs must be between 0 and 10000." });
    }

    app.Logger.LogInformation("Slow request {RequestId} started; delay {DelayMs} ms",
        context.TraceIdentifier, delay);

    try
    {
        await Task.Delay(delay, cancellationToken);
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
    {
        app.Logger.LogInformation("Slow request {RequestId} canceled", context.TraceIdentifier);
        throw;
    }

    app.Logger.LogInformation("Slow request {RequestId} completed", context.TraceIdentifier);
    return Results.Text("Slow service response");
});

app.Run();
