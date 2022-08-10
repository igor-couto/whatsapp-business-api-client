namespace WhatsappBusinessApiClient.Endpoints;

using Microsoft.AspNetCore.Mvc;

public static class SendMessage
{
    public static void MapSendMessageEndpoints(this WebApplication app)
    {
        app.MapPost("/send-text-message", SendTextMessage);
    }

    private static async Task<IResult> SendTextMessage(
        string number,
        string message,
        [FromServices] IHttpClientFactory httpClientFactory,
        [FromServices] IConfiguration configuration)
    {
        var httpClient = httpClientFactory.CreateClient("WhatsappCloudApi");

        var content = new SendTextMessageRequest(number, message).ToContent();

        var phoneNumberId = configuration.GetSection("WhatsappCloudApi")["PhoneNumberId"];

        await httpClient.PostAsync($"/{phoneNumberId}/messages", content);

        return Results.Ok();
    }
}