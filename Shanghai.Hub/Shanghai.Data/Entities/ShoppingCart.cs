namespace Shanghai.Data.Entities {
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int ShoppingCartID { get; set; }
        //public int OnlineCustomerID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        //public bool OrderOpen { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
