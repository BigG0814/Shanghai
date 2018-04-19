using Shanghai.Data.Entities;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class ShiftTradingProcessController : ShiftController
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void Create_ShiftTrade(ShiftTrade item)
        {
            using (var context = new ShanghaiContext())
            {
                //if((context.ShiftTrades.Find(item.ShiftID).Any(x => x.IsApproved == false)))
                //{
                //    throw new Exception("One Trade is already waiting for approve");
                //}
                item = context.ShiftTrades.Add(item);
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void Update_ShiftTrade(ShiftTrade item)
        {
            using (var context = new ShanghaiContext())
            {
                var data = context.ShiftTrades.Find(item.TradeID);
                data.NewEmployee = item.NewEmployee;
                data.ApprovingEmployee = item.ApprovingEmployee;
                data.IsApproved = item.IsApproved;
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public ShiftTrade ShiftTradeRequest_Get(int shiftid)
        {
            using (var context = new ShanghaiContext())
            {
                var resault = (from x in context.ShiftTrades.Include(x => x.Employee).Include(x => x.Shift).ToList()
                               where x.ShiftID.Equals(shiftid) && x.IsApproved == false
                               select new ShiftTrade
                               {
                                    ApprovingEmployee = x.ApprovingEmployee,
                                    TradeID = x.TradeID,
                                     IsApproved = x.IsApproved,
                                      Reason = x.Reason,
                                      ShiftID = x.ShiftID,
                                      TimePosted = x.TimePosted,
                                       OriginalEmployee = x.OriginalEmployee,
                                       NewEmployee = x.NewEmployee,
                                        Employee = x.Employee,
                                         Employee1 = x.NewEmployee.HasValue? context.Employees.Where(y => y.EmployeeID == x.NewEmployee.Value).FirstOrDefault() : null,
                                         Employee2 = x.ApprovingEmployee.HasValue? context.Employees.Where(y => y.EmployeeID == x.ApprovingEmployee.Value).FirstOrDefault() : null,
                                         Shift = x.Shift
                               }).FirstOrDefault();
                if (resault == null)
                {
                    return null;
                }
                else
                {
                    return resault;
                }
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ShiftTrade> ShiftTrade_List()
        {
            using (var context = new ShanghaiContext())
            {
                return context.ShiftTrades.Where(x => x.IsApproved == false).ToList();
            }
        }

        public void deleteTrade(int tradeID)
        {
            using (var context = new ShanghaiContext())
            {
                var existing = context.ShiftTrades.Find(tradeID);
                if (existing != null)
                {
                    context.ShiftTrades.Remove(existing);
                    context.SaveChanges();
                }
            }
        }
    }
}
