using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shanghai.Data.Entities;
using Shanghai.WebApp.UserControls;
using Shanghai.System.BLL;
using System.Net;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Shanghai.Data.POCOs;

namespace Shanghai.WebApp.POS
{
    public partial class TableBill : POSPage
    {
        public List<Bill> allBills
        {
            get
            {
                return ViewState["AllBills"] as List<Bill>;
            }
            set
            {
                ViewState["AllBills"] = value;
                BillRepeater.DataSource = value;
                BillRepeater.DataBind();
                BillLabel.Text = "Table #" + value[0].TableNumber.ToString();
                decimal gst;
                decimal total;
                decimal tip = (decimal)0.00;
                decimal paid = (decimal)0.00;
                decimal subtotal = value.Sum(x => x.BillItems.Sum(y => y.SellingPrice));
                if (value[0].DeliveryFee != null)
                {
                    decimal deliveryFee = value[0].DeliveryFee.Value;
                    DeliveryFeePrompt.Visible = true;
                    DeliveryFeeLabel.Visible = true;
                    DeliveryFeeLabel.Text = deliveryFee.ToString("C2");
                    gst = (deliveryFee + subtotal) * (decimal)0.05;
                    total = subtotal + gst + deliveryFee;
                }
                else
                {
                    gst = subtotal * (decimal)0.05;
                    total = subtotal + gst;
                }
                if(value.Sum(x => x.BillItems.Count()) == 0)
                {
                    PayBillBtn.Visible = false;
                    SplitBillBtn.Visible = false;
                }
                else
                {
                    PayBillBtn.Visible = true;
                    SplitBillBtn.Visible = true;
                }
                if(value.Where(x => x.Payments != null).SelectMany(x => x.Payments).ToList().Count > 0)
                {
                    if(value.Select(x => x.Payments).Count() > 0)
                    {
                        paid = value.Sum(x => x.Payments.Where(y => y.isVoid == false).Sum(y => y.PaymentAmount));
                        tip = value.Sum(x => x.Tip);
                        PaidAmountLbl.Visible = true;
                        TipAmountLbl.Visible = true;
                        tipPromptLbl.Visible = true;
                        paidPromptLbl.Visible = true;
                    }
                    else
                    {
                        PaidAmountLbl.Visible = false;
                        TipAmountLbl.Visible = false;
                        tipPromptLbl.Visible = false;
                        paidPromptLbl.Visible = false;
                    }
                }
                else
                {
                    PaidAmountLbl.Visible = false;
                    TipAmountLbl.Visible = false;
                    tipPromptLbl.Visible = false;
                    paidPromptLbl.Visible = false;
                }
                
                
                decimal NonSplitDiscount = (value.Sum(x => x.BillItems
                                                    .Where(y => y.Split < 2)
                                                    .Sum(y => y.SellingPrice))
                                            - 
                                            (value.Sum(x => x.BillItems
                                                                .Where(y => y.Split < 2)
                                                                .Sum(y => y.MenuItem.CurrentPrice))));
                decimal SplitDiscount = (value.Sum(x => x.BillItems.Where(y => y.Split >= 2).Sum(y => y.SellingPrice)) - (value.Sum(x => x.BillItems.Where(y => y.Split >= 2).Sum(y => y.MenuItem.CurrentPrice / y.Split))));

                decimal discount = NonSplitDiscount + SplitDiscount;
                if(discount != 0)
                {
                    DiscountLabel.Visible = true;
                    DiscountPromptlbl.Visible = true;
                    DiscountLabel.Text = discount.ToString("C2");
                }
                SubtotalLabel.Text = subtotal.ToString("C2");
                GSTLabel.Text = gst.ToString("C2");
                TotalLabel.Text = total.ToString("C2");
                PaidAmountLbl.Text = paid.ToString("C2");
                TipAmountLbl.Text = tip.ToString("C2");
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (LoggedInEmployee == null || SelectedTableID == 0)
            {
                Response.Redirect("~/POS/POSLogIn.aspx");
            }
            if (!Page.IsPostBack)
            {
                categorycontroller menuMgr = new categorycontroller();
                CategoryRepeater.DataSource = menuMgr.Category_List();
                CategoryRepeater.DataBind();
                MenuCategoryRepeater.DataSource = menuMgr.Category_List();
                MenuCategoryRepeater.DataBind();  
                if(SelectedTableID >= 800 && SelectedTableID <= 900)
                {
                    addCustomerCount.Visible = false;
                    cancelCustomerCount.Visible = false;
                    TakeOutPanel.Visible = true;
                    HeadingLbl.Text = "Delivery";
                    customerInfoPanel.Visible = true;
                    customerAddressPanel.Visible = true;
                    isTakeOut.Value = "false";
                    //Delivery
                }
                else if(SelectedTableID > 900)
                {
                    isTakeOut.Value = "true";
                    addCustomerCount.Visible = false;
                    cancelCustomerCount.Visible = false;
                    TakeOutPanel.Visible = true;
                    HeadingLbl.Text = "Take Out";
                    customerInfoPanel.Visible = true;
                    customerAddressPanel.Visible = false;
                    //TakeOut
                }
                else
                {
                    TakeOutPanel.Visible = false;
                }          
                BillController sysmgr = new BillController();
                allBills = sysmgr.GetAllBillsByTable(SelectedTableID, LoggedInEmployee.EmployeeID);
                if (allBills[0].BillID == 0)
                {
                    NavPanel.Visible = false;
                  CustomerCountPanel.Visible = true;
                  BillInfoPanel.Visible = false;
                }
                else
                {
                    CustomerCountTB.Text = allBills[0].CustomerCount.ToString();
                    NavPanel.Visible = true;
                   CustomerCountPanel.Visible = false;
                   BillInfoPanel.Visible = true;
                    if (allBills[0].OrderID.HasValue)
                    {
                        OrderController omgr = new OrderController();
                        var order = omgr.ListOrder(allBills[0].OrderID.Value);
                        if(order != null)
                        {
                            FNameBox.Text = order.Fname;
                            LNameBox.Text = order.LName;
                            PhoneBox.Text = order.Phone;
                            StreetBox.Text = order.Street;
                            City.Text = order.City;
                            APTBox.Text = order.AptNumber;
                        }
                       
                    }
                }

                SplitBillsPanel.Visible = false;
                BillSplitterControl.SetInitialTable(allBills);
                BillPayment.SetTableBills(allBills);
            }

        }

        protected void MenuItemInfoDropDown_MemoAdded(object s, AddMemoEventArgs e)
        {
            MenuItemInfoDropDown thing = s as MenuItemInfoDropDown;
            RepeaterItem repeaterItem = thing.Parent as RepeaterItem;
            int billIndex = (repeaterItem.Parent.Parent as RepeaterItem).ItemIndex;
            BillItem item = allBills[billIndex].BillItems.ToList()[repeaterItem.ItemIndex];
            item.Notes = e.Memo;
            BillController sysmgr = new BillController();
            allBills = sysmgr.UpdateBillItemMemo(item);
        }

        protected void MenuItem_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int ItemID = int.Parse(btn.CommandArgument.Split(';')[0]);
            MenuItemController sysmgr = new MenuItemController();
            Data.Entities.MenuItem item = sysmgr.GetByID(ItemID);

                BillItem billItem = new BillItem();
                billItem.BillID = allBills[0].BillID;
                billItem.MenuItemID = item.MenuItemID;
                billItem.SellingPrice = item.CurrentPrice;
                billItem.ItemName = item.MenuItemName;
                billItem.Split = 1;
                BillController billmgr = new BillController();
                Dictionary<List<Bill>, int> result = billmgr.addBillItem(billItem);
            allBills = result.FirstOrDefault().Key;

            if (item.isCombo)
            {
                ComboItemSelector.AllowedSelections = item.AmountOfSelections.Value;
                ComboItemSelector.Show(result.FirstOrDefault().Value);
            }

        }

