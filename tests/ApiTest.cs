using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using cloud.native.contracts;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cloud.native.tests
{
    [TestFixture("http://localhost:9999", TestName="Local")]
    public class ApiTest
    {
        private readonly HttpClient _api;

        public ApiTest(string hostUrl)
        {
            _api = new HttpClient
            {
                BaseAddress = new Uri(hostUrl)
            };
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _api?.Dispose();
        }

        [Test]
        public async Task post_and_get()
        {
            var postData = CreateSample();
            await PostData(postData);
            var getData = await GetData(postData.Id);

            AssertGetDataIsSameAsPostData(getData, postData);
        }

        private void AssertGetDataIsSameAsPostData(ContactDto getData, ContactDto postData)
        {
            Assert.That(getData.Id, Is.EqualTo(postData.Id), "Incorrect Id");
            Assert.That(getData.Name, Is.EqualTo(postData.Name), "Incorrect Name");

            Assert.That(
                getData.Communications.Select(x => x.Address).ToArray(),
                Is.EqualTo(postData.Communications.Select(x => x.Address).ToArray()),
                "Incorrect Communications.Addresses");

            Assert.That(
                getData.Communications.Select(x => x.Method).ToArray(),
                Is.EqualTo(postData.Communications.Select(x => x.Method).ToArray()),
                "Incorrect Communications.Methods");
        }

        private ContactDto CreateSample()
        {
            return new ContactDto
            {
                Id = Guid.NewGuid(),
                Name = "Bob Jones",
                Communications = new[]
                {
                   new CommunicationDto
                   {
                       Method = "email",
                       Address = "bob-jones@internet.com"
                   },
                   new CommunicationDto
                   {
                       Method = "post",
                       Address = "10 Meadow Cottages, Little Kingshill, Bucks"
                   }
                }
            };
        }

        private async Task PostData(ContactDto dto)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(dto) , Encoding.UTF8, "application/json");

            var resp = await _api.PostAsync("api/contact", content);

            Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Incorrect status code on POST");
        }

        private async Task<ContactDto> GetData(Guid id)
        {
            var resp = await _api.GetAsync($"api/contact/{id}");

            Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Incorrect status code on GET");

            var jsonContent = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ContactDto>(jsonContent);
        }
    }
}