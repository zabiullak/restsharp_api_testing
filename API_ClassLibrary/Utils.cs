using API_ClassLibrary.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API_ClassLibrary
{
    public static class Utils
    {

        public static RestClient SetUrl(string baseUrl, string endPoint)
        {
            var url = string.Format($"{baseUrl}{endPoint}");
             return new RestClient(url);
        }

        public static RestRequest CreateGetRequest()
        {
            RestRequest req = new RestRequest()
            {
                Method = Method.Get
            };

            req.AddHeaders(new Dictionary<string, string>
            {
                {"Accept","application/json" },
                {"Content-Type", "application/json" }
            });

            return req;
        }

        public async static Task<RestResponse> GetResponseAsync(RestClient client, RestRequest req)
        {
            RestResponse res =  await client.ExecuteAsync(req);
            //FW.Log.Info($"Respose ==> {res.Content}");
            return res;
        }

        internal static RestRequest CreatePostRequest<T>(T payLoad) where T : class
        {
            RestRequest req = new RestRequest()
            {
                Method = Method.Post
            };

            req.AddHeader("Accept", "application/json");
            req.AddBody(payLoad);
            req.RequestFormat = DataFormat.Json;
            return req;
        }
    }
}