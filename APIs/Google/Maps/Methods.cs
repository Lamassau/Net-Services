using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace tekno.Services.APIs.Google.Maps
{
    public static class Methods
    {
        private const string apiKey = "None";

        private const string c_LocalRequestUrl = "https://maps.googleapis.com/maps/api/geocode/json";


        private static Request GetApiRequest(string RequestUrl)
        {
            Request request = new Request(RequestUrl);

            request.Parameters.Add("sensor", "false");
            
            return request;
        }


        public static JObject Geocoding(String address)
        {
            Request request = GetApiRequest(c_LocalRequestUrl);
            request.Parameters.Add("address", address);
            return request.ExecuteRequest<JObject>();
        }
        

        
    }
}