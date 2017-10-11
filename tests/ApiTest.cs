using System;
using System.Linq;
using System.Threading.Tasks;
using cloud.native.contracts;
using NUnit.Framework;

namespace cloud.native.tests
{
    [TestFixture("http://localhost:9999", TestName="Local")]
    public class ApiTest
    {
        private readonly ApiClient _apiClient;

        public ApiTest(string hostUrl)
        {
            _apiClient = new ApiClient(hostUrl);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _apiClient.Dispose();
        }

        [Test]
        public async Task post_and_get_single()
        {
            var postData = CreateSample1();
            await _apiClient.PostData(postData);
            var getData = await _apiClient.GetData(postData.Id);

            AssertGetDataIsSameAsPostData(getData, postData);
        }

        [Test]
        public async Task post_and_get_multiple()
        {
            var postData1 = CreateSample1();
            await _apiClient.PostData(postData1);

            var postData2 = CreateSample2();
            await _apiClient.PostData(postData2);

            var getData = await _apiClient.GetData();

            AssertGetDataIsSameAsPostData(getData[0], postData1);
            AssertGetDataIsSameAsPostData(getData[1], postData2);
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

        private ContactDto CreateSample1()
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

        private ContactDto CreateSample2()
        {
            return new ContactDto
            {
                Id = Guid.NewGuid(),
                Name = "Caroline Smith",
                Communications = new[]
                {
                   new CommunicationDto
                   {
                       Method = "email",
                       Address = "csmith@sky.com"
                   },
                   new CommunicationDto
                   {
                       Method = "home-phone",
                       Address = "0203 353 35354412"
                   },
                   new CommunicationDto
                   {
                       Method = "mobile-phone",
                       Address = "07777 545 454"
                   }
                }
            };
        }
    }
}