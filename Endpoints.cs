namespace WhatsappBusinessApiClient;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/send-message", SendMessage);
        app.MapPost("/receive-message-webhook", ReceiveMessageWebhook);
        app.MapPost("/register-webhook", RegisterWebhook);
    }

    private static async Task<IResult> SendMessage(
        string number,
        string message,
        [FromServices] IHttpClientFactory httpClientFactory)
    {
        var httpClient = httpClientFactory.CreateClient("WhatsappCloudApi");

        var content = new SendTextMessageRequest(number, message).ToContent();

        await httpClient.PostAsync("/{Phone-Number-ID}/messages", content);

        return Results.Ok();
    }

    private static IResult ReceiveMessageWebhook(WebhookRequest webhookRequest)
    {
        Console.WriteLine(JsonConvert.SerializeObject(webhookRequest, Formatting.Indented));
        return Results.Ok();
    }

    //https://developers.facebook.com/docs/graph-api/reference/v14.0/app/subscriptions

    private static async Task<IResult> RegisterWebhook(string endpoint, [FromServices] IHttpClientFactory httpClientFactory)
    {
        var httpClient = httpClientFactory.CreateClient("WhatsappCloudApi");

        var content = new RegisterWebhookRequest(endpoint).ToContent();

        await httpClient.PostAsync($"Phone-Number-ID/subscriptions", content);

        return Results.Ok();
    }
}