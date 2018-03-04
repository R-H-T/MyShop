// Order.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCShop.Models
{
    [Serializable]
    public class Order
    {
        // Properties

        public int Id { get; set; }
        public int CustomerId { get => _customer.Id; }
        private Customer _customer { get; set; }
        = new Customer(-1, "John", "Appleseed");
        public long OrderDate { get; set; }
        = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        public string Comments { get; set; }
        public int Status { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
                                  = new List<OrderProduct>();
        public Decimal TotalSum
        { 
            get => OrderProducts.Select(e => e.SubTotal)
                                .Aggregate((decimal)0,
                                           (a, b) => Decimal.Add(a, b));
        }

        public OrderProduct AddProduct(Product product)
        {
            var orderProduct = OrderProduct.OrderProductFromProduct(product);
            var index = OrderProducts.FindIndex(e => e.Id == orderProduct.Id);
            if (index != -1)
            {
                return this.IncrementQuantityByProductId(product.Id);
            }

            this.OrderProducts.Add(orderProduct);

            return orderProduct;
        }

        public OrderProduct RemoveItemBy(int id)
        {
            Product product = ProductManager.Shared.Products.FirstOrDefault(e => e.Id == id);
            var orderProduct = OrderProduct.OrderProductFromProduct(product);
            var index = OrderProducts.FindIndex(e => e.Id == orderProduct.Id);

            if (index != -1)
            {
                this.OrderProducts.RemoveAt(index);
            }

            return orderProduct;
        }

        public void ClearAllItems()
        {
            this.OrderProducts = new List<OrderProduct>();
        }

        public int Count
        {
            get => OrderProducts.Select(e => e.Quantity)
                                .Aggregate(0, (a, b) => a + b);
        }

        public string Currency { get; set; }


        // Initializers

        public Order(int id,
                     string comments = "",
                     List<OrderProduct> products = null,
                     int status = 0,
                     string currency = "USD",
                     long orderDate = -1)
        {
            Id = id;
            if (orderDate != -1) OrderDate = orderDate;
            Status = status;
            Comments = comments;
            OrderProducts = products ?? new List<OrderProduct>();
            Currency = currency;
        }

        public OrderProduct IncrementQuantityByProductId(int id)
        {
            var index = OrderProducts.FindIndex(e => e.Id == id);
            if (index != -1)
            {
                this.OrderProducts[index].IncrementQuantity();
                return this.OrderProducts[index];
            }

            return null;
        }

        public OrderProduct DecrementQuantityByProductId(int id)
        {
            var index = OrderProducts.FindIndex(e => e.Id == id);
            if (index != -1)
            {
                this.OrderProducts[index].DecrementQuantity();
                return this.OrderProducts[index];
            }

            return null;
        }
    }
}
