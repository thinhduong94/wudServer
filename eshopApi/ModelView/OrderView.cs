using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.ModelView
{
    public class OrderView
    {
        public int id { get; set; }
        public string username { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int paymethod { get; set; }
        public int? benefit { get; set; }
        public decimal total { get; set; }
        public DateTime date { get; set; }
        public List<OrderDetailView> orderDetailViews { get; set; }
    }
    public class OrderDetaiViewPost
    {
        public int product_id { get; set; }
        public int quatity { get; set; }
        
    }
    public class OrderViewPost
    {
        public int id { get; set; }
        public string username { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int paymethod { get; set; }
        public List<OrderDetaiViewPost> detail { get; set; }
    }
    public class OrderDetailView
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal price { get; set; }
        public int quantiti { get; set; }
    }
}
