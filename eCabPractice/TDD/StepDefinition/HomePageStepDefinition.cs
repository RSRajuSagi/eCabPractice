using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using eCabPractice.Models;

namespace eCabPractice.TDD.StepDefinition
{
    [Binding]
    public partial class HomePageStepDefinition
    {

        HttpClient client = new HttpClient();
        private string _url;
        private string _eurl;
        private string _eurlUsers;

        public HomePageStepDefinition()
        {


        }

        [Given(@"Post on website  '(.*)'")]
        public void GivenPostOnWebsite(string eWebsite)
        {
            ScenarioContext.Current["eWebsite"] = eWebsite;
        }

        [When(@"Payload with email '(.*)' and password '(.*)'  is entered")]
        public void WhenPayloadWithEmailAndPasswordIsEntered(string eEmail, string ePassword)
        {
            var request = new Request
            {
                email = eEmail,
                password = ePassword
            };
            var serializedItemToCreate = JsonConvert.SerializeObject(request);
            var requests = new HttpRequestMessage(HttpMethod.Post, _url);
        }

        [Then(@"response '(.*)'of  token entered email '(.*)' and  password  '(.*)' is shown")]
        public void ThenResponseOfTokenEnteredEmailAndPasswordIsShown(string expectedTokenResponse, string eEmail, string ePassword)
        {
            _eurl = "https://reqres.in/api/register";
            ScenarioContext.Current["expectedTokenResponse"] = expectedTokenResponse;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var requests = new HttpRequestMessage(HttpMethod.Post, _eurl);
            var request = new Request
            {
                email = eEmail,
                password = ePassword
            };
            var serializedItemToCreate = JsonConvert.SerializeObject(request);
            requests.Content = new StringContent(serializedItemToCreate, System.Text.Encoding.Unicode,
               "application/json");
            var response = client.SendAsync(requests).Result;
            string actualTokenResponse = response.StatusCode.ToString();

            if (string.Compare(actualTokenResponse, expectedTokenResponse, true) == 0)
                try
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            else if (string.Compare(actualTokenResponse, expectedTokenResponse, true) == 0)
                try
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
        }

        [Given(@"list of users")]
        public void GivenListOfUsers()
        {
            _eurlUsers = "https://reqres.in/api/users";
        }

        [When(@"no payload")]
        public void WhenNoPayload()
        {
                       
            var requests = new HttpRequestMessage(HttpMethod.Post, _url);
        }

        [Then(@"the  response of list users")]
        public void ThenTheResponseOfListUsers()
        {
     
                var _eurlUsers = "https://reqres.in/api/users";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Get, _eurlUsers);
                var response = client.SendAsync(request).Result;
                var content = response.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<GetUsersResponse>(content);
            try
            {
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }



}

    




