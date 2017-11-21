using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.Data.POCOs
{
    public class EmployeeBySkills
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public int SkillLevel { get; set; }
        public int? YOE { get; set; }
        public decimal HourlyWage { get; set; }
        
    }
}
