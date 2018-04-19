using Shanghai.Data.Entities;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Shanghai.Data.Entities.DTOs;
using Shanghai.Data.POCOs;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class OrderController
    {
        // KW- Get info for a specific order 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Order ListOrder(int orderID)
        {
            using (var context = new ShanghaiContext())
            {
                return context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();
            }
        }



        public Order CreateOrder(Order o)
        {
            using(var context = new ShanghaiContext())
            {
               var order = context.Orders.Add(o);
                context.SaveChanges();
                return order;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderComboSelection> ListComboSelections(int cartItemID)
        {
            List<OrderComboSelection> selections = new List<OrderComboSelection>();
            using (var context = new ShanghaiContext())
            {
                var results = context.ComboItemSelections.Where(x => x.ShoppingCartItemID.Value == cartItemID).ToList();
                foreach(var item in results)
                {
                    var selection = new OrderComboSelection();
                    selection.MenuItemId = item.MenuItemId;
                    selection.Price = 0.00M;
                    selection.MenuItemName = context.MenuItems.Where(x => x.MenuItemID == item.MenuItemId).Select(x => x.MenuItemName).FirstOrDefault();
                    selections.Add(selection);
                }

                return selections;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderComboSelection> ListComboSelections(int menuItemId, int orderId)
        {
            List<OrderComboSelection> selections = new List<OrderComboSelection>();
            using (var context = new ShanghaiContext())
            {
                // note: OrderItemId is actually the OrderID
                var results = context.ComboItemSelections.Where(x => x.ComboMenuItemID == menuItemId && x.OrderItemID == orderId).ToList();
                foreach (var item in results)
                {
                    var selection = new OrderComboSelection();
                    selection.MenuItemId = item.MenuItemId;
                    selection.Price = 0.00M;
                    selection.MenuItemName = context.MenuItems.Where(x => x.MenuItemID == item.MenuItemId).Select(x => x.MenuItemName).FirstOrDefault();
                    selections.Add(selection);
                }

                return selections;
            }
        }
        // KW- List of OrderDetails
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderDetail> ListOrderDetails(int orderID)
        {
            using (var context = new ShanghaiContext())
            {
                return context.OrderDetails.Where(x => x.OrderID == orderID).Include(x => x.MenuItem).Include(x => x.Order).ToList();
            }
        }

        //KW - method to get next unused table number
        public int GetOrderTableNumber(int orderType)
        {
            using (var context = new ShanghaiContext())
            {
                Table table = new Table();
                if (orderType == 1)
                {
                    // If delivery use tables 800-835
                    table = context.Tables.Where(x => x.Available == true).Where(x => x.TableNumber >= 800 && x.TableNumber <= 835).First();
                }
                else
                {
                    // If pickup use tables 900-935
                    table = context.Tables.Where(x => x.Available == true).Where(x => x.TableNumber >= 900 && x.TableNumber <= 935).First();
                }

                var existing = context.Tables.Find(table.TableNumber);
                existing.Available = false;
                context.Entry(existing).Property(x => x.TableNumber).IsModified = true;
                context.SaveChanges();
                return table.TableNumber;
            }
        }

        //KW - Method to add a new order and new bill into the system with corresponding order items and bill items
        public int PlaceOrder(Order order, List<OrderDetail> details, List<string> managerContact, int cartId, List<int> comboIds)
        {
            decimal deliveryFee = 0.00M;
            using (var context = new ShanghaiContext())
            {
                // Add items to a new Bill
                if(order.OrderTypeID == 1)
                {
                    deliveryFee = 3.00M;
                }
                Bill bill = new Bill()
                {
                    BillDate = order.OrderDate,
                    CustomerCount = 1,
                    PaidYN = false,
                    EmployeeID = 1, //TODO take out hardcoding
                    TableNumber = GetOrderTableNumber(order.OrderTypeID),
                    SubTotal = details.Sum(x => (x.SellingPrice * x.Quantity) + deliveryFee),
                    GST = details.Sum(x => (x.SellingPrice * x.Quantity) + deliveryFee) * (decimal)0.05,
                    //TODO take GST out and into config section
                    Comments = order.SpecialInstructions,
                    Tip = (decimal)0.00
                };


                context.Orders.Add(order);

                
                //Add items to a new Order
                foreach (var item in details)
                {
                    order.OrderDetails.Add(item);

                    for (int i = 1; i <= item.Quantity; i++)
                    {
                        BillItem billItem = new BillItem()
                        {
                            MenuItemID = item.MenuItemID,
                            SellingPrice = item.SellingPrice,
                            ItemName = context.MenuItems.Where(x => x.MenuItemID == item.MenuItemID).FirstOrDefault().MenuItemName,
                            Split = 1,
                            Notes = null
                        };

                        bill.BillItems.Add(billItem);
                    }
                }
                context.Bills.Add(bill);
                context.SaveChanges();
                int billID = bill.BillID;

                var existing = context.Bills.Find(bill.BillID);
                existing.OrderID = order.OrderID;
                context.Entry(existing).Property(x => x.OrderID).IsModified = true;
                context.SaveChanges();
                int orderID = order.OrderID;

                int billItemID = context.BillItems.Where(x => x.BillID == billID && x.MenuItem.isCombo == true).Select(x => x.BillItemID).FirstOrDefault();
                //Update ComboItemSelections - if applicable - with OrderID and BillItemID
                foreach (var id in comboIds)
                {
                    var result = context.ComboItemSelections.Where(x => x.ShoppingCartItemID == id).ToList();
                    foreach (var selection in result)
                    {
                        selection.OrderItemID = orderID;
                        selection.BillItemID = billItemID;
                        context.ComboItemSelections.Add(selection);
                        var extItem = context.Entry(selection);
                        extItem.State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                // Send confirmation email
                decimal subtotal = 0.00M;
                decimal gst = 0.05M;
                OrderType orderType = new OrderType();
                var results = (from type in context.OrderTypes
                               where type.OrderTypeID == order.OrderTypeID
                               select type).FirstOrDefault();
                orderType.Name = results.Name;
                orderType.OrderTypeID = results.OrderTypeID;

                string subject = "Your Shanghai Online Order Confirmation, Order #" + orderID.ToString();
                string body;
                body = "<div><h2 style=\"font-weight: bold; text-align: center;\">Shanghai Leduc Online Order</h2>";
                body += "<p>Hello " + order.Fname + " " + order.LName + "</p>";
                body += "<p>Thank you for placing an online order with Shanghai Leduc. Please find the details of your order below:</p>";
                body += "<p>Online Order Number: " + order.OrderID.ToString() + "</p>";
                body += "<p> Order Type: " + orderType.Name + "</p>";
                body += "<p> Customer phone: " + order.Phone + "</p>";
                if (orderType.OrderTypeID == 1)
                {
                    body += "<p> Delivery address: ";
                    // Delivery details
                    if (order.AptNumber != null)
                    {
                        body += order.AptNumber + " ";
                    }
                    body += order.Street + "</p>";
                    body += "<p> Estimated delivery time is 45 - 60 min.</p>"; //TODO, what is their actual delivery time?
                }
                else
                {
                    // Pickup details
                    body += "<p> Pickup time: " + order.PickupTime + "</p>";
                }
                body += "<p style=\"font-weight: bold;\"> Order Details:</p>";
                body += "<div>";
                body += "<table style=\"width: 100 %;\">";
                body += "<tr>";
                body += "<th style=\"padding: 15px; border-bottom: 1px solid #ddd;\">Quantity</th>";
                body += "<th style=\"padding: 15px; border-bottom: 1px solid #ddd;\">Menu Item</th>";
                body += "<th style=\"padding: 15px; border-bottom: 1px solid #ddd;\">Price</th>";
                body += "</tr>";
                foreach (var item in details)
                {
                    var itemName = (from items in context.MenuItems
                                    where items.MenuItemID == item.MenuItemID
                                    select items.MenuItemName).FirstOrDefault();
                    body += "<tr>";
                    body += "<td style=\"padding: 15px; border-bottom: 1px solid #ddd;\">" + item.Quantity + "</td>";
                    body += "<td style=\"padding: 15px; border-bottom: 1px solid #ddd;\">" + itemName;
                    if (item.MenuItem.isCombo)
                    {
                        body += "<ul>";
                        var comboSelections = context.ComboItemSelections.Where(x => x.ComboMenuItemID == item.MenuItemID && x.OrderItemID == item.OrderID).ToList();
                        foreach(var selection in comboSelections)
                        {
                            string nm = context.MenuItems.Where(x => x.MenuItemID == selection.MenuItemId).Select(x => x.MenuItemName).FirstOrDefault().ToString();
                            body += "<li>" + nm + "</li>";
                        }
                        body += "</ul>";
                    }
                    body += "</td>";
                    body += "<td style=\"padding: 15px; border-bottom: 1px solid #ddd;\">" + (item.Quantity * item.SellingPrice).ToString() + "</td>";
                    body += "</tr>";
                    
                    subtotal += (item.Quantity * item.SellingPrice);
                }

                body += "<p>Sub-total: " + subtotal.ToString("C") + "</p>";
                body += "<p>GST: " + (gst * 100).ToString() + "%</p>";
                body += "<p>Total: " + ((subtotal * gst) + subtotal).ToString("C") + "</p><br/><br/>";
                body += "<p style=\"text-align: center;\">Shanghai Leduc - 5901 50 St, Leduc, AB. <br/>780-986-1862 OR 780-986-1883</p>";
                body += "</div>";
                body += "</div>";

                MailSender sender = new MailSender(order.Email, subject, body);
                sender.SendMail();


                //Send confirmation text to restaurant
                //TODO default number in case, for some reason, no manager is clocked in
                string txtSubject, txtBody;
                txtSubject = "New Online Order";
                txtBody = "<p>Online Order #" + order.OrderID + " for " + orderType.Name + " has just been placed.</p>";
                txtBody += "<p>Please check the restaurant system for details.</p>";

                    foreach(var item in managerContact)
                    {
                        MailSender txtRestaurant = new MailSender(item, txtSubject, txtBody);
                        txtRestaurant.SendMail();
                    }

                return orderID;
            }
        }

        public OrderTotals OrderTotals(int orderID)
        {

            var orderTotals = new OrderTotals();
            int itemCount = 0;
            decimal priceTotal = 0.00M;

            using (var context = new ShanghaiContext())
            {
                var itemPrices = context.OrderDetails.Where(x => x.OrderID == orderID).ToList(); // gets all the items from the specific order

                foreach (var itemPrice in itemPrices) // get the order item details for the specific order and adds the prices
                {
                    itemCount = itemCount + itemPrice.Quantity;
                    priceTotal = priceTotal + (itemPrice.Quantity * itemPrice.SellingPrice);
                }

                var orderPrices = context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault(); // gets the details from the specific order

                // now add the prices into the new OrderTotals object

                orderTotals.gst = orderPrices.GST;
                orderTotals.subtotal = priceTotal;
                if (orderPrices.DeliverFee != null)
                {
                    orderTotals.delivery = orderPrices.DeliverFee;
                    orderTotals.total = (decimal)orderPrices.DeliverFee + orderPrices.GST + priceTotal;
                }
                else
                {
                    orderTotals.total = orderPrices.GST + priceTotal;
                }

            }

            return orderTotals;
        }


        //KW - method to add a DriverID(employeeId) to an order
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void AssignDriver(int employeeId, int orderId)
        {
            using (var context = new ShanghaiContext())
            {
                var existing = context.Orders.Find(orderId);
                existing.DeliveryDriverID = employeeId;
                context.SaveChanges();
            }
        }

        public void updateOrder(string text1, string text2, string text3, string text4, string text5, string text6, int orderID)
        {
            using(var context = new ShanghaiContext())
            {
                var existing = context.Orders.Find(orderID);
                existing.Fname = text1;
                existing.LName = text2;
                existing.Phone = text3;
                existing.Street = text4;
                existing.AptNumber = text5;
                existing.City = text6;
                context.Entry(existing).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

