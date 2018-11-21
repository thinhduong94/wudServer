using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.ModelView
{
    public class productView
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imgName { get; set; }
        public int category_id { get; set; }
        public int band_id { get; set; }
        public List<productChilView> productChilViews { get; set; }
        public categoryView category { get; set; }
        public bandView band { get; set; }
    }
    public class categoryView
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class bandView
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class productChilView
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string sizeName { get; set; }
        public string sizeValue { get; set; }
        public string colorName { get; set; }
        public string colorValue { get; set; }
    }
    public class producPostView
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imgName { get; set; }
        public int category_id { get; set; }
        public int band_id { get; set; }
        public List<producPostChilView> productChilViews { get; set; }
    }
    public class producPostChilView
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string sizeName { get; set; }
        public string sizeValue { get; set; }
        public string colorName { get; set; }
        public string colorValue { get; set; }
    }
    public class SizeView
    {
        public string sizeName { get; set; }
        public string sizeValue { get; set; }
    }
    public class ColorView
    {
        public string colorName { get; set; }
        public string colorValue { get; set; }
    }
}
