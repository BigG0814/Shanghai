namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Orders")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        public int? CustomerID { get; set; }

        [Required]
        [StringLength(255)]
        public string Fname { get; set; }

        [Required]
        [StringLength(255)]
        public string LName { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public int OrderTypeID { get; set; }

        [StringLength(255)]
        public string Street { get; set; }

        [StringLength(20)]
        public string AptNumber { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(500)]
        public string SpecialInstructions { get; set; }

        public int? DeliveryDriverID { get; set; }

        public TimeSpan PickupTime { get; set; }
        public DateTime OrderDate { get; set; }

        public bool CompletedYN { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal GST { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? DeliverFee { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Tip { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("OrderID")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [ForeignKey("OrderTypeID")]
        public virtual OrderType OrderType { get; set; }
    }
}
