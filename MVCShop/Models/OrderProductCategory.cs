// OrderProductCategory.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;

namespace MVCShop
{
    [Serializable]
    public class OrderProductCategory
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        public OrderProductCategory(string name, string desc = "")
        {
            Name = name;
            Desc = desc;
        }

        public OrderProductCategory(ProductCategory category)
        {
            Name = category.Name;
            Desc = category.Desc;
        }
    }
}
