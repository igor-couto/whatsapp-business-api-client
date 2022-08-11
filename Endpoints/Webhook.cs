namespace WhatsappBusinessApiClient.Endpoints;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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

    public static IResult VerifyWebhook(WebhookVerificationQueryString webhookVerificationQueryString, [FromServices] IConfiguration configuration)
    {
        var verifyToken = configuration["VerifyToken"];

        if (webhookVerificationQueryString.Mode == "subscribe" && webhookVerificationQueryString.VerifyToken == verifyToken)
        {
            Console.WriteLine("Webhook verified with success");
            return Results.Ok(int.Parse(webhookVerificationQueryString.Challenge));
        }

        return Results.Forbid();
    }

    public static IResult ReceiveMessage(WebhookRequest webhookRequest)
    {
        Console.WriteLine("Message Received: " + JsonSerializer.Serialize(webhookRequest, new JsonSerializerOptions { WriteIndented = true }));
        return Results.Ok();
    }

    private static async Task<IResult> RegisterWebhook(
        string endpoint,
        [FromServices] IHttpClientFactory httpClientFactory,
        [FromServices] IConfiguration configuration)
    {
        var verifyToken = configuration["VerifyToken"];
        var content = new RegisterWebhookRequest(endpoint, verifyToken).ToContent();

        var appId = configuration.GetSection("WhatsappCloudApi")["AppId"];
        var httpClient = httpClientFactory.CreateClient("WhatsappCloudApiWithAppToken");
        await httpClient.PostAsync($"{appId}/subscriptions", content);

        return Results.Ok();
    }
}