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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly apiContext _context;

        public ProductController(apiContext context)
        {
            _context = context;
        }
        // GET: api/Product
        [HttpGet]
        public ActionResult Get()
        {
            var products = _context.product.ToList();
            List<productView> list = new List<productView>();

            foreach(var _proudct in products)
            {
                productView _prod = new productView();
                _prod.id = _proudct.id;
                _prod.name = _proudct.name;
                _prod.price = _proudct.price;
                _prod.imgName = _proudct.imgName;
                _prod.category = _context.category.Where(x => x.id.Equals(_proudct.category_id)).Select(x => new categoryView
                {
                    id = x.id,
                    name = x.name
                }).SingleOrDefault();
                _prod.band = _context.band.Where(x => x.id.Equals(_proudct.band_id)).Select(x => new bandView
                {
                    id = x.id,
                    name = x.name
                }).SingleOrDefault();
                _prod.productChilViews = _context.productChil.Where(x => x.product_id.Equals(_prod.id)).Select(x => new productChilView
                {
                    id = x.id,
                    colorName=x.colorName,
                    colorValue=x.colorValue,
                    sizeName =x.sizeName,
                    sizeValue =x.sizeValue
                }).ToList();
                list.Add(_prod);
            }
            return Ok(list);
        }
        [HttpGet("GetSizeByCategory/{id}", Name = "GetSizeByCategory")]
        public ActionResult GetSizeByCategory(int id)
        {
            var model = (from c in _context.category
                         join p in _context.product on c.id equals p.category_id
                         join pd in _context.productChil on p.id equals pd.product_id
                         where c.id == id
                         group pd by new
                         {
                             pd.sizeName,
                             pd.sizeValue
                         } into rs
                         select new SizeView
                         {
                             sizeName = rs.Key.sizeName,
                             sizeValue = rs.Key.sizeValue
                         });
            return Ok(model);
        }
        [HttpGet("GetColorByCategory/{id}", Name = "GetColorByCategory")]
        public ActionResult GetColorByCategory(int id)
        {
            var model = (from c in _context.category
                         join p in _context.product on c.id equals p.category_id
                         join pd in _context.productChil on p.id equals pd.product_id
                         where c.id == id
                         group pd by new
                         {
                             pd.colorName,
                             pd.colorValue
                         } into rs
                         select new ColorView
                         {
                             colorName = rs.Key.colorName,
                             colorValue = rs.Key.colorValue
                         });
            return Ok(model);
        }
        [HttpGet("GetProductByCategory/{id}", Name = "GetProductByCategory")]
        public ActionResult getProductByCategory(int id)
        {
            var products = _context.product.Where(x=>x.category_id.Equals(id)).ToList();
            List<productView> list = new List<productView>();

            foreach (var _proudct in products)
            {
                productView _prod = new productView();
                _prod.id = _proudct.id;
                _prod.name = _proudct.name;
                _prod.price = _proudct.price;
                _prod.imgName = _proudct.imgName;
                _prod.category = _context.category.Where(x => x.id.Equals(_proudct.category_id)).Select(x => new categoryView
                {
                    id = x.id,
                    name = x.name
                }).SingleOrDefault();
                _prod.band = _context.band.Where(x => x.id.Equals(_proudct.band_id)).Select(x => new bandView
                {
                    id = x.id,
                    name = x.name
                }).SingleOrDefault();
                _prod.productChilViews = _context.productChil.Where(x => x.product_id.Equals(_prod.id)).Select(x => new productChilView
                {
                    id = x.id,
                    colorName = x.colorName,
                    colorValue = x.colorValue,
                    sizeName = x.sizeName,
                    sizeValue = x.sizeValue
                }).ToList();
                list.Add(_prod);
            }
            return Ok(list);
        }
        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult Get(int id)
        {
            var _proudct = _context.product.Find(id);
           
            productView _prod = new productView();
            _prod.id = _proudct.id;
            _prod.name = _proudct.name;
            _prod.band_id = _proudct.band_id;
            _prod.category_id = _proudct.category_id;
                _prod.price = _proudct.price;
                _prod.imgName = _proudct.imgName;
            _prod.category = _context.category.Where(x => x.id.Equals(_proudct.category_id)).Select(x => new categoryView
            {
                id = x.id,
                name = x.name
            }).SingleOrDefault();
            _prod.band = _context.band.Where(x => x.id.Equals(_proudct.band_id)).Select(x => new bandView
            {
                id = x.id,
                name = x.name
            }).SingleOrDefault();
            _prod.productChilViews = _context.productChil.Where(x => x.product_id.Equals(_prod.id)).Select(x => new productChilView
                {
                    id = x.id,
                    colorName = x.colorName,
                    colorValue = x.colorValue,
                    sizeName = x.sizeName,
                    sizeValue = x.sizeValue
                }).ToList();

            return Ok(new { data = _prod });
        }
        [HttpPost("search",Name = "search")]
        public ActionResult search(SearchItem item)
        {
            var products = (from c in _context.category
                            join p in _context.product on c.id equals p.category_id
                            join b in _context.band on p.band_id equals b.id
                            join bd in _context.productChil on p.id equals bd.product_id
                            where (item.category == null || c.id ==item.category )
                            && (item.band == null || b.id == item.band)
                            && (item.color == null || bd.colorValue == item.color)
                            && (item.size == null || bd.sizeValue == item.size)
                            && (item.name == null || p.name.Contains(item.name))
                            && (item.priceFrom ==null || p.price >= item.priceFrom)
                            && (item.priceTo == null || p.price <= item.priceTo)
                            group p by p into rs
                            select rs
                            ).ToList();
            List<productView> list = new List<productView>();

            foreach (var _proudct in products)
            {
                productView _prod = new productView();
                _prod.id = _proudct.Key.id;
                _prod.name = _proudct.Key.name;
                _prod.price = _proudct.Key.price;
                _prod.imgName = _proudct.Key.imgName;
                _prod.category = _context.category.Where(x => x.id.Equals(_proudct.Key.category_id)).Select(x => new categoryView
                {
                    id = x.id,
                    name = x.name
                }).SingleOrDefault();
                _prod.band = _context.band.Where(x => x.id.Equals(_proudct.Key.band_id)).Select(x => new bandView
                {
                    id = x.id,
                    name = x.name
                }).SingleOrDefault();
                _prod.productChilViews = _context.productChil.Where(x => x.product_id.Equals(_prod.id)).Select(x => new productChilView
                {
                    id = x.id,
                    colorName = x.colorName,
                    colorValue = x.colorValue,
                    sizeName = x.sizeName,
                    sizeValue = x.sizeValue
                }).ToList();
                list.Add(_prod);
            }
            return Ok(list);
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult Post(producPostView item)
        {
            product _pro = new product();
            _pro.name = item.name;
            _pro.price = item.price;
            _pro.imgName = item.imgName;
            _pro.category_id = item.category_id;
            _pro.band_id = item.band_id;
            _context.product.Add(_pro);
            _context.SaveChanges();
            List<productChil> _proChilList = new List<productChil>();
            foreach(var i in item.productChilViews)
            {
                productChil _proChil = new productChil();
                _proChil.product_id = _pro.id;
                _proChil.sizeName = i.sizeName;
                _proChil.sizeValue = i.sizeValue;
                _proChil.colorName = i.colorName;
                _proChil.colorValue = i.colorValue;
                _proChilList.Add(_proChil);
            }
            _context.productChil.AddRange(_proChilList);
            _context.SaveChanges();
            return Ok(new { data = _pro});
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, producPostView item)
        {
            var _pro = _context.product.Find(id);
            _pro.id = id;
            _pro.name = item.name;
            _pro.price = item.price;
            _pro.imgName = item.imgName;
            _pro.category_id = item.category_id;
            _pro.band_id = item.band_id;
            
            _context.SaveChanges();

            var listChilOld = _context.productChil.Where(x => x.product_id == id).ToList();
            _context.productChil.RemoveRange(listChilOld);
            _context.SaveChanges();
            List<productChil> _proChilList = new List<productChil>();
            foreach (var i in item.productChilViews)
            {
                productChil _proChil = new productChil();
                _proChil.product_id = _pro.id;
                _proChil.sizeName = i.sizeName;
                _proChil.sizeValue = i.sizeValue;
                _proChil.colorName = i.colorName;
                _proChil.colorValue = i.colorValue;
                _proChilList.Add(_proChil);
            }
            _context.productChil.AddRange(_proChilList);
            _context.SaveChanges();
            return Ok(new { data = _pro });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
