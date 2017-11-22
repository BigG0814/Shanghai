using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.Data.POCOs
{
    public class EmployeeSkillReport
    {
        public string Skill { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Level { get; set; }
        public int _Level
        {
            get
            {
                return Convert.ToInt32(Level);
            }
            set
            {
                Level = Convert.ToString(value);
            }
        }
        public int? YOE { get; set; }
    }
}