        protected void MenuItemInfoDropDown_Remove(object sender, RemoveItemEventArgs e)
        {
            MenuItemInfoDropDown infoBtn = sender as MenuItemInfoDropDown;
            RepeaterItem repeaterItem = infoBtn.Parent as RepeaterItem;
            int billIndex = (repeaterItem.Parent.Parent as RepeaterItem).ItemIndex;
            BillItem item = allBills[billIndex].BillItems.ToList()[repeaterItem.ItemIndex];
            List<BillItem> Splits = allBills.SelectMany(x => x.BillItems).Where(y => y.Split == item.Split).Where(y => y.MenuItemID == item.MenuItemID).Take(item.Split).ToList();
            BillController sysmgr = new BillController();
            allBills = sysmgr.RemoveBillItem(Splits);
        }

        protected void MenuItemInfoDropDown_DiscountAdded(object sender, AddDiscountEventArgs e)
        {
            MenuItemInfoDropDown infoBtn = sender as MenuItemInfoDropDown;
            RepeaterItem repeaterItem = infoBtn.Parent as RepeaterItem;
            int billIndex = (repeaterItem.Parent.Parent as RepeaterItem).ItemIndex;
            BillItem item = allBills[billIndex].BillItems.ToList()[repeaterItem.ItemIndex];
            if (e.DiscountDollar.HasValue)
            {
                item.SellingPrice = item.SellingPrice - e.DiscountDollar.Value;
            }
            if (e.DiscountPercent.HasValue)
            {
                decimal percent = e.DiscountPercent.Value / 100;
                decimal complimentPercent = ((decimal)1.00 - percent);
                decimal DiscountAmount = item.SellingPrice * complimentPercent;
                item.SellingPrice = DiscountAmount;
            }
            if (e.DiscountRemove == true)
            {
                if(item.Split < 2)
                {
                    item.SellingPrice = item.MenuItem.CurrentPrice;
                }
                else
                {
                    item.SellingPrice = item.MenuItem.CurrentPrice / (decimal)item.Split;
                }
                
            }
            BillController sysmgr = new BillController();
            allBills = sysmgr.UpdateItemPrice(item);
        }

