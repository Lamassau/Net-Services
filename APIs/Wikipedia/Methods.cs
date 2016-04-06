using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace tekno.Services.APIs.Wikipedia
{
    public static class Methods
    {
        private const string c_RequestUrl = "http://en.wikipedia.org/w/api.php";

        private static Request GetApiRequest(string RequestUrl, string method)
        {
            Request request = new Request(RequestUrl);
            request.Parameters.Add("format", "json");
            request.Parameters.Add("action", method);
            return request;
        }

        public static JObject GetArticle(String query)
        {
            Request request = GetApiRequest(c_RequestUrl,"query" );

            
            request.Parameters.Add("prop", "revisions");
            request.Parameters.Add("rvprop", "content");

            request.Parameters.Add("titles", query);

            JObject jO = request.ExecuteRequest<JObject>();
            return Parse(jO["query"]["pages"].First.First["revisions"].ToString());
        }

        //http://en.wikipedia.org/w/api.php?action=opensearch&search=Indian+Cuisine
        public static JArray OpenSearch(String query)
        {
            Request request = GetApiRequest(c_RequestUrl, "opensearch");
            request.Parameters.Add("search", query);
            return request.ExecuteRequest<JArray>();
        }

        


        private static JObject Parse(string text)
        {

            Request request = GetApiRequest(c_RequestUrl, "parse");
            request.Parameters.Add("text", text);
            request.Method = requestMethod.POST;
            return request.ExecuteRequest<JObject>();
        }

    }
}


