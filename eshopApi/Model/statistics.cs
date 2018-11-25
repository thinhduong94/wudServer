using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.Model
{
    public class statistics
    {   
        public decimal total { get; set; }
        public List<orderSatisticView> orderSatisticViews { get; set; }
        public List<productSatisView> productSatisViews { get; set; }
    }
    public class orderSatisticView
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public string address { get; set; }
        public int paymethod { get; set; }
        public int? benefit { get; set; }
        public decimal total { get; set; }
    }
    public class productSatisView
    {
        public int id { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string category { get; set; }
        public string band { get; set; }
        public int quantiti { get; set; }
        public int total { get; set; }
    }
    public class statisticsInput
    {
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
    }
}
