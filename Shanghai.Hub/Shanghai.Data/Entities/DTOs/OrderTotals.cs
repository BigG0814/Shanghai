using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.Entities.DTOs
{
    public class OrderTotals
    {
        public decimal subtotal { get; set; }
        public decimal? delivery { get; set; }
        public decimal sellingPrice { get; set; }
        public int quantity { get; set; }
        public decimal gst { get; set; }
        public decimal total { get; set; }
    }
}
