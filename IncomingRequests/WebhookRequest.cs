namespace WhatsappBusinessApiClient;

using Newtonsoft.Json;

public class WebhookRequest
{
    [JsonProperty("object")]
    public string Object;

    [JsonProperty("entry")]
    public List<Entry> Entry;
}

public class Entry
{
    [JsonProperty("id")]
    public string Id;

    [JsonProperty("changes")]
    public List<Change> Changes;
}

public class Change
{
    [JsonProperty("field")]
    public string Field;

    [JsonProperty("Value")]
    public Value Value;
}

public class Value
{
    [JsonProperty("messaging_product")]
    public string MessagingProduct;

    [JsonProperty("metadata")]
    public Metadata Metadata;

    [JsonProperty("contacts")]
    public List<Contact> Contacts;

    [JsonProperty("messages")]
    public List<Message> Messages;
}

public class Metadata
{
    [JsonProperty("display_phone_number")]
    public string DisplayPhoneNumber;

    [JsonProperty("phone_number_id")]
    public string PhoneNumberId;
}

public class Contact
{
    [JsonProperty("profile")]
    public Profile Profile;

    [JsonProperty("wa_id")]
    public string WhatsappId;
}

public class Profile
{
    [JsonProperty("name")]
    public string Name;
}

public class Message
{
    [JsonProperty("from")]
    public string From;

    [JsonProperty("id")]
    public string Id;

    [JsonProperty("type")]
    public string Type;

    [JsonProperty("timestamp")]
    public string Timestamp;

    [JsonProperty("text")]
    public Text Text;
}

public class Text
{
    [JsonProperty("body")]
    public string Body;
}