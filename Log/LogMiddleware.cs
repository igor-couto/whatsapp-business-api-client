namespace WhatsappBusinessApiClient.Logs;

using System.Text;

public static class Log
{
    public static void UseLogs(this WebApplication app)
    {
        app.Use((context, next) =>
        {
            context.Request.EnableBuffering();
            return next();
        });

        app.UseMiddleware<LogMiddleware>();
    }
}

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> iLogger)
    {
        _logger = iLogger;
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        await _next(httpContext);

        await LogRequest(httpContext);
    }

    private async Task LogRequest(HttpContext httpContext)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder
            .AppendLine()
            .AppendPathFromRequest(httpContext)
            .AppendMethodFromRequest(httpContext)
            .AppendQueryStringFromRequest(httpContext)
            .AppendHeadersFromRequest(httpContext);

        await stringBuilder.AppendRequestBody(httpContext);

        _logger.LogInformation(stringBuilder.ToString());
    }
}