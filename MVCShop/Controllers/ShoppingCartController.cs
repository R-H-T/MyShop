// ShoppingCartController.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCShop.Models;

namespace MVCShop.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {

        // GET: api/shoppingcart
        [HttpGet]
        public JsonResult Index()
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.GetCart(session) ?? new object());
        }

        // GET api/shoppingcart/clear
        [HttpGet("clear/")]
        public JsonResult ClearCart()
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.ClearCart(session) ?? new object());
        }

        // GET api/shoppingcart/items
        [HttpGet("items/")]
        public JsonResult GetAllItems()
        {
            var session = HttpContext.Session;
            var item = ShoppingCartManager.GetCart(session);
            return Json(item?.Order?.OrderProducts ?? new object());
        }

        // GET api/shoppingcart/items/5
        [HttpGet("items/{id}")]
        public JsonResult GetItemById(int id)
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.ItemById(session, id) ?? new object());
        }

        // POST api/shoppingcart/discounts/add
        [HttpPost("discounts/{code}/add")]
        public void AddDiscount([FromBody]string discountCode)
        {
        }

        // GET api/shoppingcart/items/5/remove
        [HttpGet("items/{id}/remove")]
        public JsonResult RemoveItem(int id)
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.RemoveItemById(session, id));
        }

        // GET api/shoppingcart/items/5/add
        [HttpGet("items/{id}/add")]
        public JsonResult AddItem(int id)
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.AddItemById(session, id) ?? new object());
        }

        // GET api/shoppingcart/items/5/qty
        [HttpGet("items/{id}/qty")]
        public JsonResult GetQuantityForItem(int id)
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.GetQuantity(session, id) ?? new object());
        }

        // GET api/shoppingcart/items/5/qty/increment
        [HttpGet("items/{id}/qty/increment")]
        public JsonResult IncrementQuantityForItem(int id)
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.IncrementQuanityForItemById(session, id) ?? new object());
        }

        // GET api/shoppingcart/items/5/qty/decrement
        [HttpGet("items/{id}/qty/decrement")]
        public JsonResult DecrementQuantityForItem(int id)
        {
            var session = HttpContext.Session;
            return Json(ShoppingCartManager.DecrementQuanityForItemById(session, id) ?? new object());
        }

        // GET api/shoppingcart
        [HttpGet("discounts/{code}/remove")]
        public string RemoveDiscount(string code)
        {
            return "Removing discount for discount code:" + code;
        }
    }
}
