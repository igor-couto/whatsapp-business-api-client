namespace WhatsappBusinessApiClient;

using System.Text;

public static class HttpContextExtension
{
    public static async Task<string> GetRequestBody(this HttpContext httpContext)
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