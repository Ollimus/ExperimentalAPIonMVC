using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using TestAPI.Controllers;
using Newtonsoft.Json;
using System.Net;

namespace TestAPITests.IntegrationTests.APIController
{
    [TestClass]
    public class CustomersControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string baseUrl = Helper.GetUrl();

            var client = new RestClient(baseUrl);

            var request = new RestRequest("api/customers/", Method.GET) { RequestFormat = DataFormat.Json };
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var parsedData = JsonConvert.DeserializeObject(content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
