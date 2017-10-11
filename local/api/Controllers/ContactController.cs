using System;
using cloud.native.contracts;
using Microsoft.AspNetCore.Mvc;

namespace cloud.native.local.api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private static ContactDto _contactDto;

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<ContactDto> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ContactDto Get(Guid id) => _contactDto;

        // POST api/values
        [HttpPost]
        public StatusCodeResult Post([FromBody]ContactDto value)
        {
            _contactDto = value;
            return StatusCode(201);
        }
    }
}
