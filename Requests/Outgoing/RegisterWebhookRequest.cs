namespace WhatsappBusinessApiClient.Requests.Outgoing;

using System.Text;
using System.Web;

public class RegisterWebhookRequest
{
    private string _callbackUrl;
    private string _verifyToken;

    public RegisterWebhookRequest(string endpoint, string verifyToken)
    {
        _callbackUrl = HttpUtility.UrlEncode(endpoint);
        _verifyToken = verifyToken;
    }

    public StringContent ToContent()
    {
        var stringContent = $"object=whatsapp_business_account&callback_url={_callbackUrl}&verify_token={_verifyToken}";

        return new StringContent(stringContent, Encoding.UTF8, "text/plain");
    }
}