using JWTDemoClient.Models;
using System;
using System.Collections.Generic;

namespace JWTDemoClient.Services
{
    public class OderService
    {
        private readonly Random random = new Random();

        public List<Order> ListAll() => ListOrders();
        public List<Order> ListByCustomer(string customer) => ListOrders(customer);

        private List<Order> ListOrders(string customer = null)
        {
            var j = random.Next(3, 9);
            var result = new List<Order>();

            for (int k = 0; k < j; k++)
            {
                var i = random.Next(1, 100);
                result.Add(new Order { CreatedAt = DateTime.Now.AddHours(i * -1), Customer = string.IsNullOrWhiteSpace(customer) ? $"Customer {i * j:000}" : customer, Total = i * 1.7m * j * 0.9m });
            } 

            return result;
        }
    }
}
