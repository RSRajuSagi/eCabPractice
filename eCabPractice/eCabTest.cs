using eCabPractice.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace eCabPractice
{
    [TestClass]
    public class eCabTest
    {
        [TestMethod]
        public void Register_Given_Email_Return_Response_GoodRequest()
        {
            try
            {
                var url = "https://reqres.in/api/register";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new Request
                {
                    email = "eve.holt@reqres.in",
                    password = "pistol"
                };
                var serializedItemToCreate = JsonConvert.SerializeObject(request);
                var requests = new HttpRequestMessage(HttpMethod.Post, url);
                requests.Content = new StringContent(serializedItemToCreate, System.Text.Encoding.Unicode,
                    "application/json");
                var response = client.SendAsync(requests).Result;

                Assert.AreEqual(response.StatusCode,HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void Register_Given_Email_Return_Response_BadRequest()
        {
            try
            {
                var url = "https://reqres.in/api/register";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new Request
                {
                    email = "sydney@fife",                              
                
                };
                var serializedItemToCreate = JsonConvert.SerializeObject(request);
                var requests = new HttpRequestMessage(HttpMethod.Post, url);
                requests.Content = new StringContent(serializedItemToCreate, System.Text.Encoding.Unicode,
                    "application/json");
                var response = client.SendAsync(requests).Result;

                Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void Get_All_Users_Return_List()
        {
            try
            {
                var url = "https://reqres.in/api/users";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = client.SendAsync(request).Result;
                var content = response.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<GetUsersResponse>(content);
                 Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
