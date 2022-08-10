namespace WhatsappBusinessApiClient.Requests.Outgoing;

using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WhatsappBusinessApiClient.Requests.Incoming;

public class SendTextMessageRequest
{
    [JsonPropertyName("messaging_product")]
    public string MessagingProduct => "whatsapp";

    [JsonPropertyName("type")]
    public string Type => "text";

    [JsonPropertyName("recipient_type")]
    public string RecipientType => "individual";

    [JsonPropertyName("to")]
    public string To { get; private set; }

    [JsonPropertyName("text")]
    public Text Text { get; private set; }

    public SendTextMessageRequest(string whatsappId, string text)
    {
        To = whatsappId;
        Text = new();
        Text.Body = text;
    }

    public StringContent ToContent()
    {
        var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        return new StringContent(jsonString, Encoding.UTF8, "application/json");
    }
}