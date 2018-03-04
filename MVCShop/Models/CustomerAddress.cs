// CustomerAddress.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;

namespace MVCShop.Models
{
    [Serializable]
    public class CustomerAddress
    {
        public Customer Customer { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public bool IsMain { get; set; }
    }
}