using API.Tests.Tests.Base;
using API_ClassLibrary;
using API_ClassLibrary.Framework;
using API_ClassLibrary.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace API.Tests
{
    [TestFixture]
    class RegressionTests:TestBase
    {
        const string BASE_URL = "https://reqres.in";
        string TestDataFolderLocation = Path.GetFullPath(@"../../../TestData/");

        [Test]
        public async Task GetUsersLists()
        {
            RestResponse res = await API_calls.GetUsersData(BASE_URL);
            int statusCode = (int)res.StatusCode;
            //FW.Log.Info($"Status Code =>{statusCode}");
            Assert.That(statusCode, Is.EqualTo(200));
            Assert.That(res.IsSuccessful, Is.True);
        }

        [Test]
        [TestCase("CreateUser1.json")]
        [TestCase("CreateUser2.json")]
        [TestCase("CreateUser3.json")]
        [Parallelizable(ParallelScope.All)]
        public async Task CreateNewUserAsync(string jsonFileName)
        {
            var PayLoad = Helper.ParseJson<CreateUserReq>(TestDataFolderLocation + jsonFileName);
            RestResponse res = await API_calls.CreateNewUser(BASE_URL, PayLoad);

            int statusCode = (int)res.StatusCode;
            //FW.Log.Info($"Status Code =>{statusCode}");
            Assert.That(statusCode, Is.EqualTo(201));
        }

        [Test]
        public void test1()
        {
            FW.Log.Info("HIIII");
        }
    }
}
