using Shanghai.Data.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp
{
    public partial class OrderConfirmed : Page
    {
        string orderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            // this is working to get the orderID of the confirmed order
            try
            {
                orderID = Session["ConfirmedOrder"].ToString();
            }
            catch
            {
                Response.Redirect("~/FrontEnd_Pages/Menu.aspx");
            }
            GetOrderDetails(orderID);   
        }

        private void GetOrderDetails(string orderID)
        {
            var controller = new OrderController();

            var orderDetails = controller.ListOrder(int.Parse(orderID));

            datePlacedLabel.Text = "Date Placed: " + orderDetails.OrderDate.ToString();
            if(orderDetails.OrderTypeID == 1) // delivery
            {
                pickupLabel.Visible = false;
                pickupTimeLabel.Visible = false;
                deliveryLabel.Visible = true;
                deliveryLabel.Text = "Order Type: Delivery";
                deliveryEstimateLabel.Visible = true;
                deliveryEstimateLabel.Text = "Delivery Time Estimate: 45-60 minutes";
                deliveryAddressLabel.Visible = true;
                deliveryAddressLabel.Text = "Order will be delivered to " + orderDetails.Street;
                confirmationTag.Text = "Your order is on it's way!";

            }
            else
            {
                deliveryLabel.Visible = false;
                deliveryEstimateLabel.Visible = false;
                deliveryAddressLabel.Visible = false;
                pickupLabel.Visible = true;
                pickupLabel.Text = "Order Type: Pickup";
                pickupTimeLabel.Visible = true;
                pickupTimeLabel.Text = "Pickup Time: " + orderDetails.PickupTime.Hours + ":00";
                displayDelivery.Visible = false;
                confirmationTag.Text = "Your order is being prepared!";

            }

            nameLabel.Text = "Customer Name: " + orderDetails.Fname + " " + orderDetails.LName;
            phoneLabel.Text = "Customer Phone: " + orderDetails.Phone;
            emailLabel.Text = "Confirmation email sent to " + orderDetails.Email;
            var orderTotals = controller.OrderTotals(int.Parse(orderID));

            displaySubtotal.Text = orderTotals.subtotal.ToString("C");
            displayTotal.Text = orderTotals.total.ToString("C");
            displayGST.Text = orderTotals.gst.ToString("C");
            if (orderTotals.delivery != null)
            {
                displayDelivery.Visible = true;
                displayDelivery.Text = "Delivery:   " + "$3.00"; 
            }

            else
            {
                displayDelivery.Visible = false;
            }


        }

        protected void OrderItemListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hf = e.Item.FindControl("isComboHF") as HiddenField;
            if(hf != null)
            {
                int menuItemId = int.Parse((e.Item.FindControl("MenuItemIDLabel") as Label).Text);
                int orderId = int.Parse((e.Item.FindControl("OrderID") as HiddenField).Value);
                if(bool.Parse(hf.Value))
                {
                    Repeater rep = e.Item.FindControl("ComboRepeater") as Repeater;
                    OrderController controller = new OrderController();
                    rep.DataSource = controller.ListComboSelections(menuItemId, orderId);
                    rep.DataBind();
                }
            }
        }
    }
}