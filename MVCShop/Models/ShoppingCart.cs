// ShoppingCart.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
namespace MVCShop.Models
{
    [Serializable]
    public class ShoppingCart
    {
        // Properties

        public Order Order { get; set; }
        public float VAT { get => _vat; }
        private float _vat { get; set; }
        public decimal TotalSum { get => Order.TotalSum; }
        public decimal TotalSumExclVAT { 
            get => Decimal.Divide(TotalSum, (decimal)_vat);
        }
        public int Count { get => Order.Count; }


        // Initializers

        public ShoppingCart(Order order, float vat = (float)1.25)
        {
            Order = order;
            _vat = vat;
        }


        // Methods

        public OrderProduct AddProduct(Product product)
        {
            return this.Order.AddProduct(product);
        }

        public OrderProduct AddProductById(int id)
        {
            var product = ProductManager.Shared.ItemById(id);

            if (product != null)
            {
                return this.AddProduct(product);
            }

            return null;
        }


        public OrderProduct RemoveProductById(int id)
        {
            var product = ProductManager.Shared.ItemById(id);

            if (product != null)
            {
                return this.Order.RemoveItemBy(id);
            }

            return null;
        }

        public OrderProduct IncrementQuantityForOrderByProductId(int id)
        {
            return Order.IncrementQuantityByProductId(id);
        }

        internal void ClearAllItems()
        {
            Order.ClearAllItems();
        }


        public OrderProduct DecrementQuantityForOrderByProductId(int id)
        {
            return Order.DecrementQuantityByProductId(id);
        }
    }
}
