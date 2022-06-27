using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;

namespace PizzaShop.Data
{
    public class PizzaShopContext : DbContext
    {
        public PizzaShopContext (DbContextOptions<PizzaShopContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaShop.Models.Pizza>? Pizza { get; set; }

        public DbSet<PizzaShop.Models.User>? User { get; set; }

        public DbSet<PizzaShop.Models.Order>? Order { get; set; }
        public DbSet<PizzaShop.Models.OrderPizza>? OrderPizza { get; set; }
    }
}
