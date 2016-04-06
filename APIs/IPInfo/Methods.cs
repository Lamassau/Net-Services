using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace tekno.Services.APIs.IPInfo
{
    public static class Methods
    {
        private const string c_Key = "Your IP-Info API Key";

        private const string c_LocalRequestUrl = "http://api.ipinfodb.com/v3/ip-city/";


        private static Request GetApiRequest(string RequestUrl)
        {
            Request request = new Request(RequestUrl);

            request.Parameters.Add("key", c_Key );
            request.Parameters.Add("format", "json");
            //request.Parameters.Add("timezone", "true" );

            return request;
        }


        public static JObject GetLocation(string IP)
        {
            Request request = GetApiRequest(c_LocalRequestUrl);
            request.Parameters.Add("ip", IP);
            return request.ExecuteRequest<JObject>();
        }

//  {
//  "Ip" : "74.125.45.100",
//  "Status" : "OK",
//  "CountryCode" : "US",
//  "CountryName" : "United States",
//  "RegionCode" : "06",
//  "RegionName" : "California",
//  "City" : "Mountain View",
//  "ZipPostalCode" : "94043",
//  "Latitude" : "37.4192",
//  "Longitude" : "-122.057",
//  "TimezoneName" : "America/Los_Angeles",
//  "Gmtoffset" : "-28800",
//  "Isdst" : "0"
//}
        
    }
}