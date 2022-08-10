namespace WhatsappBusinessApiClient.Endpoints;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatsappBusinessApiClient.IncomingRequests;

public static class Webhook
{
    public static void MapWebhookEndpoints(this WebApplication app)
    {
        app.MapPost("/webhook", ReceiveMessage);
        app.MapPost("/register-webhook", RegisterWebhook);
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

        // essa chamada deve ser feita com o app access token no header!

        await httpClient.PostAsync($"{appId}/subscriptions", content);

        return Results.Ok();
    }
}