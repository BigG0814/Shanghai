using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    public class TableBillReport
    {
        
    }

    public class ItemDetailReport
    {
        public int BillID { get; set; }
        public DateTime BillDate { get; set; }
        public int TableNumber { get; set; }
        public int CustomerCount { get; set; }
        public string Street
        {
            get
            {
                return "5901 50 St";
            }
        }
        public string City
        {
            get
            {
                return "Leduc";
            }
        }
        public string Province
        {
            get
            {
                return "AB";
            }
        }
        public string PostalCode

        {
            get
            {
                return "T9E 6J4";
            }
        }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GST { get; set; }
        public string EmployeeName { get; set; }


    }
}
