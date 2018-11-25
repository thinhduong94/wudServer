using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.Model
{
    public class Order
    {
        public int id { get; set; }
        public string username { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public decimal total { get; set; }
        public int paymethod { get; set; }
        public DateTime paydate { get; set; }
        public int? benefit { get; set; }
    }
}
