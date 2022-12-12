using FluentApi.Domain.Entities;
using FluentApi.Domain.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi.DataAccess.EFrameworkServer
{
    public class MyContext:DbContext
    {
        public MyContext():base("StoreDb")
        {
            //this.Database.Connection.Con
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderMap());

            modelBuilder.Entity<Order>()
                .HasOptional<Customer>(s => s.Customer)
                .WithMany()
                .WillCascadeOnDelete();
        }
    }
}
