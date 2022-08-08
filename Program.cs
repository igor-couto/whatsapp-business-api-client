using WhatsappBusinessApiClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var version = builder.Configuration.GetSection("WhatsappCloudApi")["Version"];
var userToken = builder.Configuration.GetSection("WhatsappCloudApi")["UserToken"];

builder.Services.AddHttpClient("WhatsappCloudApi", httpClient =>
{
    //httpClient.BaseAddress = new Uri($"https://graph.facebook.com/{version}/");
    httpClient.BaseAddress = new Uri($"https://localhost:7201/{version}/");
    httpClient.DefaultRequestHeaders.Add("Authorization", userToken);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config => config.DefaultModelsExpandDepth(-1));

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();