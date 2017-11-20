namespace WorkSchedule.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Skills
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Skills()
        {
            EmployeeSkills = new HashSet<EmployeeSkills>();
        }

        [Key]
        public int SkillID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public bool RequiresTicket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSkills> EmployeeSkills { get; set; }
    }
}
