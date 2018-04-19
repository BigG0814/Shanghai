namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DailySales")]
    public partial class DailySale
    {
        [Key]
        public int SaleID { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Tip { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? SaleTotal { get; set; }

        public DateTime Date { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
