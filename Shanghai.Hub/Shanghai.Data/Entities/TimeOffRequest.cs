namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeOffRequests")]
    public partial class TimeOffRequest
    {
        [Key]
        public int RequestID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime DatePosted { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
