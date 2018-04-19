using Shanghai.Data.Entities;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Shanghai.Data.POCOs;
using System.ComponentModel;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class BillController
    {
        public Bill GetNewOrCurrentBill_ByTable(int tableID, int employeeID, int? customerCount = null)
        {
            using (var context = new ShanghaiContext())
            {
                Bill bill = (from x in context.Bills.Include(x => x.BillItems).Include(x => x.Payments).Include(y => y.BillItems.Select(z => z.MenuItem)).ToList()
                              where x.TableNumber == tableID
                              select x).LastOrDefault();
                if (bill == null)
                {
                    bill = new Bill();
                    bill.BillDate = DateTime.Now;
                    bill.EmployeeID = employeeID;
                    bill.TableNumber = tableID;
                    bill.SubTotal = 0;
                    bill.GST = 0;
                    bill.Tip = 0;
                    bill.PaidYN = false;
                    bill.CustomerCount = 0;
                }
                else
                {
                    return bill;
                }
                return bill;
            }
        }
        public List<Bill> CreateBill(Bill bill)
        {
            using(var context = new ShanghaiContext())
            {
                Bill item = context.Bills.Add(bill);
                Table table = context.Tables.Find(bill.TableNumber);
                if (table == null)
                    throw new Exception("Oops... The table given does not exist in the database!");
                table.Available = false;
                context.Entry(table).Property(x => x.Available).IsModified = true;
                context.SaveChanges();
                return GetAllBillsByTable(item.TableNumber, item.EmployeeID);
            }
        }
        public List<Bill> UpdateBillCustomerCount(Bill bill)
        {
            using (var context = new ShanghaiContext())
            {
                var existing = context.Bills.Find(bill.BillID);
                if(existing == null)
                {
                    throw new Exception("Bill does not exist");
                }
                else
                {
                    existing.CustomerCount = bill.CustomerCount;
                    context.Entry(existing).Property(x => x.CustomerCount).IsModified = true;
                    context.SaveChanges();
                }
                return GetAllBillsByTable(existing.TableNumber, existing.EmployeeID);
            }
        }
        public List<Bill> UpdateBillItemMemo(BillItem item)
        {
            using(var context = new ShanghaiContext())
            {
                var existing = context.BillItems.Find(item.BillItemID);
                if(existing == null)
                {
                    throw new Exception("Bill Item does not exist");
                }
                else
                {
                    existing.Notes = item.Notes;
                    context.Entry(existing).Property(x => x.Notes).IsModified = true;
                    context.SaveChanges();
                }
                var bill = context.Bills.Find(existing.BillID);
                return GetAllBillsByTable(bill.TableNumber, bill.EmployeeID);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TakeoutandDelivery> DeliveryTables()
        {
            using (var context = new ShanghaiContext())
            {
                List<TakeoutandDelivery> Tablesfull = (from bi in context.Bills.ToList()
                                                       where bi.TableNumber > 799 && bi.TableNumber < 836 && bi.isClosed == false
                                                       select new TakeoutandDelivery
                                                       {
                                                           TableNumber = bi.TableNumber,
                                                           FullName = bi.TableNumber + " - " + (from x in context.Orders.ToList()
                                                                       where x.OrderID == bi.OrderID
                                                                       select x.Fname + " " + x.LName).FirstOrDefault()
                                                       }).ToList();
                TakeoutandDelivery empty = (from ta in context.Tables
                                            where ta.Available == true && ta.TableNumber > 799 && ta.TableNumber < 836
                                            select new TakeoutandDelivery
                                            {
                                                TableNumber = ta.TableNumber,
                                                FullName = ta.TableNumber+ "- Empty"
                                            }).FirstOrDefault();
             
                Tablesfull.Add(empty);
                return Tablesfull;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TakeoutandDelivery> TakeoutTables()
        {
            using (var context = new ShanghaiContext())
            {
                List<TakeoutandDelivery> Tablesfull = (from bi in context.Bills.ToList()
                                                       where bi.TableNumber > 899 && bi.isClosed == false
                                                       select new TakeoutandDelivery
                                                       {
                                                           TableNumber = bi.TableNumber,
                                                           FullName = bi.TableNumber + " - " +(from x in context.Orders.ToList()
                                                                       where x.OrderID == bi.OrderID
                                                                       select x.Fname + " " + x.LName).FirstOrDefault()
                                                       }).ToList();
                TakeoutandDelivery empty = (from ta in context.Tables
                                            where ta.Available == true && ta.TableNumber > 899
                                            select new TakeoutandDelivery
                                            {
                                                TableNumber = ta.TableNumber,
                                                FullName = ta.TableNumber + "- Empty"
                                            }).FirstOrDefault();
                Tablesfull.Add(empty);
                return Tablesfull;
            }
        }
        public List<Bill> RemoveBillItem(BillItem item)
        {
            using(var context = new ShanghaiContext())
            {
                var existing = context.BillItems.Find(item.BillItemID);
                if (existing == null)
                    throw new Exception("Bill Item does not exist");
                context.BillItems.Remove(existing);
                context.SaveChanges();
                var bill = context.Bills.Find(item.BillID);
                return GetAllBillsByTable(bill.TableNumber, bill.EmployeeID);
            }
        }

        public List<Bill> RemoveBillItem(List<BillItem> items)
        {
            using (var context = new ShanghaiContext())
            {
                var initialbill = context.Bills.Find(items[0].BillID);
                var returnList = new List<Bill>();
                foreach (BillItem item  in items)
                {
                    var existing = context.BillItems.Find(item.BillItemID);
                    if (existing == null)
                        throw new Exception("Bill Item does not exist");
                    context.BillItems.Remove(existing);
                    context.SaveChanges();
                    var bill = context.Bills.Find(item.BillID);
                    returnList = GetAllBillsByTable(initialbill.TableNumber, initialbill.EmployeeID);
                    if (bill.BillItems.Count == 0 && returnList.Count > 1)
                    {
                        context.Bills.Remove(bill);
                        context.SaveChanges();
                        returnList = GetAllBillsByTable(initialbill.TableNumber, initialbill.EmployeeID);
                    }
                }

                return returnList;
            }
        }

        public List<Bill> UpdateItemPrice(BillItem item)
        {
            using (var context = new ShanghaiContext())
            {
                var existing = context.BillItems.Find(item.BillItemID);
                if (existing == null)
                    throw new Exception("Bill Item not found. Update Cancelled.");
                existing.SellingPrice = item.SellingPrice;
                context.Entry(existing).Property(x => x.SellingPrice).IsModified = true;
                context.SaveChanges();
                var bill = context.Bills.Find(existing.BillID);
                return GetAllBillsByTable(bill.TableNumber, bill.EmployeeID);
            }
        }
        public Dictionary<List<Bill>, int> addBillItem(BillItem item)
        {
            using(var context = new ShanghaiContext())
            {
                context.BillItems.Add(item);
                context.SaveChanges();
                var bill = context.Bills.Find(item.BillID);
                Dictionary<List<Bill>, int> bills = new Dictionary<List<Bill>, int>();
                bills.Add(GetAllBillsByTable(bill.TableNumber, bill.EmployeeID), item.BillItemID);
                return bills;
            }
        }


        public List<Bill> GetAllBillsByTable(int tableID, int employeeID)
        {
            using(var context = new ShanghaiContext())
            {
                var ActiveBillsForTable = context.Bills
                                                    .Include(x => x.Payments)
                                                    .Include(x => x.Payments.Select(y => y.PaymentType))
                                                    .Include(x => x.BillItems)
                                                    .Include(x => x.BillItems.Select(y => y.MenuItem))
                                                    .Where(x => x.TableNumber == tableID && x.isClosed == false)
                                                    .ToList();
                if(ActiveBillsForTable.Count == 0)
                {
                    Bill bill = new Bill();
                    bill.BillDate = DateTime.Now;
                    bill.EmployeeID = employeeID;
                    bill.TableNumber = tableID;
                    bill.SubTotal = 0;
                    bill.GST = 0;
                    bill.Tip = 0;
                    bill.isClosed = false;
                    bill.PaidYN = false;
                    bill.CustomerCount = 0;
                    return new List<Bill>() { bill };
                }
                return ActiveBillsForTable;
                
            }
        }



        public List<int> TablesOccupied()
        {
            using (var context = new ShanghaiContext())
            {
                List<int> TablesOccupied = (from ta in context.Tables
                                           where ta.Available == false
                                           select ta.TableNumber).ToList();
                return TablesOccupied;
            }
        }

        public string TableOccupiedInfo(int TableNumber)
        {
            using (var context = new ShanghaiContext())
            {
                string EmployeeName = (context.Bills.Include(x => x.Employee)
                                                    .Where(x => x.TableNumber == TableNumber)
                                                    .Where(x => x.isClosed == false)
                                                    .Select(x => x.Employee.FName + " " + x.Employee.LName).FirstOrDefault());
                return EmployeeName;
            }
        }
        

        public void SaveTableSplit(List<TempBill> bills)
        {
            using(var context = new ShanghaiContext())
            {

                foreach(TempBill bill in bills)
                {
                    List<ComboItemSelection> comboItems = new List<ComboItemSelection>(); 
                    if(bill.OriginalBillID != null) //Temp Bill is an existing Bill
                    {
                        var existingBill = context.Bills.Find(bill.OriginalBillID); //Existing Bill
                        if (existingBill == null)
                            throw new Exception("Bill not found from original bill ID");
                        var existingBillItems = context.BillItems.Where(x => x.BillID == existingBill.BillID).ToList();
                        context.BillItems.RemoveRange(existingBillItems); //Removes items from the bill, they will be replaced with the temp bill's items
                        decimal subtotal = (decimal)0.00;
                        foreach(BillItemDetail det in bill.items) //Foreach Bill Item on this temp bill
                        {
                            BillItem item = new BillItem() //Create a new Bill Item
                            {
                                BillID = existingBill.BillID,
                                ItemName = det.MenuItemName,
                                MenuItemID = det.MenuItemID,
                                Notes = det.Notes,
                                SellingPrice = det.SellingPrice,
                                Split = det.Split,
                            };
                            subtotal += item.SellingPrice;
                            context.BillItems.Add(item);
                            context.SaveChanges();
                            if(det.comboItems != null) 
                            {
                                foreach(ComboItemDetail combo in det.comboItems) //Foreach combo Item for this detail item
                                {
                                    ComboItemSelection newSelect = new ComboItemSelection();
                                    newSelect.BillItemID = item.BillItemID;
                                    newSelect.MenuItemId = combo.MenuItemID;
                                    context.ComboItemSelections.Add(newSelect);
                                    context.SaveChanges();    
                                }
                            }
                        }
                        existingBill.SubTotal = subtotal;
                        existingBill.GST = subtotal * (decimal)0.05;
                        context.Entry(existingBill).Property(x => x.SubTotal).IsModified = true;
                        context.Entry(existingBill).Property(x => x.GST).IsModified = true;
                        if(bill.items.Count == 0)
                        {
                            context.Bills.Remove(existingBill);
                        }
                    }
                    else //TempBill is not yet created in DB
                    {
                        if(bill.items.Count > 0)
                        {
                            Bill newBill = new Bill()
                            {
                                BillDate = DateTime.Now,
                                Comments = "",
                                CustomerCount = 0,
                                EmployeeID = bill.EmployeeID,
                                GST = bill.GST,
                                PaidYN = bill.PaidYN,
                                SubTotal = bill.SubTotal,
                                TableNumber = bill.TableNumber,
                                Tip = (decimal)0.00,
                                BillItems = new List<BillItem>()
                            };
                            newBill.SubTotal = newBill.BillItems.Sum(x => x.SellingPrice);
                            newBill.GST = newBill.SubTotal * (decimal)0.05;
                            context.Bills.Add(newBill);
                            context.SaveChanges();
                            foreach (BillItemDetail det in bill.items)
                            {
                                BillItem item = new BillItem()
                                {
                                    ItemName = det.MenuItemName,
                                    MenuItemID = det.MenuItemID,
                                    Notes = det.Notes,
                                    SellingPrice = det.SellingPrice,
                                    Split = det.Split,
                                    BillID = newBill.BillID
                                };

                                context.BillItems.Add(item);
                                context.SaveChanges();
                                
                                if (det.comboItems != null)
                                {
                                    foreach (ComboItemDetail combo in det.comboItems)
                                    {
                                        ComboItemSelection newSelect = new ComboItemSelection();
                                        newSelect.BillItemID = item.BillItemID;
                                        newSelect.MenuItemId = combo.MenuItemID;
                                        context.ComboItemSelections.Add(newSelect);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            


                        }
                        
                    }//End If/Else original bill id exists
                }//End Foreach bill in bills
                context.SaveChanges();

                List<int> allIDs = context.ComboItemSelections.Where(x => x.BillItemID.HasValue).Select(x => x.BillItemID.Value).ToList().Distinct().ToList();
                List<int> deleteME = allIDs.Where(x => !context.BillItems.Select(y => y.BillItemID).Contains(x)).ToList();
                foreach(int i in deleteME)
                {
                    List<ComboItemSelection> list = context.ComboItemSelections.Where(x => x.BillItemID.HasValue).Where(x => x.BillItemID == i).ToList();
                    context.ComboItemSelections.RemoveRange(list);
                    context.SaveChanges();
                }
            }//End Using Shanghai context
        }


        // KW- Get info for a specific bill 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Bill GetBill(int orderId)
        {
            using (var context = new ShanghaiContext())
            {
                return context.Bills.Where(x => x.OrderID == orderId).FirstOrDefault();
            }
        }

        public Bill addPayment(BillPayment payment)
        {
            using(var context = new ShanghaiContext())
            {
                context.BillPayments.Add(payment);
                context.SaveChanges();
                UpdateBillTotals(payment.BillID);
                return context.Bills.Include(x => x.Payments).Include(x => x.Payments.Select(y => y.PaymentType)).Include(x => x.BillItems).Include(x => x.BillItems.Select(y => y.MenuItem)).Where(x => x.BillID == payment.BillID).FirstOrDefault();
            }
        }

        public void UpdateBillTotals(int billID)
        {
            using(var context = new ShanghaiContext())
            {
                var existing = context.Bills.Include(x => x.Payments).Include(x=> x.BillItems).Include(x => x.BillItems.Select(y => y.MenuItem)).Where(x => x.BillID == billID).FirstOrDefault();
                existing.SubTotal = (context.BillItems.Where(x => x.BillID == existing.BillID).Sum(x => x.SellingPrice));
                if(existing.DeliveryFee.HasValue)
                {
                    existing.GST = decimal.Round((existing.SubTotal + existing.DeliveryFee.Value) * (decimal)0.05,2);
                    existing.PaidYN = (existing.SubTotal + existing.DeliveryFee.Value + existing.GST) <= existing.Payments.Where( x=> x.isVoid == false).Sum(x => x.PaymentAmount) ? true : false;
                    existing.Tip = existing.Payments.Sum(x => x.PaymentAmount) > (existing.SubTotal + existing.DeliveryFee.Value + existing.GST) ? existing.Payments.Where(x => x.isVoid == false).Sum(x => x.PaymentAmount) - (existing.SubTotal + existing.DeliveryFee.Value + existing.GST) : (decimal)0.00;
                }
                else
                {
                    existing.GST = decimal.Round(existing.SubTotal * (decimal)0.05, 2);
                    existing.PaidYN = (existing.SubTotal + existing.GST) <= existing.Payments.Where(x => x.isVoid == false).Sum(x => x.PaymentAmount) ? true : false;
                    existing.Tip = existing.Payments.Sum(x => x.PaymentAmount) > (existing.SubTotal  + existing.GST) ? existing.Payments.Where(x => x.isVoid == false).Sum(x => x.PaymentAmount) - (existing.SubTotal + existing.GST) : (decimal)0.00;
                }
                context.Entry(existing).Property(x => x.SubTotal).IsModified = true;
                context.Entry(existing).Property(x => x.GST).IsModified = true;
                context.Entry(existing).Property(x => x.Tip).IsModified = true;
                context.Entry(existing).Property(x => x.PaidYN).IsModified = true;
                context.SaveChanges(); 
            }
        }

        public Bill voidPayment(int paymentId)
        {
            using(var context = new ShanghaiContext())
            {
                var existing = context.BillPayments.Find(paymentId);
                if (existing == null)
                    throw new Exception("That paymentID does not exist");
                existing.isVoid = !existing.isVoid;
                context.Entry(existing).Property(x => x.isVoid).IsModified = true;
                
                context.SaveChanges();

                return context.Bills
                    .Include(x => x.Payments)
                    .Include(x => x.Payments.Select(y => y.PaymentType))
                    .Include(x => x.BillItems)
                    .Include(x => x.BillItems.Select(y => y.MenuItem))
                    .Where(x => x.BillID == existing.BillID).FirstOrDefault();
            }
        }

        public Bill closeBill(int billID)
        {
            using(var context = new ShanghaiContext())
            {
                var existing = context.Bills.Find(billID);
                if (existing == null)
                    throw new Exception("Given Bill does not exist");
                if (existing.PaidYN)
                {
                    existing.isClosed = true;
                    context.Entry(existing).Property(x => x.isClosed).IsModified = true;
                    context.SaveChanges();
                }
                var allBillsCloses = context.Bills.Where(x => x.TableNumber == existing.TableNumber).Where(x => x.isClosed == false).ToList();
                if (existing.OrderID.HasValue)
                {
                    var order = context.Orders.Find(existing.OrderID.Value);
                    order.CompletedYN = true;
                    order.Tip = existing.Tip;
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if(allBillsCloses.Count == 0)
                {
                    var table = context.Tables.Find(existing.TableNumber);
                    table.Available = true;
                    context.Entry(table).Property(x => x.Available).IsModified = true;
                    context.SaveChanges();
                }
                return context.Bills
                    .Include(x => x.Payments)
                    .Include(x => x.Payments.Select(y => y.PaymentType))
                    .Include(x => x.BillItems)
                    .Include(x => x.BillItems.Select(y => y.MenuItem))
                    .Where(x => x.BillID == existing.BillID).FirstOrDefault();
            }
        }

        public List<ComboItemSelection> getComboSelections(BillItem item)
        {
            using (var context = new ShanghaiContext())
            {
                return context.ComboItemSelections.Where(x => x.BillItemID == item.BillItemID).ToList();
               
            }
        }

        public List<Bill> addComboSelection(ComboItemSelection selection)
        {
            using(var context = new ShanghaiContext())
            {
                context.ComboItemSelections.Add(selection);
                context.SaveChanges();
                var billitem = context.BillItems.Find(selection.BillItemID).BillID;
                var bill = context.Bills.Find(billitem);
                return GetAllBillsByTable(bill.TableNumber, bill.EmployeeID);
            }
        }

        public List<ComboItemDetail> getComboItemsByBillItem(int BillItemID)
        {
            using(var context = new ShanghaiContext())
            {
                var result = from x in context.ComboItemSelections.ToList()
                             where x.BillItemID == BillItemID
                             select new ComboItemDetail
                             {
                                 BillItemID = x.BillItemID,
                                 ComboItemSelectionID = x.ComboItemSelectionID,
                                 MenuItemID = x.MenuItemId,
                                 MenuItemName = context.MenuItems.Find(x.MenuItemId).MenuItemName
                             };
                return result.ToList();
            }
        }
    }
}
