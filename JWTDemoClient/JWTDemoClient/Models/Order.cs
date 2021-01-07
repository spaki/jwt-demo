using System;

namespace JWTDemoClient.Models
{
    public class Order
    {
        public string Customer { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
