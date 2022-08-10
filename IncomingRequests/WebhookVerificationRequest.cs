using Newtonsoft.Json;

namespace WhatsappBusinessApiClient.IncomingRequests;

public class WebhookVerificationRequest
{
    [JsonProperty("mode")]
    public string Mode { get; set; }

    [JsonProperty("verify_token")]
    public string VerifyToken { get; set; }

    [JsonProperty("challenge")]
    public string Challenge { get; set; }
}