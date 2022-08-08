namespace WhatsappBusinessApiClient;

using Newtonsoft.Json;

public class WebhookRequest
{
    [JsonProperty("object")]
    public string Object { get; set; }

    [JsonProperty("entry")]
    public List<Entry> Entry { get; set; }
}

public class Entry
{
    [JsonProperty("changes")]
    public List<Change> Changes { get; set; }
}

public class Change
{
    [JsonProperty("field")]
    public string Field { get; set; }

    [JsonProperty("Value")]
    public Value Value { get; set; }
}

public class Value
{
    [JsonProperty("messaging_product")]
    public string MessagingProduct { get; set; }

    [JsonProperty("metadata")]
    public Metadata Metadata { get; set; }
}

public class Metadata
{
    [JsonProperty("display_phone_number")]
    public string display_phone_number { get; set; }

    [JsonProperty("phone_number_id")]
    public string phone_number_id { get; set; }
}