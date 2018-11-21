using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.ModelView
{
    public class OrderView
    {
        
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

    }
}