        protected void addCustomerCount_Click(object sender, EventArgs e)
        {
            int customercount;
            if(int.TryParse(CustomerCountTB.Text, out customercount))
            {
                if(allBills[0].BillID == 0)
                {
                    allBills[0].CustomerCount = customercount;
                    BillController sysmgr = new BillController();
                    allBills = sysmgr.CreateBill(allBills[0]);
                    CustomerCountPanel.Visible = false;
                    BillInfoPanel.Visible = true;
                    NavPanel.Visible = true;
                }
                else
                {
                    allBills[0].CustomerCount = customercount;
                    BillController sysmgr = new BillController();
                    allBills = sysmgr.UpdateBillCustomerCount(allBills[0]);
                    CustomerCountPanel.Visible = false;
                    BillInfoPanel.Visible = true;
                    NavPanel.Visible = true;
                }
                
            }
        }

        protected void cancelCustomerCount_Click(object sender, EventArgs e)
        {
            if(allBills[0].BillID == 0)
            {
                Response.Redirect("~/POS/SeatingChart.aspx");
            }
            else
            {
                CustomerCountPanel.Visible = false;
                BillInfoPanel.Visible = true;
            }
        }

        protected void CategorySelected(object sender, CommandEventArgs e)
        {
            foreach(RepeaterItem item in CategoryRepeater.Items)
            {
                Button b = item.FindControl("CatBtn") as Button;
                if(b.CommandArgument.ToString() == e.CommandArgument.ToString())
                {
                    b.CssClass = "btn btn-info wrap";
                }
                else
                {
                    b.CssClass = "btn btn-primary wrap";
                }
            }
            foreach(RepeaterItem item in MenuCategoryRepeater.Items)
            {
                HiddenField field = item.FindControl("HdnCategoryName") as HiddenField;
                Panel p = field.Parent as Panel;
                
                if (field.Value.ToString() == e.CommandArgument.ToString())
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        protected void BillItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label discountLbl = (Label)e.Item.FindControl("ItemDiscount");
                int billIndex = (e.Item.Parent.Parent as RepeaterItem).ItemIndex;
                BillItem item = allBills[billIndex].BillItems.ToList()[e.Item.ItemIndex];
                if (item.MenuItem.isCombo)
                {
                    BillController sysmgr = new BillController();
                    List<ComboItemDetail> selections = sysmgr.getComboItemsByBillItem(item.BillItemID);
                    Repeater rep = (Repeater)e.Item.FindControl("ItemSelectionsRepeater");
                    if(rep != null)
                    {
                        rep.DataSource = selections;
                        rep.DataBind();
                    }
                    
                }
                if(item.Split < 2)
                {
                    decimal discount = item.SellingPrice - item.MenuItem.CurrentPrice;
                    if (discount != 0)
                        discountLbl.Text = discount.ToString("C2");
                }
                else
                {
                    decimal discount = item.SellingPrice - item.MenuItem.CurrentPrice / (decimal)item.Split;
                    if (discount != 0)
                        discountLbl.Text = discount.ToString("C2");
                }

            }
        }

