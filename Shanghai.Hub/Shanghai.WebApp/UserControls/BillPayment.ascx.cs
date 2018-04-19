using Shanghai.Data.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.UserControls
{
    public partial class BillPayment : UserControl
    {
        public List<Bill> TableBills
        {
            get
            {
                return Session["PayableBills"] as List<Bill>;
            }
            set
            {
                Session["PayableBills"] = value;
                BillItemRepeater.DataSource = TableBills[0].BillItems;
                BillItemRepeater.DataBind();
            }
        }

        public int CurrentIndex
        {
            get
            {
                return (int)Session["BillIndex"];
            }
            set
            {
                Session["BillIndex"] = value;
            }
        }



        public Bill CurrentBill
        {
            get
            {
                return Session["BillInFocus"] as Bill;
            }
            set
            {
                TipAmountLbl.Text = "$0.00";
                PaymentAmountTB.Text = "";

                Session["BillInFocus"] = value;
                BillItemRepeater.DataSource = value.BillItems;
                BillItemRepeater.DataBind();
                BillLabel.Text = "Bill #" + value.BillID.ToString();
                decimal subtotal = value.BillItems.Sum(y => y.SellingPrice);
                decimal gst; 
                decimal total;
                decimal OutstandingAmount = (decimal)0.00;
                if (value.DeliveryFee.HasValue)
                {
                    DeliveryFeeLabel.Visible = true;
                    DeliveryFeePromptLbl.Visible = true;
                    DeliveryFeeLabel.Text = value.DeliveryFee.Value.ToString("C2");
                    gst = decimal.Round(((subtotal + value.DeliveryFee.Value) * (decimal)0.00), 2);
                    total = subtotal + value.DeliveryFee.Value + gst;
                    deliveryRow.Visible = true;
                }
                else
                {
                    deliveryRow.Visible = false;
                    DeliveryFeeLabel.Visible = false;
                    DeliveryFeePromptLbl.Visible = false;
                    DeliveryFeeLabel.Text = "0.00";
                    gst = decimal.Round(subtotal * (decimal)0.05, 2);
                    total = subtotal + gst;
                }
                if (value.Payments != null)
                {
                    if(value.Payments.Count > 0)
                    {
                        decimal totalPaid = value.Payments.Where(x => x.isVoid == false).Sum(x => x.PaymentAmount);
                        if (total > totalPaid)
                        {
                            OutstandingAmount = total - totalPaid;
                            TipAmountLbl.Visible = false;
                            TipPrompt.Visible = false;
                            tipRow.Visible = false;

                        }
                        else
                        {
                            tipRow.Visible = true;
                            OutstandingAmount = (decimal)0.00;
                            TipAmountLbl.Visible = true;
                            TipPrompt.Visible = true;
                            TipAmountLbl.Text = (totalPaid - total).ToString("C2");
                        }
                        if (value.Payments.Count > 0)
                        {
                            PaidPrompt.Visible = true;
                            PaymentRepeater.DataSource = value.Payments.OrderBy(x => x.BillPaymentID);
                            PaymentRepeater.DataBind();
                            TotalPaidLbl.Text = totalPaid.ToString("C2");
                            paidRow.Visible = true;
                        }
                        else
                        {
                            paidRow.Visible = false;
                        }
                    }
                    else
                    {
                        TotalPaidLbl.Text = (0.00).ToString("C2");
                        paidRow.Visible = false;
                        OutstandingAmount = total;
                    }
                    
                }
                else
                {
                    TotalPaidLbl.Text = (0.00).ToString("C2");
                    paidRow.Visible = false;
                    OutstandingAmount = total;
                }

                
                decimal NonSplitDiscount =           (value.BillItems
                                                    .Where(y => y.Split < 2)
                                                    .Sum(y => y.SellingPrice)
                                                        -
                                                     (value.BillItems
                                                    .Where(y => y.Split < 2)
                                                    .Sum(y => y.MenuItem.CurrentPrice)));
                decimal SplitDiscount = (value.BillItems.Where(y => y.Split >= 2).Sum(y => y.SellingPrice)) - (value.BillItems.Where(y => y.Split >= 2).Sum(y => y.MenuItem.CurrentPrice / y.Split));
                decimal discount = NonSplitDiscount + SplitDiscount;
                if (discount != 0)
                {
                    discountRow.Visible = true;
                    DiscountLabel.Visible = true;
                    DiscountPromptlbl.Visible = true;
                    DiscountLabel.Text = discount.ToString("C2");
                }
                else
                {
                    discountRow.Visible = false;
                    DiscountLabel.Visible = false;
                    DiscountPromptlbl.Visible = false;
                }
                SubtotalLabel.Text = subtotal.ToString("C2");
                GSTLabel.Text = gst.ToString("C2");
                TotalLabel.Text = total.ToString("C2");
                OutstandingLbl.Text = OutstandingAmount.ToString("C2");
                CloseBillBtn.Visible = value.PaidYN;
                if (value.isClosed)
                {
                    CloseBillBtn.Visible = true;
                    CloseBillBtn.Text = "Bill Closed";
                    CloseBillBtn.Enabled = false;

                }
                else
                {
                    CloseBillBtn.Visible = value.PaidYN;
                    CloseBillBtn.Text = "Close Bill";
                    CloseBillBtn.Enabled = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(CurrentBill != null)
            {
                BillItemRepeater.DataSource = CurrentBill.BillItems;
                BillItemRepeater.DataBind();
            }
        }

        public void SetTableBills(List<Bill> bills)
        {
            if(TableBills != null)
                TableBills.Clear();
            TableBills = bills;
            CurrentBill = TableBills[0];
            CurrentIndex = 0;
            if(TableBills.Count == 1)
            {
                NextBillBtn.Visible = false;
                PrevBillBtn.Visible = false;
            }
            else
            {
                NextBillBtn.Visible = true;
                PrevBillBtn.Visible = true;
            }
        }

        protected void SavePaymentBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PaymentAmountTB.Text))
            {
                decimal paymentAmount;
                if(decimal.TryParse(PaymentAmountTB.Text, out paymentAmount))
                {
                    int paymentType = int.Parse(PaymentTypeDropDown.SelectedValue);
                    Shanghai.Data.Entities.BillPayment newPayment = new Shanghai.Data.Entities.BillPayment();
                    newPayment.BillID = CurrentBill.BillID;
                    newPayment.isVoid = false;
                    newPayment.PaymentAmount = decimal.Round(paymentAmount,2);
                    newPayment.PaymentTypeID = paymentType;
                    BillController sysmgr = new BillController();
                    CurrentBill = sysmgr.addPayment(newPayment);
                    TableBills[CurrentIndex] = CurrentBill;
                }
                
            }
        }

        protected void NextBillBtn_Click(object sender, EventArgs e)
        {
            CurrentIndex += 1;
            if(CurrentIndex >= TableBills.Count)
            {
                CurrentIndex = 0;
            }
            CurrentBill = TableBills[CurrentIndex];
        }

        protected void PrevBillBtn_Click(object sender, EventArgs e)
        {
            CurrentIndex -= 1;
            if(CurrentIndex < 0)
            {
                CurrentIndex = TableBills.Count - 1;
            }
            CurrentBill = TableBills[CurrentIndex];
        }

        protected void VoidPayment_Click(object sender, EventArgs e)
        {
            Button voidBtn = (Button)sender;
            int paymentID = int.Parse(voidBtn.CommandArgument.ToString());
            BillController sysmgr = new BillController();
            CurrentBill = sysmgr.voidPayment(paymentID);
            TableBills[CurrentIndex] = CurrentBill;

        }

        protected void CloseBillBtn_Click(object sender, EventArgs e)
        {
            BillController sysmgr = new BillController();
            if (CurrentBill.PaidYN)
            {
                CurrentBill = sysmgr.closeBill(CurrentBill.BillID);
                TableBills[CurrentIndex] = CurrentBill;
                CurrentIndex += 1;
                if (CurrentIndex >= TableBills.Count)
                {
                    CurrentIndex = 0;
                }
                CurrentBill = TableBills[CurrentIndex];
                if (TableBills.Where(x => x.isClosed).Count() == TableBills.Count)
                {
                    Response.Redirect("~/POS/SeatingChart.aspx");
                }
            }
        }
    }
}