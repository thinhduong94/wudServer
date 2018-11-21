using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.Model
{
    public class product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imgName { get; set; }
        public int category_id { get; set; }
        public int band_id { get; set; }
    }
}
