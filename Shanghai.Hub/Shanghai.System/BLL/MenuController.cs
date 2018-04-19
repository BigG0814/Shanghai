using Shanghai.Data.Entities;
using Shanghai.Data.Entities.DTOs;
using Shanghai.Data.POCOs;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shanghai.System.BLL
{
    [DataObject]
    public class MenuController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuItem> Menu_List()
        {
            using (var context = new ShanghaiContext())
            {
                return context.MenuItems.ToList();
            }
        }

        //edit menu item

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<MenuItem> MenuByCategory(string categoryid)
        {
            int catid = int.Parse(categoryid);
            using (var context = new ShanghaiContext())
            {
                List<MenuItem> result;
                if(catid != 0)
                {
                    result = (from m in context.MenuItems.Include(x => x.MenuCategory)
                              where m.CategoryID == catid
                              select m).ToList();
                }
                else
                {
                    result = (from m in context.MenuItems.Include(x => x.MenuCategory)
                              select m).ToList();
                }
                
                return result;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddMenuItem(MenuItem item)
        {
            using(var context = new ShanghaiContext())
            {
                context.MenuItems.Add(item);
                context.SaveChanges();
            }
        }


        public void DeleteMenuitem(int menuitemid)
        {
            using (var context = new ShanghaiContext())
            {
                var existingItem = context.MenuItems.Find(menuitemid);
                existingItem.ActiveYN = false;
                context.Entry(existingItem).Property(x => x.ActiveYN).IsModified = true;
                context.SaveChanges();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteMenuitem(MenuItem item)
        {
            DeleteMenuitem(item.MenuItemID);
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateMenuItem(MenuItem item)
        {
            using(var context = new ShanghaiContext())
            {
                var data = context.MenuItems.Find(item.MenuItemID);
                data.MenuItemName = item.MenuItemName;
                data.Description = item.Description;
                data.IncludeInWebMenu = item.IncludeInWebMenu;
                data.CategoryID = item.CategoryID;
                data.CurrentPrice = item.CurrentPrice;
                data.AmountOfSelections = item.AmountOfSelections;
                data.ActiveYN = item.ActiveYN;
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
                
            }
        }

      

    }
}
