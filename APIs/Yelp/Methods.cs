using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace tekno.Services.APIs.Yelp
{
    public static class Methods
    {

        private const string c_Key = "Your Yelp API Key";
        private const Int32 c_MaxResultCount = 20;

        private const string c_PhoneRequestUrl = "http://api.yelp.com/phone_search";
        private const string c_ReviewsRequestUrl = "http://api.yelp.com/business_review_search";

        private static Request GetApiRequest(string requestUrl)
        {
            Request request = new Request(requestUrl);
            request.Parameters.Add("ywsid", c_Key);
            return request;
        }




        public static JObject SearchbyPhone(string phone)
        {

            Request request = GetApiRequest(c_PhoneRequestUrl);
            
            request.Parameters.Add("phone", phone);

            return request.ExecuteRequest<JObject>();
        }


        public static JObject SearchLocation(string location, string category, string term)
        {
            return SearchLocation(location, term,category , c_MaxResultCount);
        }

        public static JObject SearchLocation(string location, string category , string term, Int32 resultCount)
        {
            Request request = GetApiRequest(c_ReviewsRequestUrl);

            request.Parameters.Add("location", location);
            request.Parameters.Add("category", category);
            request.Parameters.Add("term", term);
            request.Parameters.Add("num_biz_requested", resultCount.ToString());

            return request.ExecuteRequest<JObject>();
        }

        public static JObject SearchLocation(string location, Double radius, string category, string term)
        {
            return SearchLocation(location, term, category,  c_MaxResultCount);
        }

        public static JObject SearchLocation(string location, Double radius, string category, string term, Int32 resultCount)
        {
            Request request = GetApiRequest(c_ReviewsRequestUrl);

            request.Parameters.Add("location", location);
            request.Parameters.Add("radius", radius.ToString());
            request.Parameters.Add("category", category);
            request.Parameters.Add("term", term);
            request.Parameters.Add("num_biz_requested", resultCount.ToString());

            return request.ExecuteRequest<JObject>();
        }


       




    }
}