        protected void BillRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label DiscountPrompt = e.Item.FindControl("DiscountPromptlbl") as Label;
                Label SubLabel = e.Item.FindControl("SubtotalLabel") as Label;
                Label TaxLabel = e.Item.FindControl("GSTLabel") as Label;
                Label DiscountAmountLabel = e.Item.FindControl("DiscountLabel") as Label;
                Label TotalCostLbl = e.Item.FindControl("TotalLabel") as Label;
                Label TipLbl = e.Item.FindControl("TipAmountLbl") as Label;
                Label PaidLbl = e.Item.FindControl("PaidAmountLbl") as Label;
                Label TipLblPrompt = e.Item.FindControl("TipPromptLbl") as Label;
                Label PaidLblPrompt = e.Item.FindControl("PaidPromptLbl") as Label;


                decimal dbPrice = allBills[e.Item.ItemIndex].BillItems.Where(x => x.Split < 2).Select(x => x.MenuItem.CurrentPrice).Sum();
                decimal sellPrice = allBills[e.Item.ItemIndex].BillItems.Sum(x => x.SellingPrice);
                decimal gst = (sellPrice * (decimal)0.05);
                decimal NonSplitdiscount = allBills[e.Item.ItemIndex].BillItems.Where(x => x.Split < 2).Select(x => x.SellingPrice).Sum() - dbPrice;
                decimal SplitDiscount = allBills[e.Item.ItemIndex].BillItems.Where(x => x.Split >= 2).Select(x => x.SellingPrice).Sum() - allBills[e.Item.ItemIndex].BillItems.Where(x => x.Split >= 2).Select(x => x.MenuItem.CurrentPrice / (decimal)x.Split).Sum();

                decimal tip = allBills[e.Item.ItemIndex].Tip;
                decimal paid = (decimal)0.00;

                if (tip > (decimal)0.00)
                {
                    TipLbl.Visible = true;
                    TipLblPrompt.Visible = true;
                    TipLbl.Text = tip.ToString("C2");
                }
                else
                {
                    TipLbl.Visible = false;
                    TipLblPrompt.Visible = false;
                }

                if (allBills[e.Item.ItemIndex].Payments != null)
                {
                    if (allBills[e.Item.ItemIndex].Payments.Count > 0)
                    {
                        paid = allBills[e.Item.ItemIndex].Payments.Sum(x => x.PaymentAmount);
                        paidPromptLbl.Visible = true;
                        PaidLbl.Visible = true;
                    }
                }

                PaidLbl.Text = paid.ToString("C2");


