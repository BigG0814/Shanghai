using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shanghai.Data.POCOs;
using Shanghai.System.DAL;
using System.Data.Entity;
using System.ComponentModel;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class ReportController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ItemDetailReport> GetTableReceipt(int billID)
        {
            using (var context = new ShanghaiContext())
            {
                var result = from x in context.BillItems.Include(x => x.Bill).Include(x => x.Bill.Employee).Include(x => x.MenuItem)
                             where x.BillID == billID
                             select new ItemDetailReport
                             {
                                 TableNumber = x.Bill.TableNumber,
                                 ItemName = x.MenuItem.MenuItemName,
                                 Price = x.SellingPrice,
                                 BillID = x.Bill.BillID,
                                 CustomerCount = x.Bill.CustomerCount,
                                 GST=x.Bill.GST,
                                 SubTotal=x.Bill.SubTotal,
                                 BillDate=x.Bill.BillDate,
                                 EmployeeName=x.Bill.Employee.FName
                                 
                              };
                List<ItemDetailReport> thing = result.ToList();
                return result.ToList();
            }

        }
    }
}
