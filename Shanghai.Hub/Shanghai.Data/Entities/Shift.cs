using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Shanghai.Data.Entities
{
    
    [Serializable]
    [Table("Shifts")]
    public class Shift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shift()
        {
            ShiftTrades = new HashSet<ShiftTrade>();
        }

        public int ShiftID { get; set; }

        public int EmployeeID { get; set; }

        public int JobTypeID { get; set; }

        public DateTime ShiftDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsPosted { get; set; }

        public bool isFinal { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("JobTypeID")]
        public virtual JobType JobType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftTrade> ShiftTrades { get; set; }


    }
}
