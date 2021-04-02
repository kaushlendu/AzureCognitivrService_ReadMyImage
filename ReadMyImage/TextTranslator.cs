using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReadMyImage
{
    class TextTranslator
    {
        string location = "southeastasia";
        private static readonly string subscriptionKey = "4c50be791876429f935bd3fb1ac4306d";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        public async Task<string> translate(string text)
        {
            string route = "/translate?api-version=3.0&from=en&to=hi";
            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                string temp = result.Substring(result.IndexOf("text") + 7, result.Length - result.IndexOf("text") - 7);
                return temp.Substring(0, temp.IndexOf(",") - 1);
            }
        }

    }
}
