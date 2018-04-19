using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using Shanghai.Data.Entities;
using Microsoft.AspNet.Identity;
using Shanghai.Security.BLL;

namespace Shanghai.WebApp
{
    public partial class ViewCart : Page
    {
        decimal delivery = 0.00M;
        int cartId = Global.userCartId;

        public bool isResturantOpen
        {
            get
            {
                return (bool)ViewState["IsResturantOpen"];
            }
            set
            {
                ViewState["isResturantOpen"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime ct = DateTime.Now;
            var day = ct.DayOfWeek;
            //DateTime ct = Convert.ToDateTime("2:00:00 PM");
            DateTime weekdayOpening = Convert.ToDateTime("10:00:00 AM");
            DateTime weekdayClosing = Convert.ToDateTime("10:00:00 PM");
            DateTime weekendOpening = Convert.ToDateTime("4:00:00 PM");
            DateTime weekendClosing = Convert.ToDateTime("10:00:00 PM");

            if (day != DayOfWeek.Saturday || day != DayOfWeek.Sunday && ct > weekdayOpening && ct < weekdayClosing)
            {
                MessagePanel.Visible = false;
                isResturantOpen = true;
            }
            else if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday && ct > weekdayOpening && ct < weekdayClosing)
            {
                AlertPanel.Visible = false;
                MessagePanel.Visible = false;
                isResturantOpen = true;
            }
            else
            {
                AlertPanel.Visible = false;
                isResturantOpen = false;

            }

        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        private void DisplayTotals()
        {
            decimal subtotal = 0.00M;
            decimal gst = 0.00M;
            decimal total = 0.00M;

            foreach (ListViewDataItem row in CartListView.Items)
            {
                var totalLabel = row.FindControl("Total") as Label;
                subtotal += decimal.Parse(totalLabel.Text, NumberStyles.Currency);
            }
            gst = subtotal * 0.05M;
            total = subtotal + gst + delivery;

            displaySubtotal.Text = subtotal.ToString("C");
            displayTotal.Text = total.ToString("C");
            displayGST.Text = gst.ToString("C");
            deliveryLabel.Text = delivery.ToString("C");
            displayTotal.Text = total.ToString("C");
        }

        protected void SubmitOrderDetails_Click(object sender, EventArgs e)
        {
            List<string> contactInfo = new List<string>();
            var cartController = new CartController();
            var orderController = new OrderController();
            var userManager = new UserManager();
            var managers = userManager.GetManagerUsers();
            var shiftController = new ShiftController();
            if (managers != null)
            {
                contactInfo = shiftController.GetOnShiftManagerContactInfo(managers);
            }
            else
            {
                //TODO - CHANGE DEFAULT CONTACT
                string defaultContact1 = "7809861862@msg.telus.com";
                string defaultContact2 = "7809861883@msg.telus.com";
                contactInfo.Add(defaultContact1);
                contactInfo.Add(defaultContact2);             
            }
                int orderID = 0;
                var orderDetails = new List<OrderDetail>();
                List<int> comboIds = new List<int>();

            if (CartListView.Items.Count == 0)
            {
                AlertPanel.Visible = true;
            }
            else
            {
                foreach (ListViewDataItem row in CartListView.Items)
                {
                    var detail = new OrderDetail();
                    Label menuItemIDLabel = row.FindControl("MenuItemIDLabel") as Label;
                    detail.MenuItemID = int.Parse(menuItemIDLabel.Text);
                    Label quantityLabel = row.FindControl("QuantityLabel") as Label;
                    detail.Quantity = int.Parse(quantityLabel.Text);
                    Label priceLabel = row.FindControl("Price") as Label;
                    detail.SellingPrice = decimal.Parse(priceLabel.Text, NumberStyles.Currency);
                    orderDetails.Add(detail);
                    HiddenField hf = row.FindControl("isComboHF") as HiddenField;
                    if (bool.Parse(hf.Value))
                    {
                        int id = int.Parse((row.FindControl("CartItemID") as HiddenField).Value);
                        comboIds.Add(id);
                    }
                }

                var order = new Order();
                order.GST = decimal.Parse(displayGST.Text, NumberStyles.Currency);
                order.OrderDate = DateTime.Now;

                if (DeliveryDetailsPanel.Visible == true)
                {
                    order.Fname = FirstNameTextBoxDL.Text;
                    order.LName = LastNameTextBoxDL.Text;
                    order.Phone = PhoneTextBoxDL.Text;
                    order.AptNumber = AptTextBoxDL.Text;
                    order.Email = EmailTextBoxDL.Text;
                    order.OrderTypeID = 1;
                    order.Street = AddressTextBoxDL.Text;
                    order.City = "Leduc";
                    order.SpecialInstructions = CommentsDL.Text;
                    order.DeliverFee = decimal.Parse(deliveryLabel.Text, NumberStyles.Currency);
                    order.CompletedYN = false;

                    orderID = orderController.PlaceOrder(order, orderDetails, contactInfo, cartId, comboIds);

                }
                else if (PickupDetailsPanel.Visible == true)
                {

                    order.Fname = FirstNameTextBoxPU.Text;
                    order.LName = LastNameTextBoxPU.Text;
                    order.Phone = PhoneTextBoxPU.Text;
                    TimeSpan puTime = new TimeSpan();
                    switch (timeSelect.SelectedValue)
                    {
                        case "0":
                            // error
                            break;
                        case "12":
                            puTime = TimeSpan.Parse("12:00:00");
                            break;
                        case "1":
                            puTime = TimeSpan.Parse("13:00:00");
                            break;
                        case "2":
                            puTime = TimeSpan.Parse("14:00:00");
                            break;
                        case "3":
                            puTime = TimeSpan.Parse("15:00:00");
                            break;
                        case "4":
                            puTime = TimeSpan.Parse("16:00:00");
                            break;
                        case "5":
                            puTime = TimeSpan.Parse("17:00:00");
                            break;
                        case "6":
                            puTime = TimeSpan.Parse("18:00:00");
                            break;
                        case "7":
                            puTime = TimeSpan.Parse("19:00:00");
                            break;
                        case "8":
                            puTime = TimeSpan.Parse("20:00:00");
                            break;
                        default:
                            // error
                            break;

                    }
                    order.PickupTime = puTime;
                    order.Email = EmailTextBoxPU.Text;
                    order.OrderTypeID = 2;
                    order.SpecialInstructions = CommentsPU.Text;
                    order.CompletedYN = false;
                    orderID = orderController.PlaceOrder(order, orderDetails, contactInfo, cartId, comboIds);
                }
                else
                {
                    // error
                }

                //if no error then...
                //delete cart from DB
                cartController.DeleteOpenCart(cartId);
                Global.userCartId = cartController.StartNewCart();
                if (orderID != 0)
                {
                    Session["ConfirmedOrder"] = orderID;
                    Response.Redirect("OrderConfirmed.aspx");
                }
            }
        }

        protected void CartListView_DataBound(object sender, EventArgs e)
        {
            DisplayTotals();
        }

        protected void CartListView_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {
            DisplayTotals();
        }

        protected void PickUpButton_OnClick(object sender, EventArgs e)
        {
            DeliveryDetailsPanel.Visible = false;
            PickupDetailsPanel.Visible = true;      

            deliveryLabel.Visible = false;
            deliveryDisplayLabel.Visible = false;


            DisplayTotals();

        }

        protected void DeliveryButton_OnClick(object sender, EventArgs e)
        {
            PickupDetailsPanel.Visible = false;
            DeliveryDetailsPanel.Visible = true;

            deliveryLabel.Visible = true;
            deliveryDisplayLabel.Visible = true;

            delivery = 3.00M;
            DisplayTotals();

        }

        protected void CartDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["cartid"] = cartId.ToString();
        }

        protected void CartListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
           
            HiddenField hf = e.Item.FindControl("isComboHF") as HiddenField;
            if(hf != null)
            {
                int id = int.Parse((e.Item.FindControl("CartItemID") as HiddenField).Value);
                if (bool.Parse(hf.Value))
                {
                    Repeater rep = e.Item.FindControl("ComboRepeater") as Repeater;
                    OrderController sysmgr = new OrderController();
                    rep.DataSource = sysmgr.ListComboSelections(id);
                    rep.DataBind();
                }
            }
            
        }
    }
}