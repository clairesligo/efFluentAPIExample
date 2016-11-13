namespace FluentEFExampleCF.Migrations.Orders
{
    using Models.OrderModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FluentEFExampleCF.Models.OrderModels.OrderContextdb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations/Orders";
        }

        protected override void Seed(OrderContextdb context)
        {
            List<Customer> customers = new List<Customer>();
            customers.AddRange(
                new Customer[] { new Customer()
                {
                    FirstName = "Fred",
                    SecondName = "Flinstone",
                    CustomerOrders =
                                    new HashSet<Order> {
                                        new Order {  item = "item 1",  Qty = 20, OrderDate = DateTime.Now},
                                        new Order {  item = "item 2",  Qty = 30, OrderDate = DateTime.Now},
                                        new Order {  item = "item 3",  Qty = 30, OrderDate = DateTime.Now}
                                    },

                },
                    new Customer()
                    {
                        FirstName = "Bob",
                        SecondName = "Couples",
                        CustomerOrders =
                                    new HashSet<Order> {
                                        new Order {  item = "item 1",  Qty = 20, OrderDate = DateTime.Now},
                                        new Order {  item = "item 2",  Qty = 300, OrderDate = DateTime.Now},
                                        new Order {  item = "item 4",  Qty = 100, OrderDate = DateTime.Now}
                                    },

                    } }
                );
            context.Customers.AddOrUpdate(c => new { c.FirstName, c.SecondName }, customers.ToArray());
            
        }
    }
}
