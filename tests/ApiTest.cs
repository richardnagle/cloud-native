using System;
using cloud.native.contracts;
using NUnit.Framework;

namespace cloud.native.tests
{
    [TestFixture("http://localhost:6666", TestName="Local")]
    public class ApiTest
    {
        private Uri _uri;

        public ApiTest(string url)
        {
            _uri = new Uri(url);
        }

        [Test]
        public void post_and_get()
        {
            var sampleData = CreateSample();
            PostData(sampleData);
            var readData = GetData(sampleData.Id);

            Assert.That(readData.Id, Is.EqualTo(sampleData.Id));
        }

        private ContactDto CreateSample()
        {
            return new ContactDto
            {
                Id = Guid.NewGuid()
            };
        }

        private void PostData(ContactDto dto)
        {}

        private ContactDto GetData(Guid id)
        {
            return new ContactDto();
        }
    }
}