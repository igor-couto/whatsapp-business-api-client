namespace WhatsappBusinessApiClient;

using System.Text;
using System.Web;

public class RegisterWebhookRequest
{
    private string _callbackUrl;

    public RegisterWebhookRequest(string endpoint)
    {
        _callbackUrl = HttpUtility.UrlEncode(endpoint);
    }

    public StringContent ToContent()
    {
        var stringContent = $"object=page&callback_url={_callbackUrl}&fields=about%2C+picture&include_values=true&verify_token=thisisaverifystring";

        return new StringContent(stringContent, Encoding.UTF8, "text/plain");
    }
}