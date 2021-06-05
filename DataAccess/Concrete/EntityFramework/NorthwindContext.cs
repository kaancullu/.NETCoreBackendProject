using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Entity framework üzerinden "DbContext" çağırılır.
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=175.45.2.12"); //Normalde şu şekilde IP adresi girilerek bağlantı kurulur.
            optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; Database = Northwind; Trusted_Connection = true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


    }
}
