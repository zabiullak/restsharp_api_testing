using API_ClassLibrary.Framework;
using API_ClassLibrary.Models.Request;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace API_ClassLibrary
{
    public static class API_calls
    {
        



        public static async Task<RestResponse> GetUsersData(string baseUrl)
        {
            //var restCleint = new RestClient(baseUrl);
            //var restRequest = new RestRequest(ListUsers);

            RestClient client = Utils.SetUrl(baseUrl, EndPoint.ListUsers);
            RestRequest req = Utils.CreateGetRequest();
            //FW.Log.Info($"Request ==> {req.GetType()}");
            req.RequestFormat = DataFormat.Json;
            RestResponse res = await Utils.GetResponseAsync(client, req);
            
            return res;
            //restRequest.AddHeader("Accept", "application/json");
            //restRequest.RequestFormat = DataFormat.Json;
            //var res = restCleint.Execute(restRequest);
            //var content = res.Content;
            //var userList = JsonConvert.DeserializeObject<ListUsersDTO>(content);
            //return userList;
        }

        public static async Task<RestResponse> CreateNewUser(string bASE_URL, dynamic payLoad)
        {
            RestClient client = Utils.SetUrl(bASE_URL, EndPoint.CreateUser);
            RestRequest req = Utils.CreatePostRequest<CreateUserReq>(payLoad);
            
            RestResponse res = await Utils.GetResponseAsync(client, req);
            return res;
        }
    }

    public class EndPoint
    {
        internal const string ListUsers = "/api/users?page=2";
        internal const string CreateUser = "/api/users";
    }
}
