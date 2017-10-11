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
        private static readonly IDictionary<Guid, ContactDto> _contactDtos = new ConcurrentDictionary<Guid, ContactDto>();

        [HttpGet]
        public ContactDto[] Get()
        {
            return _contactDtos.Values.ToArray();
        }

        [HttpGet("{id}")]
        public ContactDto Get(Guid id) => _contactDtos[id];

        [HttpPost]
        public StatusCodeResult Post([FromBody]ContactDto value)
        {
            _contactDtos.Add(value.Id, value);
            return StatusCode(201);
        }
    }
}
