using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.Data.POCOs
{
    public class EmployeeBySkills
    {
        public bool Active;
        public string Name;
        public string Phone;
        public int SkillLevel;
        public int? YOE;

        public decimal HourlyWage { get; set; }
        public int ID { get; set; }
        public string SkillName { get; set; }
    }
}
