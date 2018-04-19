using System;

namespace Shanghai.Data.POCOs
{
    public class ShiftSummary
    {
        public int ShiftID { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int JobTypeID { get; set; }
        public bool isFinal { get; set; }
    }
}