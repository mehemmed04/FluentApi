using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FluentApi.Domain.Services
{

    public static class ImageService
    {
        public struct SearchResult
        {
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }
        const string subscriptionKey = "3beee7f4-5f7e-4e18-bba6-e2182bdce82a";
        
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/images/search";
        public static SearchResult BingImageSearch(string SearchTerm)
        {
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(SearchTerm);

            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();


            var searchResult = new SearchResult()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<String, String>()
            };

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }
            return searchResult;

        }

        public static void GetImagePath(string name)
        {
            SearchResult result = BingImageSearch(name);
            //deserialize the JSON response from the Bing Image Search API
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(result.jsonResult);

            var firstJsonObj = jsonObj["value"][0];
            MessageBox.Show("Title for the first image result: " + firstJsonObj["name"] + "\n");
            //After running the application, copy the output URL into a browser to see the image.
            MessageBox.Show("URL for the first image result: " + firstJsonObj["webSearchUrl"] + "\n");

        }

    }
}
