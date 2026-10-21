var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("slow", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["SlowService:BaseUrl"]
        ?? throw new InvalidOperationException("Missing SlowService:BaseUrl configuration."));
});

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { service = "LabApi" }));

app.MapGet("/fetch", async (IHttpClientFactory clientFactory, CancellationToken cancellationToken) =>
{
    using var client = clientFactory.CreateClient("slow");
    client.Timeout = TimeSpan.FromSeconds(1);
    var logger = app.Logger;
    
    logger.LogInformation(
        "before | Time: {Time:O} | Thread: {ThreadId}",
        DateTimeOffset.UtcNow,
        Environment.CurrentManagedThreadId);

    try
    {
        var response = await client.GetStringAsync("/slow?delayMs=3000", cancellationToken);
        return Results.Text(response, "text/plain");
    }
    catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested &&
                                                ex.InnerException is TimeoutException)
    {
        logger.LogInformation(
            "timeout | Time: {Time:O} | Thread: {ThreadId}",
            DateTimeOffset.UtcNow,
            Environment.CurrentManagedThreadId);
        return Results.Problem(statusCode: 504, title: "Slow service timeout");
    }
    catch (HttpRequestException ex)
    {
        logger.LogWarning(
            ex,
            "Slow service error | Status code: {StatusCode}",
            ex.StatusCode);
        return Results.Problem(statusCode: 502, title: "Slow service error");
    }
});

app.MapGet("/fetch-sequential", async (IHttpClientFactory clientFactory, CancellationToken cancellationToken) =>
{
    using var client = clientFactory.CreateClient("slow");
    
    var response1 = await client.GetStringAsync("/slow?delayMs=3000", cancellationToken);
    var response2 = await client.GetStringAsync("/slow?delayMs=3000", cancellationToken);

    return Results.Ok(new[] { response1, response2 });
});

app.MapGet("/fetch-concurrent", async (IHttpClientFactory clientFactory, CancellationToken cancellationToken) =>
{
    using var client = clientFactory.CreateClient("slow");

    var response1Task = client.GetStringAsync("/slow?delayMs=3000", cancellationToken);
    var response2Task = client.GetStringAsync("/slow?delayMs=3000", cancellationToken);
    
    var responses = await Task.WhenAll(response1Task, response2Task);

    return Results.Ok(responses);
});

app.Run();
