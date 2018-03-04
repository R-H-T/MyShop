// ProductReview.cs
//
// @Date: 28/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCShop.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string Title { get; set; } = "No title";
        public string Auhtor { get; set; } = "Anonymous";
        public string Comment { get; set; } = "No Desc";
        public float Rating { get; set; } = 0;

        public ProductReview()
        {
        }
    }
}
