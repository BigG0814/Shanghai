﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    public class CartItem
    {
        public int MenuItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string MenuItemName { get; set; }
    }
}
