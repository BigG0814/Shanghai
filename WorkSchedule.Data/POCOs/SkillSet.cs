using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.Data.POCOs
{
    public class SkillSet
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int SkillLevel { get; set; }
        public int? YOE { get; set; }
        public decimal HourlyWage { get; set; }
    }
}
