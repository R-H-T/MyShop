// ShoppingCartManager.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MVCShop.Models
{
    public class ShoppingCartManager
    {
        private const string KEY_SHOPPING_CART = "ShoppingCartKey";

        public ShoppingCartManager()
        {
        }

        public static bool RemoveItem(ISession session, OrderProduct item)
        {
            return ShoppingCartManager.RemoveItemById(session, item.Id);
        }

        public static bool RemoveItemById(ISession session, int id)
        {
            var cart = ShoppingCartManager.GetCart(session);
            var product = ShoppingCartManager.ItemById(session, id);

            if (product != null)
            {
                cart.RemoveProductById(id);
                ShoppingCartManager.SaveCart(session, cart);
                return true;
            }

            return false;
        }

        public static string ClearCart(ISession session)
        {
            var cart = ShoppingCartManager.GetCart(session);
            cart.ClearAllItems();
            return ShoppingCartManager.SaveCart(session, cart);
        }

        public static OrderProduct ItemById(ISession session, int id)
        {
            var cart = ShoppingCartManager.GetCart(session);
            var order = cart.Order;
            if (order == null) return null;
            var products = order.OrderProducts;
            if (products == null) return null;
            return products.FirstOrDefault(e => e.Id == id);
        }

        public static OrderProduct IncrementQuanityForItemById(ISession session, int id)
        {
            var cart = ShoppingCartManager.GetCart(session);
            var product = ShoppingCartManager.ItemById(session, id);
            if (product != null)
            {
                var orderProduct = cart.IncrementQuantityForOrderByProductId(id);
                ShoppingCartManager.SaveCart(session, cart);
                return orderProduct;
            }
            return null;
        }

        public static OrderProduct DecrementQuanityForItemById(ISession session, int id)
        {
            var cart = ShoppingCartManager.GetCart(session);
            var product = ShoppingCartManager.ItemById(session, id);
            if (product != null)
            {
                var orderProduct = cart.DecrementQuantityForOrderByProductId(id);
                ShoppingCartManager.SaveCart(session, cart);
                return orderProduct;
            }
            return null;
        }

        public static Dictionary<string, int> GetQuantity(ISession session, int id)
        {

            var cart = ShoppingCartManager.GetCart(session);
            var product = ShoppingCartManager.ItemById(session, id);

            if (product != null)
            {
                var dict = new Dictionary<string, int>();
                dict["quantity"] = product.Quantity;
                return dict;
            }

            return null;
        }

        public static OrderProduct AddItemById(ISession session, int id)
        {
            var cart = ShoppingCartManager.GetCart(session);
            var product = cart.AddProductById(id);

            ShoppingCartManager.SaveCart(session, cart);

            return product;
        }

        public static void SetQuantityForItemById(ISession session, int id, int quantity)
        {
            var cart = ShoppingCartManager.GetCart(session);
            var product = ShoppingCartManager.ItemById(session, id);
            if (product != null)
            {
                // TODO: Use the new methods from the models.
                int index = cart.Order.OrderProducts.IndexOf(product);
                if (index != -1 && cart.Order.OrderProducts.ElementAt(index) != null)
                {
                    cart.Order.OrderProducts[index].SetQuantity(quantity);
                    ShoppingCartManager.SaveCart(session, cart);
                }
            }
        }

        public static ShoppingCart GetCart(ISession session)
        {
            var cart = RestoreCart(session);
            if (cart == null)
            {
                cart = new ShoppingCart(new Order(0));
            }
            return cart;
        }

        public static void AddItem(ISession session, OrderProduct item)
        {
            var cart = ShoppingCartManager.GetCart(session);

            var existingItem = cart.Order.OrderProducts.FirstOrDefault(e => e.Id == item.Id);
            if (existingItem != null)
            {
                int index = cart.Order.OrderProducts.IndexOf(existingItem);
                cart.Order.OrderProducts[index].IncrementQuantity();
            } 
            else
            {
                cart.Order.OrderProducts.Add(item);
            }

            SaveCart(session, cart);
        }

        public static string SaveCart(ISession session, ShoppingCart cart) 
        {
            try
            {
                session.SetString(KEY_SHOPPING_CART, JsonConvert.SerializeObject(cart));
                return "";
            }
            catch (Exception e)
            {
                return String.Concat("Error 2: ", e.Message);
            }
        }

        public static ShoppingCart RestoreCart(ISession session) 
        {
            if (!session.IsAvailable)
                return null;

            var data = session.GetString(KEY_SHOPPING_CART);

            if (data != null)
            {
                try
                {
                    var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(data);
                    return shoppingCart;
                }
                catch (Exception e)
                {
                    throw new Exception(message: "Error 1: " + e.Message);
                }
            }

            return null;
        }
    }
}
