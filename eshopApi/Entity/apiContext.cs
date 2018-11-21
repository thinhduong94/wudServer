using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshopApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eshopApi.Entity
{
    public class apiContext : DbContext
    {
        public apiContext(DbContextOptions<apiContext> options)
        : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<category> category { get; set; }
        public DbSet<product> product { get; set; }
        public DbSet<productChil> productChil { get; set; }
        public DbSet<band> band { get; set; }
        public DbSet<role> role { get; set; }
        public DbSet<benefit> benefit { get; set; }
        public DbSet<account> account { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<orderDetail> orderDetail { get; set; }
    }
}
