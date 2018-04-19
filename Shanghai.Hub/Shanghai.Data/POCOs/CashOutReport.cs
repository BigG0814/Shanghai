using Shanghai.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
   public class CashOutReport
    {
        public decimal totalTax { get; set; }
        public decimal totalSale { get; set; }
        public decimal totalDiscount { get; set; }
        public int BillID { get; set; }
        public DateTime BillDate { get; set; }
        public int TableNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GST { get; set; }
        public string EmployeeName { get; set; }
        public decimal Tips { get; set; }
        public int? PaymentTypeID { get; set; }
       
    }
}
