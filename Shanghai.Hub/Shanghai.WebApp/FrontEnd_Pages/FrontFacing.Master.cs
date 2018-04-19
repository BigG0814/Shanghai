using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp
{
    public partial class FrontFacing : MasterPage
    {
        public static int itemCount = 0;
        public static string priceTotal = "0.00";

        protected void Page_Load(object sender, EventArgs e)
        {
            // checks if user is logged on and authorized to view certain things
            if (!Request.IsAuthenticated)
            {
                Menu_LogOut.Visible = false;
                Menu_BackEnd.Visible = false;
                Menu_LogIn.Visible = true;
            }
            else
            {
                if (Page.User.IsInRole("POSSYSTEM"))
                {
                    Response.Redirect("~/POS/poslogin.aspx");
                }
                Menu_LogOut.Visible = true;
                Menu_BackEnd.Visible = true;
                Menu_LogIn.Visible = false;
            }

            var controller = new CartController();
            int cartID = Global.userCartId;

            if (cartID != 0)
            {
                Tuple<int, decimal> cartInfo = controller.getCountAndPrice(cartID);

                itemCount = cartInfo.Item1;
                priceTotal = cartInfo.Item2.ToString("F");
            }
            else
            {
                // this should never be executed..
                itemCount = 0;
                priceTotal = "0.00";
            }
            
        }

        protected void LogOut(object sender, EventArgs e)
        {
            var AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            Response.Redirect("~/Account/Login.aspx");
            
        }
    }
}