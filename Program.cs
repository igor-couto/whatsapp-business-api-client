using WhatsappBusinessApiClient.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var version = builder.Configuration.GetSection("WhatsappCloudApi")["Version"];
var userToken = builder.Configuration.GetSection("WhatsappCloudApi")["UserToken"];

builder.Services.AddHttpClient("WhatsappCloudApi", httpClient =>
{
    httpClient.BaseAddress = new Uri($"https://graph.facebook.com/{version}/");
    httpClient.DefaultRequestHeaders.Add("Authorization", userToken);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config => config.DefaultModelsExpandDepth(-1));

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapSendMessageEndpoints();

app.MapWebhookEndpoints();

app.Run();