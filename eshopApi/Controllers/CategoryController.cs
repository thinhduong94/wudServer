using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshopApi.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshopApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly apiContext _context;

        public CategoryController(apiContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public ActionResult Get()
        {
            var model = _context.category.ToList();
            return Ok(new {data=model});
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "GetCategory")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Category
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Category/5
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
