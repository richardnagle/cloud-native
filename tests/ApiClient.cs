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
    public class ApiClient: IDisposable
    {
        private readonly HttpClient _api;

        public ApiClient(string hostUrl)
        {
            _api = new HttpClient
            {
                BaseAddress = new Uri(hostUrl)
            };
        }

        public async Task PostData(ContactDto dto)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(dto) , Encoding.UTF8, "application/json");

            var resp = await _api.PostAsync("api/contact", content);

            Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Incorrect status code on POST");
        }

        public async Task<ContactDto> GetData(Guid id)
        {
            return await Get<ContactDto>($"api/contact/{id}");
        }

        public async Task<ContactDto[]> GetData()
        {
            return await Get<ContactDto[]>("api/contact");
        }

        private async Task<T> Get<T>(string resource)
        {
            var resp = await _api.GetAsync(resource);

            Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Incorrect status code on GET");

            var jsonContent = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        public void Dispose()
        {
            _api?.Dispose();
        }
    }
}