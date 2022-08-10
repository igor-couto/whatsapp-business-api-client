namespace WhatsappBusinessApiClient.Logs;

using System.Text;

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