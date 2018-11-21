using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshopApi.Entity;
using eshopApi.Model;
using eshopApi.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshopApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly apiContext _context;

        public OrderController(apiContext context)
        {
            _context = context;
        }
        // GET: api/Order
        [HttpGet]
        public ActionResult Get()
        {

            return Ok();
        }
        [HttpGet("getBenefit/{id}",Name = "getBenefit")]
        public ActionResult getBenefit(string id)
        {
            var benefit = (from u in _context.account
                           join r in _context.role on u.role_id equals r.id
                           join b in _context.benefit on r.id equals b.rode_id
                           where u.username.Equals(id)
                           select b).FirstOrDefault();
            return Ok(benefit);
        }
        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Order
        [HttpPost]
        public ActionResult Post(OrderViewPost item)
        {
            Order order = new Order();
            order.address = item.address;
            order.phone = item.phone;
            order.username = item.username;
            order.paymethod = item.paymethod;
            order.total = 0;
            decimal valueBenefit = 0;
            var user = _context.account.Where(x => x.username == item.username).FirstOrDefault();
            if (user != null)
            {
                var benefit = (from u in _context.account
                               join r in _context.role on u.role_id equals r.id
                               join b in _context.benefit on r.id equals b.rode_id
                               where u.username.Equals(item.username)
                               select b).FirstOrDefault();
                if(benefit != null)
                {
                    valueBenefit = benefit.value;
                }
            }
            foreach (var de in item.detail)
            {
                var pro = _context.product.Find(de.product_id);
                if (pro != null)
                {
                    order.total += (de.quatity * pro.price);      
                }
            }
            order.total = order.total - (order.total * valueBenefit)/100;
            order.paydate = DateTime.Now;
            _context.Order.Add(order);
            _context.SaveChanges();
            List<orderDetail> detailList = new List<orderDetail>();
            foreach (var de in item.detail)
            {
                orderDetail od = new orderDetail();
                var pro = _context.product.Find(de.product_id);
                if(pro != null)
                {
                    od.order_id = order.id;
                    od.product_id = de.product_id;
                    od.quatity = de.quatity;
                    od.price = pro.price;
                    detailList.Add(od);
                }
                
            }
            _context.orderDetail.AddRange(detailList);
            _context.SaveChanges();
            return Ok(order);

        }

        // PUT: api/Order/5
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
