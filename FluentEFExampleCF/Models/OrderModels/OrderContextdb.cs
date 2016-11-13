using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FluentEFExampleCF.Models.OrderModels
{

    public class Customer
    {
        public Customer()
        {
            CustomerOrders = new HashSet<Order>();
        }

        public int CustomerNumber { get; set; }
        public string FirstName{ get; set; }
        public string SecondName { get; set; }

        public virtual ICollection<Order> CustomerOrders { get; set; }

    }

    public class Order
    {
        public int OrderNumber { get; set; }
        public int CustomerOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string  item { get; set; }
        public int Qty { get; set; }
        public virtual Customer Owner { get; set; }
    }


    public class OrderContextdb : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        // This gets run when you update the database
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerNumber);

            modelBuilder.Entity<Customer>().Property(c => c.CustomerNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            // No need for this as it is covered by the statement from the orders back creating a one to many relationship
            // With customer Number being manditory
            //modelBuilder.Entity<Customer>().HasMany(c => c.CustomerOrders);
             

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderNumber)
                .Property(o => o.OrderNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Owner navigation propperty is necessary
            // many customer orders on the cutomer side
            modelBuilder.Entity<Order>()
                .HasRequired(c => c.Owner)
                .WithMany(c => c.CustomerOrders);
                

            base.OnModelCreating(modelBuilder);
        }

        public OrderContextdb() : base("DefaultConnection")
        {
            Database.SetInitializer(new OrderDBInitializer<OrderContextdb>());
            // Pass False if you do not want to drop the database everytime you run the application
            Database.Initialize(true);            

       }
        private class OrderDBInitializer<T> : DropCreateDatabaseAlways<OrderContextdb>
        {

        }

    }

}