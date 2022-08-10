namespace WhatsappBusinessApiClient.Endpoints;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatsappBusinessApiClient.Requests.Incoming;
using WhatsappBusinessApiClient.Requests.Outgoing;

public static class Webhook
{
    public static void MapWebhookEndpoints(this WebApplication app)
    {
        app.MapGet("/webhook", VerifyWebhook);
        app.MapPost("/webhook", ReceiveMessage);
        app.MapPost("/webhook/register", RegisterWebhook);
    }

    public static IResult VerifyWebhook(WebhookVerificationRequest webhookVerificationRequest, [FromServices] IConfiguration configuration)
    {
        var verifyToken = configuration["VerifyToken"];

        if (webhookVerificationRequest.Mode == "subscribe" && webhookVerificationRequest.VerifyToken == verifyToken)
        {
            Console.WriteLine("Webhook verified with success");
            return Results.Ok();
        }

        return Results.Forbid();
    }

    public static IResult ReceiveMessage(WebhookRequest webhookRequest)
    {
        Console.WriteLine(JsonConvert.SerializeObject(webhookRequest, Formatting.Indented));
        return Results.Ok();
    }

    //https://developers.facebook.com/docs/graph-api/reference/v14.0/app/subscriptions
    private static async Task<IResult> RegisterWebhook(
        string endpoint,
        [FromServices] IHttpClientFactory httpClientFactory,
        [FromServices] IConfiguration configuration)
    {
        var httpClient = httpClientFactory.CreateClient("WhatsappCloudApi");

        var content = new RegisterWebhookRequest(endpoint).ToContent();

        var appId = configuration.GetSection("WhatsappCloudApi")["AppId"];

        await httpClient.PostAsync($"{appId}/subscriptions", content);

        return Results.Ok();
    }
}