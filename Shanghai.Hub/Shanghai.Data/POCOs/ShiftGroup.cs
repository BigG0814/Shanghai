using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    public class ShiftGroup
    {
        public int JobType { get; set; }
        public string JobDescription { get; set; }
        public DateTime Date { get; set; }
        public List<ShiftSummary> Shifts { get; set; }
    }
}
