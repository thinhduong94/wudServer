using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshopApi.Entity;
using eshopApi.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshopApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly apiContext _context;

        public AccountController(apiContext context)
        {
            _context = context;
        }
        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "getAccount")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost("login",Name ="login")]
        public ActionResult login(loginView value)
        {
            var model = _context.account.Where(x => x.username.Equals(value.username) && x.password.Equals(value.password)).FirstOrDefault();
            DateTime now = DateTime.Now;
            if (model != null)
            {
                var data = (from a in _context.account
                            join r in _context.role on a.role_id equals r.id
                            where a.id == model.id
                            select new AccountView
                            {
                                id = a.id,
                                username = a.username,
                                email = a.email,
                                role = new roleView
                                {
                                    id = r.id,
                                    name = r.name,
                                    benefits = (from b in _context.benefit
                                                where b.rode_id == r.id
                                                && (b.dateForm == null || b.dateForm <= now)
                                                && (b.dateTo == null || b.dateTo >= now)
                                                select new benefitView
                                                {
                                                    id = b.id,
                                                    name = b.name,
                                                    value = b.value
                                                }).ToList()
                                }
                            }).FirstOrDefault();
                return Ok(data);
            }
            return Ok(null);

        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Account/5
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
