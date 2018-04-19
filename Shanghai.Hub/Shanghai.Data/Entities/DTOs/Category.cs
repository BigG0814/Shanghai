using Shanghai.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.Entities.DTOs
{
    public class Category
    {
        public string CatName { get; set; }
        public IEnumerable<MenuItemInfo> Items { get; set; }
    }
}
