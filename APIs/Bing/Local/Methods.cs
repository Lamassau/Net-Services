using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace tekno.Services.APIs.Bing.Local
{
    public static class Methods
    {

        /// <summary>
        /// !TODO replace with your Bing API Key
        /// </summary>
        private const string c_Key = "Your Bing API Key";
        private const string c_Version = "2.0";
        private const string c_MainRequestUrl =  "http://api.bing.net/json.aspx";


        private static Request GetApiRequest(string RequestUrl, string method)
        {
            Request request = new Request(RequestUrl );
            request.Parameters.Add("AppId", c_Key );
            request.Parameters.Add("Version", c_Version );

            request.Parameters.Add("Sources", method);

            return request;
        }



        public static JObject LocalSearch(String query)
        {
            Request request = GetApiRequest(c_MainRequestUrl, "Phonebook");

            request.Parameters.Add("Query", query);
            return request.ExecuteRequest<JObject>();
        }

        



    }
}