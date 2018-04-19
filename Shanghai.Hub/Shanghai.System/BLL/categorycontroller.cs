using Shanghai.Data.Entities;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using Shanghai.Data;
using System.Text;
using System.Threading.Tasks;


namespace Shanghai.System.BLL
{
    [DataObject]
    public class categorycontroller
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategory> Category_List()
        {
            using (var context = new ShanghaiContext())
            {
                return context.MenuCategories.Include(x => x.MenuItems).ToList();
            }
        }

        //edit categories


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddMenuCategory(MenuCategory item)
        {
            using (var context = new ShanghaiContext())
            {
                context.MenuCategories.Add(item);
                context.SaveChanges();
            }
        }


     
        public void DeleteMenuCategory(int categoryid)
        {
            using (var context = new ShanghaiContext())
            {
             var existingItem = context.MenuCategories.Find(categoryid);
                context.MenuCategories.Remove(existingItem);
                context.SaveChanges();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteMenuCategory(MenuCategory item)
        {
            DeleteMenuCategory(item.CategoryID);
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateMenuCategory(MenuCategory item)
        {
            using (var context = new ShanghaiContext())
            {
                var data = context.MenuCategories.Find(item.CategoryID);
                data.CategoryName = item.CategoryName;
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}

