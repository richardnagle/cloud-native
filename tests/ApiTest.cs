using System;
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
            var sampleData = CreateSample();
            await PostData(sampleData);
            var readData = await GetData(sampleData.Id);

            Assert.That(readData.Id, Is.EqualTo(sampleData.Id));
        }

        private ContactDto CreateSample()
        {
            return new ContactDto
            {
                Id = Guid.NewGuid()
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
            return new ContactDto();
        }
    }
}