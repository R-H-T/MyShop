// ProductController.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVCShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MVCShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> model = ProductManager.Shared.Products;

            return View(model);
        }

        [HttpGet]
        public IActionResult GetFavorite()
        {
            var model = _context.Products.Where(e => e.IsFavorite).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string name) {

            var model = _context.Products.FirstOrDefault(e => e.Name == name);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(string name)
        {
            var itemToDelete = _context.Products.FirstOrDefault(e => e.Name == name);

            if (itemToDelete != null)
            {
                _context.Products.Remove(itemToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
