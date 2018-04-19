using Shanghai.Data.Entities;
using Shanghai.System.BLL;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.BackEnd_Page
{
    public partial class Lookup_Order : BasePage
    {
        int result;

        protected void Page_Load(object sender, EventArgs e)
        {
            DeliveryPanel.Visible = false;
            PickupPanel.Visible = false;
            OrderDetailsPanel.Visible = false;
            StatusLabel.Visible = false;
            TypeLabel.Visible = false;
        }

        protected void LookupOrders_Click(object sender, EventArgs e)
        {
            ErrorMsg.Visible = false;
            OrderPanel.Visible = true;
            DeliveryPanel.Visible = false;
            PickupPanel.Visible = false;
            StatusLabel.Visible = false;
            TypeLabel.Visible = false;

        }

        protected void LookupDetails_Click(object sender, EventArgs e)
        {
            ErrorMsg.Visible = false;
            
            if(int.TryParse(OrderNumberTextbox.Text, out result))
            {

                var controller = new OrderController();
                var billController = new BillController();
                
                var custInfo = controller.ListOrder(result);

                if (custInfo != null)
                {
                    var billInfo = billController.GetBill(custInfo.OrderID);
                    var orderInfo = controller.ListOrderDetails(result);
                    var orderTotals = controller.OrderTotals(result);

                    var paid = billInfo.PaidYN;
                    var type = custInfo.OrderTypeID;

                    if (paid == true)
                    {
                        StatusLabel.Text = "Order Status: Completed";
                    }
                    else
                    {
                        StatusLabel.Text = "Order Status: Not Complete";
                    }

                    if(type == 1)
                    {
                        TypeLabel.Text = "Order Type: Delivery";
                    }
                    else
                    {
                        TypeLabel.Text = "Order Type: Pickup";
                    }

                    StatusLabel.Visible = true;
                    TypeLabel.Visible = true;

                    displaySubtotal.Text = orderTotals.subtotal.ToString("C");
                    displayTotal.Text = orderTotals.total.ToString("C");
                    displayGST.Text = orderTotals.gst.ToString("C");

                    if (custInfo.OrderTypeID == 1)
                    {
                        DeliveryPanel.Visible = true;
                        PickupPanel.Visible = false;
                        displayDelivery.Visible = true;
                        displayDelivery.Text = "Delivery:   " + "$3.00";

                        DLCustomerName.Text = custInfo.Fname + " " + custInfo.LName;
                        DLAddress.Text = custInfo.Street;
                        DLPhone.Text = custInfo.Phone;
                    }
                    else if (custInfo.OrderTypeID == 2)
                    {
                        DeliveryPanel.Visible = false;
                        PickupPanel.Visible = true;
                        displayDelivery.Visible = false;

                        PUCustomerName.Text = custInfo.Fname + " " + custInfo.LName;
                        PUTime.Text = (custInfo.PickupTime).ToString();
                        PUPhone.Text = custInfo.Phone;
                    }

                    CommentsTextbox.Text = custInfo.SpecialInstructions;
                    OrderDetailsPanel.Visible = true;

                } // end if custInfo != null
                else
                {
                    TypeLabel.Text = "";
                    TypeLabel.Visible = false;
                    ErrorMsg.Visible = true;
                    ErrorMsg.Text = "No order with that number. Please try again";
                }
            }
            else
            {
                TypeLabel.Text = "";
                TypeLabel.Visible = false;
                ErrorMsg.Visible = true;
                ErrorMsg.Text = "Invalid Order Number. Please try again";
            }
        }


        protected void OrderItemListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hf = e.Item.FindControl("isComboHF") as HiddenField;
            if (hf != null)
            {
                int menuItemId = int.Parse((e.Item.FindControl("MenuItemIDLabel") as Label).Text);
                int orderId = int.Parse(OrderNumberTextbox.Text);
                if (bool.Parse(hf.Value))
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