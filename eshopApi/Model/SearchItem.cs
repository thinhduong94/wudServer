using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.Model
{
    public class SearchItem
    {
        public string name { get; set; }
        public decimal? priceTo { get; set; }
        public decimal? priceFrom { get; set; }
        public int ? band { get; set; }
        public int ? category { get; set; }
        public string size { get; set; }
        public string color { get; set; }
    }
}
