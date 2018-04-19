namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemID { get; set; }

        public int ShoppingCartID { get; set; }

        public int MenuItemID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("MenuItemID")]
        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("ShoppingCartID")]
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
