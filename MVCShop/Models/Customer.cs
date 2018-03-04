// Customer.cs
//
// @Date: 25/2/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;

namespace MVCShop.Models
{
    [Serializable]
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => String.Join(" ", FirstName, LastName); }
        public List<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();

        public Customer(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
