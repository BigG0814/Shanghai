using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    public class MenuItemInfo
    {
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public decimal Price { get; set; }

        public bool? isCombo { get; set; }

        public int Categoryid { get; set; }

        public string Description { get; set; }

        public int? AmountOfSelections { get; set; }
    }
}
