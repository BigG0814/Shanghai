using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    [Serializable]
    public class TempBill
    {
        public int? OriginalBillID { get; set; }
        public bool PaidYN { get; set; }

        public bool Tip { get; set; }

        public bool isClosed { get; set; }

        public int EmployeeID { get; set; }

        public int TableNumber { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GST { get; set; }

        public List<BillItemDetail> items { get; set; }
    }
}
