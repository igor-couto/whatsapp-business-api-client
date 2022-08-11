namespace WhatsappBusinessApiClient.Logs;

using System.Text;

public static class StringBuilderExtension
{
    public static StringBuilder AppendPathFromRequest(this StringBuilder stringBuilder, HttpContext httpContext)
    {
        if (httpContext.Request.Path.HasValue)
            return stringBuilder.AppendLine($"Path: {httpContext.Request.Path.Value}");
        else
            return stringBuilder;
    }

    public static StringBuilder AppendMethodFromRequest(this StringBuilder stringBuilder, HttpContext httpContext)
        => stringBuilder.AppendLine($"Method: {httpContext.Request.Method}");

    public static StringBuilder AppendQueryStringFromRequest(this StringBuilder stringBuilder, HttpContext httpContext)
    {
        if (httpContext.Request.QueryString.HasValue)
            return stringBuilder.AppendLine($"Query string: {httpContext.Request.QueryString.Value}");
        else
            return stringBuilder;
    }

    public static StringBuilder AppendHeadersFromRequest(this StringBuilder stringBuilder, HttpContext httpContext)
    {
        if (httpContext.Request.Headers is not null)
        {
            stringBuilder.AppendLine("Headers:");
            return stringBuilder.AppendHeaders(httpContext.Request.Headers);
        }
        else
            return stringBuilder;
    }

    private static StringBuilder AppendHeaders(this StringBuilder stringBuilder, IHeaderDictionary headers)
    {
        foreach (var header in headers)
            stringBuilder.AppendLine($"\t{header.Key}: {header.Value}");

        return stringBuilder;
    }

    public static async Task<StringBuilder> AppendRequestBody(this StringBuilder stringBuilder, HttpContext httpContext)
    {
        var requestBody = await GetRequestBody(httpContext);
        if (!string.IsNullOrEmpty(requestBody))
        {
            stringBuilder.AppendLine("Body:");
            return stringBuilder.AppendLine($"{requestBody}");
        }
        else return stringBuilder;
    }

    public static async Task<string> GetRequestBody(HttpContext httpContext)
    {
        if (!httpContext.Request.Body.CanSeek)
            httpContext.Request.EnableBuffering();
        httpContext.Request.Body.Position = 0;
        var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
        var requestBody = await reader.ReadToEndAsync().ConfigureAwait(false);
        httpContext.Request.Body.Position = 0;
        return requestBody ?? string.Empty;
    }
}