
using Newtonsoft.Json.Linq;
using StructureMap;
using System;

namespace tekno.Services.APIs.Google.PlacesSearch
{
    public static class Methods
    {

        /// <summary>
        /// !TODO replace with your Google API Key
        /// </summary>
        private const string apiKey = "Your Google API Key";

        private const string c_LocalRequestUrl = "https://maps.googleapis.com/maps";


        private static Request GetApiRequest(string RequestUrl)
        {
            Request request = new Request(RequestUrl);

            request.Parameters.Add("key", apiKey);

            return request;
        }




        public static JObject GetVenue(string placeId)
        {
            Request request = GetApiRequest(c_LocalRequestUrl + "/api/place/details/json");
            request.Parameters.Add("placeid", placeId);
            
            return request.ExecuteRequest<JObject>();
        }
    }
}