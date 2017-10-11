using cloud.native.contracts;
using Microsoft.AspNetCore.Mvc;

namespace cloud.native.local.api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        //// GET api/values
        //[HttpGet]
        //public IEnumerable<ContactDto> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ContactDto Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public StatusCodeResult Post([FromBody]ContactDto value) => StatusCode(201);
    }
}
