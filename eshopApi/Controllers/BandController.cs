using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshopApi.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshopApi.Controllers
{
    [Route("api/band")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly apiContext _context;

        public BandController(apiContext context)
        {
            _context = context;
        }
        // GET: api/Band
        [HttpGet]
        public ActionResult Get()
        {
            var model = _context.band.ToList();
            return Ok(new { data = model });
        }

        // GET: api/Band/5
        [HttpGet("{id}", Name = "GetBand")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Band
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Band/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
