namespace WhatsappBusinessApiClient.Requests.Outgoing;

using System.Text;
using Newtonsoft.Json;
using WhatsappBusinessApiClient.Requests.Incoming;

public class SendTextMessageRequest
{
    [JsonProperty("messaging_product")]
    public string MessagingProduct => "whatsapp";

    [JsonProperty("type")]
    public string Type => "text";

    [JsonProperty("recipient_type")]
    public string RecipientType => "individual";

    [JsonProperty("to")]
    public string To { get; private set; }

    [JsonProperty("text")]
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