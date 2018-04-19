namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employees")]
    [Serializable]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Bills = new HashSet<Bill>();
            DailySales = new HashSet<DailySale>();
            Shifts = new HashSet<Shift>();
            Orders = new HashSet<Order>();
            ShiftTrades = new HashSet<ShiftTrade>();
            ShiftTrades1 = new HashSet<ShiftTrade>();
            ShiftTrades2 = new HashSet<ShiftTrade>();
            TimeOffRequests = new HashSet<TimeOffRequest>();
        }

        public int EmployeeID { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FName + " " + LName;
            }
        }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(255)]
        public string FName { get; set; }

        [StringLength(255)]
        public string LName { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255)]
        public string Street { get; set; }

        [StringLength(255)]
        public string City { get; set; }
 
        [StringLength(2)]
        public string Province { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(7)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Hire Date is required.")]
        public DateTime HireDate { get; set; }

        [StringLength(12)]
        public string HomePhone { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string CellPhone { get; set; }

        public int? CellularProviderID { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Contact Phone Number is required.")]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "ContactRelation is required.")]
        public string ContactRelation { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "ContactName is required.")]
        public string ContactName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        
        public int LoginCode { get; set; }

        public bool ActiveYN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }

        public virtual CellularProvider CellularProvider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailySale> DailySales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shift> Shifts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftTrade> ShiftTrades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftTrade> ShiftTrades1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftTrade> ShiftTrades2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeOffRequest> TimeOffRequests { get; set; }
    }
}
