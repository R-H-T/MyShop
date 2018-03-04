// OrderProduct.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCShop.Models
{
    [Serializable]
    public class OrderProduct
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice
        {
            get
            {
                Decimal result;
                result = (HasDiscount) ? Decimal.Divide(Price, (decimal)DiscountPercent) : Price;
                return result;
            }
        }
        public float DiscountPercent { get; set; } = 0;
        public bool HasDiscount { get => (DiscountPercent > 0); }
        public List<OrderProductCategory> Categories { get; set; } = new List<OrderProductCategory>();
        public string ImagePath { get; set; } = "";
        public int Quantity { get; set; } = 1;
        public decimal SubTotal { get => this._subTotal(); }
        private decimal _subTotal()
        {
            Decimal price =  (DiscountedPrice != Price) ?  DiscountedPrice : Price;
            Decimal result = Decimal.Multiply(Quantity, price);

            return result;
        }


        // Initializers

        public OrderProduct(int id,
                            string name,
                            decimal price, 
                            string description = "",
                            float discountPercent = 0,
                            List<OrderProductCategory> categories = null,
                            string imagePath = null)
        {
            Id = id;
            Name = name;
            Price = price;
            Desc = description;
            DiscountPercent = discountPercent;
            Categories = categories;
            ImagePath = imagePath;
        }


        // Methods

        public static OrderProduct OrderProductFromProduct(Product product)
        {
            List<OrderProductCategory> categories = null;
            if (product.Categories != null)
            {
                categories = (List<OrderProductCategory>) product
                    .Categories
                    .Select(e => new OrderProductCategory(e));
            }

            return new OrderProduct(product.Id,
                                    product.Name,
                                    product.Price,
                                    product.Desc,
                                    product.DiscountPercent,
                                    categories,
                                    product.ImagePath);
        }

        public void DecrementQuantity()
        {
            if (this.Quantity > 1)
            {
                this.Quantity -= 1;
            }
            else
            {
                this.Quantity = 1;
            }
        }

        public void SetQuantity(int quantity)
        {
            this.Quantity = (quantity > 0) ? quantity : this.Quantity = 1;
        }

        public void IncrementQuantity()
        {
            if (this.Quantity < int.MaxValue)
            {
                this.Quantity += 1;
            }
        }
    }
}
