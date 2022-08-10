namespace WhatsappBusinessApiClient.Requests.Incoming;

using System.Text.Json.Serialization;

public class WebhookRequest
{
    [JsonPropertyName("object")]
    public string Object;

    [JsonPropertyName("entry")]
    public List<Entry> Entry;
}

public class Entry
{
    [JsonPropertyName("id")]
    public string Id;

    [JsonPropertyName("changes")]
    public List<Change> Changes;
}

public class Change
{
    [JsonPropertyName("field")]
    public string Field;

    [JsonPropertyName("Value")]
    public Value Value;
}

public class Value
{
    [JsonPropertyName("messaging_product")]
    public string MessagingProduct;

    [JsonPropertyName("metadata")]
    public Metadata Metadata;

    [JsonPropertyName("contacts")]
    public List<Contact> Contacts;

    [JsonPropertyName("messages")]
    public List<Message> Messages;
}

public class Metadata
{
    [JsonPropertyName("display_phone_number")]
    public string DisplayPhoneNumber;

    [JsonPropertyName("phone_number_id")]
    public string PhoneNumberId;
}

public class Contact
{
    [JsonPropertyName("profile")]
    public Profile Profile;

    [JsonPropertyName("wa_id")]
    public string WhatsappId;
}

public class Profile
{
    [JsonPropertyName("name")]
    public string Name;
}

public class Message
{
    [JsonPropertyName("from")]
    public string From;

    [JsonPropertyName("id")]
    public string Id;

    [JsonPropertyName("type")]
    public string Type;

    [JsonPropertyName("timestamp")]
    public string Timestamp;

    [JsonPropertyName("text")]
    public Text Text;
}

public class Text
{
    [JsonPropertyName("body")]
    public string Body;
}