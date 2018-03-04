// ProductManager.cs
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
    public class ProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
            setup();
        }

        private void setup()
        {
            // Create initial content if table was empty.
            List<Product> model = _context.Products.ToList();

            if (model.Count <= 0)
            {
                var products = ProductManager.InitialData();
                products.ForEach(product =>
                {
                    _context.Products.Add(product);
                });

                _context.SaveChanges();
            }
        }

        public static ProductManager Shared { get => new ProductManager(new ApplicationDbContext()); } // Singleton
        public List<Product> Products { get => _products; }
        public int Count { get => _products.Count; }

        private List<Product> _products {
            get
            {
                return _context.Products.ToList();
            }
        }

        public bool Add(Product product)
        {
            var prevCount = _products.Count;
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return (Count > prevCount);
        }

        private static List<Product> InitialData() {

            var products = new List<Product>();

            products.Add(new Product(name: "Awesome Product 1",
                                     price: (decimal)99.99,
                                     description: "The best deal on the market.",
                                     isFavorite: false,
                                     discountPercent: (float)1.20,
                                     categories: null,
                                     imagePath: "https://picsum.photos/640/412?image=20"));
            
            products.Add(new Product(name: "Awesome Product 2",
                                     price: (decimal)99.99,
                                     description: null,
                                     isFavorite: false,
                                     discountPercent: 0,
                                     categories: null,
                                     imagePath: "https://picsum.photos/640/412?image=10"));

            products.Add(new Product(name: "Super Product",
                                     price: (decimal)88.45,
                                     description: null,
                                     isFavorite: false,
                                     discountPercent: 0,
                                     categories: null,
                                     imagePath: "https://picsum.photos/640/412?image=23"));

            products.Add(new Product(name: "Marvelous Product",
                                     price: (decimal)59.50,
                                     description: null,
                                     isFavorite: false,
                                     discountPercent: 0,
                                     categories: null,
                                     imagePath: "https://picsum.photos/640/412?image=24"));
            
            products.Add(new Product(name: "Gorgeous Product",
                                     price: (decimal)123.95,
                                     description: null,
                                     isFavorite: false,
                                     discountPercent: 0,
                                     categories: null,
                                     imagePath: "https://picsum.photos/640/412?image=26"));
            
            return products;
        }

        public Product ItemById(int id)
        {
            var product = Products.FirstOrDefault(e => e.Id == id);

            return product;
        }
    }
}
