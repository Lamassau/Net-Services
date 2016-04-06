using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace tekno.Services.APIs.Fatsecret
{
    public static class Methods
    {
        private const string c_Key = "Your FatSecret API Key"; 
        private const  string c_Secret = "Your FatSecret Secret Key";
        private const string c_RequestUrl = "http://platform.fatsecret.com/rest/server.api";


        private static Request GetApiRequest(string RequestUrl, string method)
        {
            Request request = new Request(RequestUrl);
            request.Parameters.Add("method", method );
            request.Parameters.Add("format", "json");
            request.oAuth_ConsumerKey= c_Key;
            request.oAuth_ConsumerSecret= c_Secret;
            return request;
        }


        public static JObject FoodsSearch(String query,int maxResults=20)
        {
            Request request = GetApiRequest(c_RequestUrl, "foods.search");
            request.Parameters.Add("search_expression", query);
            request.Parameters.Add("max_results", maxResults.ToString()); 
            return request.ExecuteRequest<JObject>();
        }
    }
}