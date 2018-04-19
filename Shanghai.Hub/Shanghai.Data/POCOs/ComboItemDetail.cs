using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.POCOs
{
    [Serializable]
    public class ComboItemDetail
    {
        public int ComboItemSelectionID { get; set; }

        public int? BillItemID { get; set; }

        public int? OrderItemID { get; set; }

        public int? ShoppingCartItemID { get; set; }

        public int MenuItemID { get; set; }

        public string MenuItemName { get; set; }
    }
}
