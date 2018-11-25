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
            var model = (from o in _context.Order
                         select new OrderView
                         {
                             id = o.id,
                             username = o.username,
                             address = o.address,
                             date = o.paydate,
                             paymethod = o.paymethod,
                             phone = o.phone,
                             total = o.total,
                             orderDetailViews = (from d in _context.orderDetail
                                                 join p in _context.product on d.product_id equals p.id
                                                 where d.order_id == o.id
                                                 select new OrderDetailView
                                                 {
                                                     id = d.id,
                                                     image = p.imgName,
                                                     price = p.price,
                                                     quantiti = d.quatity
                                                 }).ToList()
                        }).ToList();

            return Ok(model);
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
        public ActionResult Get(int id)
        {
            var model = (from o in _context.Order
                         where o.id == id
                         select new OrderView
                         {
                             id = o.id,
                             username = o.username,
                             address = o.address,
                             date = o.paydate,
                             paymethod = o.paymethod,
                             phone = o.phone,
                             total = o.total,
                             orderDetailViews = (from d in _context.orderDetail
                                                 join p in _context.product on d.product_id equals p.id
                                                 where d.order_id == o.id
                                                 select new OrderDetailView
                                                 {
                                                     id = d.id,
                                                     image = p.imgName,
                                                     name = p.name,
                                                     price = p.price,
                                                     quantiti = d.quatity
                                                 }).ToList()
                         }).FirstOrDefault();

            return Ok(model);
        }
        [HttpPost("statistics", Name = "statistics")]
        public ActionResult statistics(statisticsInput input)
        {
            statistics s = new statistics();
             var Order = (from o in _context.Order
                         where (input.dateFrom == null || input.dateFrom <= o.paydate)
                         && (input.dateTo == null || input.dateTo >= o.paydate)
                         select new orderSatisticView
                         {
                             id = o.id,
                             date = o.paydate,
                             username = o.username,
                             address = o.address,
                             paymethod = o.paymethod,
                             total = o.total

                         }).ToList();
            s.orderSatisticViews = Order;
            s.total = Order.Sum(x => x.total);
            List<productSatisView> pros = new List<productSatisView>();
            foreach(var item in Order)
            {
                var prod = (from od in _context.orderDetail
                            join p in _context.product on od.product_id equals p.id
                            where od.order_id == item.id
                            select new productSatisView
                            {
                                id = p.id,
                                name = p.name,
                                img = p.imgName,
                                band = p.band_id+"",
                                category = p.category_id+"",
                                quantiti = od.quatity
                            }).ToList();
                foreach(var i in prod)
                {
                    int dem = 0;
                    foreach(var j in pros)
                    {
                        if(i.id == j.id)
                        {
                            dem = 1;
                            j.total += i.quantiti;

                        }
                    }
                    if (dem == 0)
                    {
                        i.total = i.quantiti;
                        pros.Add(i);
                    }
                }
            }

            s.productSatisViews = pros;
            return Ok(s);
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