                decimal discount = NonSplitdiscount + SplitDiscount;
                if (discount != 0)
                {
                    if (DiscountPrompt != null)
                    {
                        DiscountPrompt.Visible = true;
                        DiscountAmountLabel.Visible = true;
                        DiscountAmountLabel.Text = discount.ToString("C2");
                    }

                }
                else
                {
                    if (DiscountPrompt != null)
                    {
                        DiscountPrompt.Visible = false;
                        DiscountAmountLabel.Visible = false;
                        DiscountAmountLabel.Text = "";
                    }

                }
                if (SubLabel != null)
                {
                    SubLabel.Text = sellPrice.ToString("C2");
                    TaxLabel.Text = gst.ToString("C2");
                    TotalCostLbl.Text = (sellPrice + gst + discount).ToString("C2");
                }
            }
            

        }

        protected void SplitBillBtn_Click(object sender, EventArgs e)
        {
            BillSplitterControl.SetInitialTable(allBills);
            SplitBillsPanel.Visible = true;
            BillInfoPanel.Visible = false;
            BillPaymentPanel.Visible = false;
            CustomerCountPanel.Visible = false;
            
        }

        protected void BillSplitterControl_TableSplit(object sender, BillSplitEventArgs e)
        {
            if (e.wasSuccess)
            {
                SplitBillsPanel.Visible = false;
                BillInfoPanel.Visible = true;
                BillController sysmgr = new BillController();
                allBills = sysmgr.GetAllBillsByTable(allBills[0].TableNumber, 1);
            }
        }

        protected void tempPayBtn_Click(object sender, EventArgs e)
        {
            BillPayment.SetTableBills(allBills);
            BillPaymentPanel.Visible = !BillPaymentPanel.Visible;
            if (BillPaymentPanel.Visible)
            {
                BillInfoPanel.Visible = false;
                CustomerCountPanel.Visible = false;
                SplitBillsPanel.Visible = false;
            }
            else
            {
                BillInfoPanel.Visible = true;
                CustomerCountPanel.Visible = false;
                SplitBillsPanel.Visible = false;
            }
            
        }

        protected void SelectMenuItems_Click(object sender, EventArgs e)
        {
                BillInfoPanel.Visible = true;
                CustomerCountPanel.Visible = false;
                SplitBillsPanel.Visible = false;
                BillPaymentPanel.Visible = false;
        }

        protected void AdjustCustomerCount_Click(object sender, EventArgs e)
        {
            BillInfoPanel.Visible = false;
            CustomerCountPanel.Visible = true;
            SplitBillsPanel.Visible = false;
            BillPaymentPanel.Visible = false;
        }

        protected void ItemsSelected(object sender, ItemSelectedEventArgs e)
        {
            foreach(Shanghai.Data.Entities.MenuItem item in ComboItemSelector.SelectedItems)
            {
                ComboItemSelection selection = new ComboItemSelection();
                selection.BillItemID = ComboItemSelector.ParentItemID;
                selection.MenuItemId = item.MenuItemID;
                BillController sysmgr = new BillController();
                allBills = sysmgr.addComboSelection(selection);
            }
            BillRepeater.DataBind();
        }

        protected void MenuItemInfoDropDown_UpdateItems(object s, OptionUpdateComboEventArgs e)
        {
            MenuItemInfoDropDown infoBtn = s as MenuItemInfoDropDown;
            RepeaterItem repeaterItem = infoBtn.Parent as RepeaterItem;
            int billIndex = (repeaterItem.Parent.Parent as RepeaterItem).ItemIndex;
            BillItem item = allBills[billIndex].BillItems.ToList()[repeaterItem.ItemIndex];
            BillController sysmgr = new BillController();
            ComboItemSelector.EditComboItem(sysmgr.getComboSelections(item));
        }

        protected void ComboItemSelector_ItemsUpdated(object s, ItemsUpdatedEventArgs e)
        {
            BillRepeater.DataBind();
        }

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            
            int customercount;
            if (int.TryParse(CustomerCountTB.Text, out customercount))
            {
                if (allBills[0].BillID == 0)
                {
                    Order order = new Order()
                    {
                        Fname = FNameBox.Text,
                        LName = LNameBox.Text,
                        Phone = PhoneBox.Text,
                        Street = StreetBox.Text,
                        AptNumber = APTBox.Text,
                        City = City.Text,
                        CompletedYN = false,
                        OrderDate = DateTime.Now,
                        GST = (decimal)0.00
                    };
                    if (isTakeOut.Value == "false")
                    {
                        order.DeliverFee = (decimal)4.5;
                        order.OrderTypeID = 1;

                    }
                    else
                    {
                        order.OrderTypeID = 2;
                    }
                    OrderController omgr = new OrderController();
                    order = omgr.CreateOrder(order);
                    allBills[0].CustomerCount = customercount;
                    allBills[0].OrderID = order.OrderID;
                    BillController sysmgr = new BillController();
                    allBills = sysmgr.CreateBill(allBills[0]);
                    CustomerCountPanel.Visible = false;
                    BillInfoPanel.Visible = true;
                    NavPanel.Visible = true;
                }
                else
                {
                    OrderController omgr = new OrderController();
                     omgr.updateOrder(FNameBox.Text, LNameBox.Text, PhoneBox.Text, StreetBox.Text, APTBox.Text, City.Text, allBills[0].OrderID.Value);
                    allBills[0].CustomerCount = customercount;
                    BillController sysmgr = new BillController();
                    allBills = sysmgr.UpdateBillCustomerCount(allBills[0]);
                    CustomerCountPanel.Visible = false;
                    BillInfoPanel.Visible = true;
                    NavPanel.Visible = true;
                }

            }
        }
    }

}