using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    [Serializable]
    public class BillItemDetail
    {
        public int MenuItemID { get; set; }
        public string OriginalMenuItemName { get; set; }
        public string MenuItemName { get; set; }
        public decimal SellingPrice { get; set; }
        public string Notes { get; set; }

        public List<ComboItemDetail> comboItems { get; set; }

        public int Split { get; set; }

    }
}
