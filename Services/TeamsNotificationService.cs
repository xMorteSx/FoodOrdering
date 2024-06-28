using System.Net.Http.Headers;

namespace FoodOrdering.Services
{
    public class TeamsNotificationService
    {
        private const string TEAMS_APP_ID = "6b69e5ad-4371-457d-93a7-81dd19760974";
        private const string ENTITY_ID = "index";

        public async Task SendNewOrderNotificationAsync(string orderName, Uri localUrl)
        {
            var teamsUrl = $"https://teams.microsoft.com/l/entity/{TEAMS_APP_ID}/{ENTITY_ID}/index";

            var adaptiveCardJson = @"{
              ""type"": ""message"",
              ""attachments"": [
                {
                  ""contentType"": ""application/vnd.microsoft.card.adaptive"",
                  ""content"": {
                    ""type"": ""AdaptiveCard"",
                    ""body"": [
                      {
                        ""type"": ""TextBlock"",
                        ""text"": ""Zamówienie o nazwe " + orderName + @" stworzone.""
                      }
                    ],
                    ""actions"": [
                      {
                        ""type"": ""Action.OpenUrl"",
                        ""title"": ""Otwórz aplikacje"",
                        ""url"": """ + teamsUrl + @"""
                      }
                    ],
                    ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
                    ""version"": ""1.0""
                  }
                }
              ]
            }";

            var webhookUrl = "https://marcinai.webhook.office.com/webhookb2/3bc67c74-8670-4f46-b52e-7d38c069f5c9@edc00cee-d881-4ab5-846a-dcbb3335c3e5/IncomingWebhook/04dc66a9ef6344f893dc02f2fc8d3c51/17fd5b35-cd76-4149-835a-e65ec6e9411f";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(adaptiveCardJson, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(webhookUrl, content);
        }
    }
}
