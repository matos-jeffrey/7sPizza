using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaEntity
{
    public class SevensDBContext : DbContext
    {
        public SevensDBContext(DbContextOptions<SevensDBContext> context)
            : base(context)
        {
        }

        //protected override void onconfiguring(dbcontextoptionsbuilder optionsbuilder)
        //{
        //    if (!optionsbuilder.isconfigured)
        //    {
        //        optionsbuilder.usesqlserver(@"Server=tcp:team7pizza.database.windows.net,1433;Initial Catalog=team7pizzaDB;Persist Security Info=False;User ID=team7;Password=RevatureQNS1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    }
        //}

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Topping> Topping { get; set; }
    }
}
