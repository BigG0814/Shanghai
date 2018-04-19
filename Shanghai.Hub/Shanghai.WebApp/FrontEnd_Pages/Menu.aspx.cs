using Shanghai.Data.Entities;
using Shanghai.Data.POCOs;
using Shanghai.System.BLL;
using Shanghai.WebApp.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp
{
    public partial class Menu : Page
    {
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

            if(day != DayOfWeek.Saturday || day != DayOfWeek.Sunday && ct > weekdayOpening && ct < weekdayClosing)
            {
                MessagePanel.Visible = false;
                isResturantOpen = true;
            }
            else if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday && ct > weekdayOpening && ct < weekdayClosing)
            {
                MessagePanel.Visible = false;
                isResturantOpen = true;
            }
            else
            {
                isResturantOpen = false;
                
            }
                
        }

        protected void AddToCart_Command(object sender, CommandEventArgs e)
        {
            Thread.Sleep(500);

            LinkButton btn = (LinkButton)(sender);
            var itemId = int.Parse(btn.CommandArgument.Split(';')[0]);
            bool isCombo = bool.Parse(e.CommandArgument.ToString().Split(';')[1]);
            var allowedSelections = int.Parse(String.IsNullOrEmpty(e.CommandArgument.ToString().Split(';')[2])? "0" : e.CommandArgument.ToString().Split(';')[2]);
            if (isCombo)
            {
                ComboItemSelector.Show(int.Parse(btn.CommandArgument.Split(';')[0]));
                ComboItemSelector.AllowedSelections = allowedSelections;//the number of allowed selections for that item
            }
            else
            {
                int cartId = Global.userCartId;

                var controller = new CartController();
                var cartItem = controller.AddItemToCart(cartId, itemId);
                if (isCombo)
                    ComboItemSelector.ParentItemID = cartItem;
                Tuple<int, decimal> cartInfo = controller.getCountAndPrice(cartId);

                FrontFacing.itemCount = cartInfo.Item1;
                FrontFacing.priceTotal = cartInfo.Item2.ToString("F");
            }
        }

        protected void ComboItemSelector_SelectionSubmitted(object sender, ItemSelectedEventArgs e)
        {
            int cartId = Global.userCartId;
            var itemID = ComboItemSelector.ParentItemID;
            var selectedMenuItems = e.SelectedMenuIDs;
            var controller = new CartController();
            List<ComboItemSelection> list = new List<ComboItemSelection>();
            foreach (int id in selectedMenuItems)
            {
                ComboItemSelection selection = new ComboItemSelection();
                selection.ComboMenuItemID = ComboItemSelector.ParentItemID;
                selection.MenuItemId = id;
                list.Add(selection);
                
            }
            //This updates the combo item with the ShoppingCartItemID
            int cartItemID = controller.addComboSelections(list, cartId);

            //This adds the selected items to the ComboItemsTable
            controller.updateComboSelection(list, cartItemID);

            Tuple<int, decimal> cartInfo = controller.getCountAndPrice(cartId);

            FrontFacing.itemCount = cartInfo.Item1;
            FrontFacing.priceTotal = cartInfo.Item2.ToString("F");
        }
    }
}