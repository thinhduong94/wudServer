using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshopApi.ModelView
{
    public class AccountView
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public roleView role { get; set; }
    }
    public class roleView
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<benefitView> benefits { get; set; }
    }
    public class benefitView
    {
        public int id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
    }
    public class loginView
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
