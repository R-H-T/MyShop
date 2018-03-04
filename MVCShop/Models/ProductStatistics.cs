// ProductStatistics.cs
//
// @Date: 28/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MVCShop.Models
{
    public class ProductStatistics
    {
        public int Id { get; set; }
        public float Ratings { get; set; } = 0;
        public float Popularity { get; set; } = 0;
        public List<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

        public ProductStatistics() {
        }

        private float allRatings()
        {
            if (ProductReviews.Count < 0) return 0;

            Ratings = ProductReviews.Select(e => (float)e.Rating).Aggregate((float) 0, (a, b) => (a + b));

            return 0;
        }
    }
}
