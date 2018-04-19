namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillItems")]
    [Serializable]
    public partial class BillItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillItemID { get; set; }
        public int BillID { get; set; }

        public int MenuItemID { get; set; }

        public decimal SellingPrice { get; set; }

        [StringLength(50)]
        public string Notes { get; set; }

        [ForeignKey("BillID")]
        public virtual Bill Bill { get; set; }

        [ForeignKey("MenuItemID")]
        public virtual MenuItem MenuItem { get; set; }

        public int Split { get; set; }

        public string ItemName { get; set; }

    }
}
