using Shanghai.Data.Entities;
using Shanghai.Data.Entities.DTOs;
using Shanghai.Data.POCOs;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class MenuItemController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Category> MenuItemsByCategory()
        {
            using (var context = new ShanghaiContext())
            {
                var result = from category in context.MenuCategories
                             where category.CategoryName != "Buffet"
                             orderby category.CategoryID
                             select new Category
                             {
                                 CatName = category.CategoryName,
                                 Items = from item in category.MenuItems
                                             orderby item.MenuItemID
                                             where item.ActiveYN == true
                                             select new MenuItemInfo
                                             {
                                                 ItemName = item.MenuItemName,
                                                 ItemID = item.MenuItemID,
                                                 Price = item.CurrentPrice,
                                                 isCombo = item.isCombo,
                                                 Description = item.Description,
                                                 AmountOfSelections = item.AmountOfSelections
                                                
                                             }
                             };
                return result.ToList();
            }
        }

        public List<MenuItem> getItemByComboSelection(List<ComboItemSelection> selections)
        {
            using(var context = new ShanghaiContext())
            {
                var result = from x in context.MenuItems.ToList()
                             where selections.Select(y => y.MenuItemId).ToList().Contains(x.MenuItemID)
                             select x;
                return result.ToList();

                            
            }
        }

        public void UpdateComboItems(List<ComboItemSelection> originalSelections, List<MenuItem> newItems)
        {
            using(var context = new ShanghaiContext())
            {
                for (int i = 0; i < newItems.Count; i++)
                {
                    if(originalSelections[i].BillItemID != null)
                    {
                        var billItem = context.BillItems.Find(originalSelections[i].BillItemID.Value);
                        if (billItem.Split > 1)
                        {
                            var Splits = context.BillItems.Where(x => x.Split == billItem.Split).Where(x => x.BillID == billItem.BillID).Where(x => x.MenuItemID == x.MenuItemID).Take(billItem.Split).ToList();
                            foreach(BillItem item in Splits)
                            {
                                var selection = context.ComboItemSelections.Where(x => x.BillItemID == item.BillItemID).FirstOrDefault();
                                selection.MenuItemId = newItems[i].MenuItemID;
                                context.Entry(selection).Property(x => x.MenuItemId).IsModified = true;
                            }
                        }
                        else
                        {
                            var existing = context.ComboItemSelections.Find(originalSelections[i].ComboItemSelectionID);
                            existing.MenuItemId = newItems[i].MenuItemID;
                            context.Entry(existing).Property(x => x.MenuItemId).IsModified = true;
                        }
                    }
                    else
                    {
                        var existing = context.ComboItemSelections.Find(originalSelections[i].ComboItemSelectionID);
                        existing.MenuItemId = newItems[i].MenuItemID;
                        context.Entry(existing).Property(x => x.MenuItemId).IsModified = true;
                    }
                }
                context.SaveChanges();
            }
            
        }

        public MenuItem GetByID(int id)
        {
            using(var context = new ShanghaiContext())
            {
                return context.MenuItems.Find(id);
            }
        }
      
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<MenuItem> getCombinationOptions()
        {
            using(var context = new ShanghaiContext())
            {
                var result = from x in context.MenuItems.Where(x => x.isComboOption) select x;
                return result.ToList();
            }
        }
       
    }
}
