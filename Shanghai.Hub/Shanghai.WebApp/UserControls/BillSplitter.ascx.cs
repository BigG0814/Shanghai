using Shanghai.Data.Entities;
using Shanghai.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Shanghai.System.BLL;

namespace Shanghai.WebApp.UserControls
{
    public partial class BillSplitter : UserControl
    {


        [Serializable]
        internal class SelectedBillItem : BillItemDetail
        {
            public int BillIndex { get; set; }
            public int ItemIndex { get; set; }
        }

        public Bill initialBill
        {
            get
            {
                if (Session[this.UniqueID.ToString() + "_initialBill"] == null)
                    return new Bill();
                return Session[this.UniqueID.ToString() + "_initialBill"] as Bill;
            }
            set
            {
                Session[this.UniqueID.ToString() + "_initialBill"] = value;
            }
        }

        public List<TempBill> bills
        {
            get
            {
                if (Session[this.UniqueID.ToString() + "_tempBill"] == null)
                    return new List<TempBill>();
                return Session[this.UniqueID.ToString() + "_tempBill"] as List<TempBill>;
            }
            set
            {
                Session[this.UniqueID.ToString() + "_tempBill"] = value;
            }
        }

        private List<SelectedBillItem> selectedItems
        {
            get
            {
                if (Session[this.UniqueID.ToString() + "_selectedItems"] == null)
                    return new List<SelectedBillItem>();
                return Session[this.UniqueID.ToString() + "_selectedItems"] as List<SelectedBillItem>;
            }
            set
            {
                Session[this.UniqueID.ToString() + "_selectedItems"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SplitMenuItemBtn.Visible = false;
            }
        }

        public void SetInitialTable(List<Bill> initialBills)
        {
            initialBill = null;
            bills.Clear();
            initialBill = initialBills[0];
            foreach (Bill bill in initialBills)
            {
                TempBill tBill = new TempBill();
                tBill.EmployeeID = bill.EmployeeID;
                decimal dbPrice = bill.BillItems.Select(x => x.MenuItem.CurrentPrice).Sum();
                decimal sellPrice = bill.BillItems.Sum(x => x.SellingPrice);
                decimal gst = (sellPrice * (decimal)0.05);
                tBill.SubTotal = sellPrice;
                tBill.GST = gst;
                tBill.OriginalBillID = bill.BillID;
                tBill.PaidYN = false;
                tBill.TableNumber = bill.TableNumber;
                tBill.items = new List<BillItemDetail>();
                foreach (BillItem item in bill.BillItems)
                {
                    BillItemDetail det = new BillItemDetail();
                    det.MenuItemID = item.MenuItemID;
                    det.OriginalMenuItemName = item.MenuItem.MenuItemName;
                    det.MenuItemName = item.ItemName;
                    det.Notes = item.Notes;
                    det.SellingPrice = item.SellingPrice;
                    det.Split = item.Split;
                    if (item.MenuItem.isCombo)
                    {
                        BillController sysmgr = new BillController();
                        det.comboItems = sysmgr.getComboItemsByBillItem(item.BillItemID);
                    }
                    tBill.items.Add(det);
                }
                List<TempBill> tempList = bills;
                tempList.Add(tBill);
                bills = tempList;
            }
            TempBillRepeater.DataSource = bills;
            TempBillRepeater.DataBind();
        }

        protected void AddTableBtn_Click(object sender, EventArgs e)
        {
            TempBill newBill = new TempBill()
            {
                EmployeeID = bills[0].EmployeeID,
                GST = 0,
                isClosed = false,
                items = new List<BillItemDetail>(),
                OriginalBillID = null,
                PaidYN = false,
                SubTotal = 0,
                TableNumber = bills[0].TableNumber
            };
            bills.Add(newBill);
            TempBillRepeater.DataSource = bills;
            TempBillRepeater.DataBind();
        }


        protected void ItemBtn_Click(object sender, EventArgs e)
        {
            Button sendBtn = sender as Button;
            if (sendBtn.BackColor != Color.AliceBlue)
                sendBtn.BackColor = Color.AliceBlue;
            else
                sendBtn.BackColor = Color.Black;
            int ItemIndex = int.Parse((sender as Button).CommandArgument.ToString());
            RepeaterItem main = ((sender as Button).Parent.Parent as Repeater).Parent as RepeaterItem;
            int BillIndex = int.Parse(((main.FindControl("BillIndex") as HiddenField).Value));
            var thing = bills[BillIndex].items[ItemIndex];
            SelectedBillItem item = new SelectedBillItem()
            {
                BillIndex = BillIndex,
                ItemIndex = ItemIndex,
                MenuItemID = thing.MenuItemID,
                OriginalMenuItemName = thing.OriginalMenuItemName,
                MenuItemName = thing.MenuItemName,
                Notes = thing.Notes,
                comboItems = thing.comboItems,
                SellingPrice = thing.SellingPrice
            };

            var selectedItem = selectedItems.Where(x => x.BillIndex == item.BillIndex && x.ItemIndex == ItemIndex).FirstOrDefault();
            if (selectedItem == null)
            {
                List<SelectedBillItem> items = selectedItems;
                items.Add(item);
                selectedItems = items;
            }
            else
            {
                selectedItems.Remove(selectedItem);
            }
            if (selectedItems.Count >= 1)
            {
                SplitMenuItemBtn.Visible = true;
            }
            else
            {
                SplitMenuItemBtn.Visible = false;
            }
        }

        protected void BillBtn_Click(object sender, EventArgs e)
        {

        }

        protected void SplitItemBtn_Click(object sender, EventArgs e)
        {

            foreach (SelectedBillItem item in selectedItems)
            {
                try
                {
                    BillItemDetail billItem = bills[item.BillIndex].items[item.ItemIndex];
                    int split = int.Parse(SplitNumber_tb.Text);
                    List<BillItemDetail> ItemSplits = new List<BillItemDetail>();
                    if (billItem.Split > 1)
                    {
                        //If Split is 3
                        //Gather all instances of that split
                        foreach (TempBill b in bills)
                        {
                            //List of Items on bill B;
                            List<BillItemDetail> existingItems = b.items.Where(x => x.MenuItemID == billItem.MenuItemID)
                                                .Where(x => x.MenuItemName == billItem.MenuItemName)
                                                .Where(x => x.Split == billItem.Split).ToList();

                            foreach (BillItemDetail removeMe in existingItems)
                            {
                                ItemSplits.Add(removeMe);
                                b.items.Remove(removeMe);
                                if (ItemSplits.Count == billItem.Split)
                                    break;
                            }
                            if (ItemSplits.Count == billItem.Split)
                                break;
                        }

                        int SplitItemCount = billItem.Split - split; //Meaning the splits are being increased
                        if (SplitItemCount < 0)
                        {

                            SplitItemCount = SplitItemCount * -1;
                            for (int i = 0; i < SplitItemCount + ItemSplits.Count; i++)
                            {
                                BillItemDetail detail = new BillItemDetail();
                                if (i < ItemSplits.Count)
                                    detail = ItemSplits[i];
                                detail.MenuItemID = billItem.MenuItemID;
                                if (split == 1)
                                {
                                    detail.MenuItemName = billItem.OriginalMenuItemName;
                                }
                                else
                                {
                                    detail.MenuItemName = "1/" + split.ToString() + " " + billItem.OriginalMenuItemName;
                                }
                                detail.SellingPrice = (billItem.SellingPrice * billItem.Split) / (decimal)split;
                                detail.OriginalMenuItemName = billItem.OriginalMenuItemName;
                                detail.Split = split;
                                detail.comboItems = billItem.comboItems;
                                bills[0].items.Add(detail);
                            }
                        }
                        else //splits are being consolidated
                        {
                            List<BillItemDetail> removeItems = ItemSplits.Take(SplitItemCount).ToList();
                            if (removeItems.Count != 0)
                            {
                                foreach (BillItemDetail billDetail in removeItems)
                                {
                                    ItemSplits.Remove(billDetail);
                                }
                            }
                            foreach (BillItemDetail detail in ItemSplits)
                            {
                                detail.MenuItemID = detail.MenuItemID;
                                detail.SellingPrice = (detail.SellingPrice * detail.Split) / (decimal)split;
                                if (split == 1)
                                    detail.MenuItemName = detail.OriginalMenuItemName;
                                else
                                    detail.MenuItemName = "1/" + split + " " + detail.OriginalMenuItemName;
                                detail.Split = split;
                                detail.comboItems = billItem.comboItems;
                                bills[0].items.Add(detail);
                            }
                        }
                    }
                    else
                    {
                        bills[item.BillIndex].items.Remove(bills[item.BillIndex].items[item.ItemIndex]);
                        for (int i = 1; i <= split; i++)
                        {
                            BillItemDetail detail = new BillItemDetail();
                            detail.MenuItemID = billItem.MenuItemID;
                            detail.OriginalMenuItemName = billItem.OriginalMenuItemName;
                            if (split == 1)
                            {
                                detail.MenuItemName = billItem.OriginalMenuItemName;
                            }
                            else
                            {
                                detail.MenuItemName = "1/" + split.ToString() + " " + billItem.OriginalMenuItemName;
                            }
                            detail.SellingPrice = (billItem.SellingPrice * billItem.Split) / (decimal)split;
                            detail.Split = split;
                            detail.comboItems = billItem.comboItems;
                            bills[0].items.Add(detail);
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    //Do Nothing with the selected Item! It has already been consolidated!
                }
                catch (ArgumentOutOfRangeException)
                {
                    //Do nothing with the selected Item! It has already been consolidated!
                }

            }
            selectedItems.Clear();
            SplitMenuItemBtn.Visible = false;
            TempBillRepeater.DataSource = bills;
            TempBillRepeater.DataBind();
        }

        protected void BillDiv_Click(object sender, EventArgs e)
        {
            if (selectedItems.Count != 0)
            {
                selectedItems = selectedItems.OrderBy(x => x.BillIndex).OrderByDescending(x => x.ItemIndex).ToList();
                foreach (SelectedBillItem item in selectedItems)
                {
                    var existingItem = bills[item.BillIndex].items[item.ItemIndex];
                    int selectedBillIndex = int.Parse(((LinkButton)sender).CommandArgument);
                    if (bills[selectedBillIndex].items == null)
                        bills[selectedBillIndex].items = new List<BillItemDetail>();
                    bills[selectedBillIndex].items.Add(existingItem);
                    bills[item.BillIndex].items.Remove(bills[item.BillIndex].items[item.ItemIndex]);
                }
                selectedItems.Clear();
                SplitMenuItemBtn.Visible = false;
                TempBillRepeater.DataSource = bills;
                TempBillRepeater.DataBind();
            }
        }

        protected void SaveChangesBtn_Click(object sender, EventArgs e)
        {
            selectedItems.Clear();
            BillController sysmgr = new BillController();
            sysmgr.SaveTableSplit(bills);
            TableSplit(this, new BillSplitEventArgs(true));
        }

        public event OnTableSplitSaved TableSplit;

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            selectedItems.Clear();
            TableSplit(this, new BillSplitEventArgs(true));
        }
    }


    public delegate void OnTableSplitSaved(object s, BillSplitEventArgs e);
}