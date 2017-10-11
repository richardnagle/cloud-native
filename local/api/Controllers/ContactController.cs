using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using cloud.native.contracts;
using Microsoft.AspNetCore.Mvc;

namespace cloud.native.local.api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private static readonly IList<ContactDto> _contactDtos = new List<ContactDto>();

        [HttpGet]
        public ContactDto[] Get()
        {
            return _contactDtos.ToArray();
        }

        [HttpGet("{id}")]
        public ContactDto Get(Guid id) => _contactDtos.SingleOrDefault(dto => dto.Id == id);

        [HttpPost]
        public StatusCodeResult Post([FromBody]ContactDto value)
        {
            _contactDtos.Add(value);
            return StatusCode(201);
        }

        [HttpDelete]
        public void Delete()
        {
            _contactDtos.Clear();
        }
    }
}
