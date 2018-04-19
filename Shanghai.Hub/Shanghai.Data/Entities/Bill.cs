    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace Shanghai.Data.Entities
{
    [Table("Bills")]
    [Serializable]
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
        }

        public int BillID { get; set; }

        public int? OrderID { get; set; }

        public DateTime BillDate { get; set; }

        public int CustomerCount { get; set; }

        public bool PaidYN { get; set; }

        public int EmployeeID { get; set; }

        public int TableNumber { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GST { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillItem> BillItems { get; set; }

        public virtual ICollection<BillPayment> Payments { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Table Table { get; set; }

        public decimal Tip { get; set; }

        public bool isClosed { get; set; }

        public decimal? DeliveryFee { get; set; }
    }
}
