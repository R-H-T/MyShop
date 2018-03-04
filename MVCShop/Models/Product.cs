// Product.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCShop.Models
{
    [Serializable]
    public class Product
    {
        // Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsFavorite { get; set; }
        public decimal Price { get; set; } // TODO: Handle price for region.
        public float DiscountPercent { get; set; } = 0;
        public decimal DiscountedPrice {
            get
            {
                Decimal result;
                result = (HasDiscount) ? Decimal.Divide(Price, (decimal)DiscountPercent) : Price;
                return result;
            }
        }
        public bool HasDiscount { get => (DiscountPercent > 0); }

        public List<ProductCategory> Categories { get; set; }

        public string Comments { get; set; }
        public string ImagePath { get; set; } = "";
        //public ProductStatistics Statistics { get; set; } = new ProductStatistics();
        public bool OnSale { get; set; } = true;
        public bool InStock { get; set; } = true; // TODO: Make it check the inventory.
        // TODO: Add individual VAT rate. Checks from VAT Region which checks rate by category.


        // Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MVCShop.Models.Product"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="price">Price.</param>
        /// <param name="description">Description.</param>
        /// <param name="isFavorite">If set to <c>true</c> is favorite.</param>
        /// <param name="discountPercent">Discount percent.</param>
        /// <param name="categories">Categories.</param>
        /// <param name="imagePath">Image path.</param>
        public Product(string name,
                       decimal price, 
                       string description = "", 
                       bool isFavorite = false,
                       float discountPercent = 0,
                       List<ProductCategory> categories = null,
                       string imagePath = "")
        {
            Name = name;
            Price = price;
            Desc = description;
            IsFavorite = isFavorite;
            DiscountPercent = discountPercent;
            Categories = categories;
            ImagePath = imagePath;
        }

        public Product()
        {
            
        }


        // Methods

        public static bool Create(string name, 
                                  decimal price, 
                                  string description = "", 
                                  bool isFavorite = false,
                                  float discountPercent = 0,
                                  List<ProductCategory> categories = null,
                                  string imagePath = "")
        {
            return ProductManager.Shared.Add(new Product(name: name,
                                                         price: price,
                                                         description: description,
                                                         isFavorite: isFavorite,
                                                         discountPercent: discountPercent,
                                                         categories: categories,
                                                         imagePath: imagePath));
        }

        public static List<Product> GetAll()
        {
            return ProductManager.Shared.Products ?? new List<Product>();
        }
    }
}
