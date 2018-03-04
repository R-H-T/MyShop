// IInventory.cs
//
// @Date: 1/3/2018
// @Auhtor: Roberth Hansson-Tornéus (Gawee.Narak@gmail.com)
// @Copyright: ©2018 – Roberth Hansson-Tornéus, All rights reserved.
//

using System;
using System.Collections.Generic;

namespace MVCShop.Models.Interfaces.Inventory
{
    public interface IInventory<Element>
    {
        // Properties

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        Element Items { get; set; }


        // Methods

        /// <summary>
        /// Gets the stocks by identifiers.
        /// </summary>
        /// <returns>The stocks by identifiers.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<int, int> GetStocksByIdentifiers(int[] id);

        /// <summary>
        /// Gets the stock by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        void GetStockById(int id);
    }
}
