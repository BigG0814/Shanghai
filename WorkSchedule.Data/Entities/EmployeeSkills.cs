namespace WorkSchedule.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeeSkills
    {
        [Key]
        public int EmployeeSkillID { get; set; }

        public int EmployeeID { get; set; }

        public int SkillID { get; set; }

        public int Level { get; set; }

        public int? YearsOfExperience { get; set; }

        [Column(TypeName = "money")]
        public decimal HourlyWage { get; set; }

        public virtual Employees Employees { get; set; }

        public virtual Skills Skills { get; set; }
    }
}
