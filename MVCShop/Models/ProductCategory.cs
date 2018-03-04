// ProductCategory.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.ComponentModel.DataAnnotations;

namespace MVCShop
{
    [Serializable]
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public ProductCategory(string name, string desc = "")
        {
            Name = name;
            Desc = desc;
        }
    }
}
