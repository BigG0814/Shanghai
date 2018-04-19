 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shanghai.Data.POCOs;
using Shanghai.System.DAL;
using System.Data.Entity;
using System.ComponentModel;
using Shanghai.Data.Entities;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class EmployeeReportController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<CashOutReport> getCashOut()
        {
            using(var context = new ShanghaiContext())
            {
                var result = from x in context.BillPayments.Include(x => x.Bill).Include(x => x.Bill.Employee).Include(x => x.Bill.BillItems)
                            where x.isVoid == false
                            where x.Bill.isClosed
                             select new CashOutReport
                             {
                                 totalSale = x.Bill.BillItems.Sum(y => y.SellingPrice),
                                 TableNumber = x.Bill.TableNumber,
                                 BillID = x.Bill.BillID,
                                 totalTax = x.Bill.GST,
                                 SubTotal = x.Bill.SubTotal,
                                 BillDate = x.Bill.BillDate,
                                Tips=x.Bill.Tip,
                                EmployeeName = x.Bill.Employee.FName,
                                PaymentTypeID=x.PaymentTypeID
                                
                             };
                return result.ToList();
            }
        }

        //Select by employees
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<CashOutReport> getCashOut_byemployee(int employeeid)
        {
            using (var context = new ShanghaiContext())
            {
               if(employeeid !=0)
                {
                    var result = from x in context.BillPayments.Include(x => x.Bill).Include(x => x.Bill.Employee).Include(x => x.Bill.BillItems)
                                 where x.isVoid == false
                                 where x.Bill.isClosed
                                 where x.Bill.EmployeeID == employeeid

                                 select new CashOutReport
                                 {
                                     totalSale = x.Bill.BillItems.Sum(y => y.SellingPrice),
                                     TableNumber = x.Bill.TableNumber,
                                     BillID = x.Bill.BillID,
                                     totalTax = x.Bill.GST,
                                     SubTotal = x.Bill.SubTotal,
                                     BillDate = x.Bill.BillDate,
                                     Tips = x.Bill.Tip,
                                     EmployeeName = x.Bill.Employee.FName,
                                     PaymentTypeID = x.PaymentTypeID

                                 };
                    return result.ToList();
                }
               else
                {
                    var result = from x in context.BillPayments.Include(x => x.Bill).Include(x => x.Bill.Employee).Include(x => x.Bill.BillItems)
                                 where x.isVoid == false
                                 where x.Bill.isClosed
                              

                                 select new CashOutReport
                                 {
                                     totalSale = x.Bill.BillItems.Sum(y => y.SellingPrice),
                                     TableNumber = x.Bill.TableNumber,
                                     BillID = x.Bill.BillID,
                                     totalTax = x.Bill.GST,
                                     SubTotal = x.Bill.SubTotal,
                                     BillDate = x.Bill.BillDate,
                                     Tips = x.Bill.Tip,
                                     EmployeeName = x.Bill.Employee.FName,
                                     PaymentTypeID = x.PaymentTypeID

                                 };
                    return result.ToList();
                }
               
            }
        }
        


        //select by date
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<CashOutReport> getCashOut_bydate(DateTime start, DateTime end)
        {
            using (var context = new ShanghaiContext())
            {
                var result = from x in context.BillPayments.Include(x => x.Bill).Include(x => x.Bill.Employee).Include(x => x.Bill.BillItems)
                             where x.isVoid == false
                             where x.Bill.isClosed
                             where x.Bill.BillDate >= start && x.Bill.BillDate <= end
                             select new CashOutReport
                             {
                                 totalSale = x.Bill.BillItems.Sum(y => y.SellingPrice),
                                 TableNumber = x.Bill.TableNumber,
                                 BillID = x.Bill.BillID,
                                 totalTax = x.Bill.GST,
                                 SubTotal = x.Bill.SubTotal,
                                 BillDate = x.Bill.BillDate,
                                 Tips = x.Bill.Tip,
                                 EmployeeName = x.Bill.Employee.FName,
                                 PaymentTypeID = x.PaymentTypeID

                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<IncomefordaybyDate> getincomeby_date()
        {
            using (var context = new ShanghaiContext())
            {
                var result = from b in context.Bills
                             group b by b.BillDate into d
                             select new IncomefordaybyDate
                             {
                                 IncomeForDay = d.Sum(z => z.SubTotal),
                                 BillDate = d.Key
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<TotalCustomer> customerby_date()
        {
            using (var context = new ShanghaiContext())
            {
                var result = from c in context.Bills
                             group c by c.BillDate into r
                             select new TotalCustomer
                             {
                                 Customer = r.Sum(w => w.CustomerCount),
                                 BillDate = r.Key
                             };
                return result.ToList();
            }
        }
    }


}

