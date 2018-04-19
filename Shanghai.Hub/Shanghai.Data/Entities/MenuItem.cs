namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("MenuItems")]
    public partial class MenuItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuItem()
        {
            BillItems = new HashSet<BillItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int MenuItemID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuItemName { get; set; }

        public bool ActiveYN { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal CurrentPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillItem> BillItems { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public bool IncludeInWebMenu { get; set; }

        public bool isCombo { get; set; }

        public bool isComboOption { get; set; }

        public int? AmountOfSelections { get; set; }

        public string Description { get; set; }

    }
}
