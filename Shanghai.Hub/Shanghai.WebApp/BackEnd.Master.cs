
using System;
using Shanghai.Security.BLL;
using Shanghai.Security.Entities;
using System.Web;

using System.Web.UI;
using System.Configuration;

namespace Shanghai.WebApp
{
    public partial class BackEnd : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // checks if user is logged on and authorized to view certain things
            if (Request.IsAuthenticated)
            {
                if (!Page.User.IsInRole("Managers"))
                    if(!Page.User.IsInRole("POSSYSTEM"))
                {
                    A0.Visible = false;
                }
                if (Page.User.IsInRole("POSSYSTEM"))
                    Response.Redirect("~/POS/POSLogin");
               
                if((Page as BasePage).LoggedinEmployee != null)
                {
                    HelloLabel.Text = (Page as BasePage).LoggedinEmployee.FullName;
                }
            }

        }

        protected void LogOut(object sender, EventArgs e)
        {
            var AuthenticationManger = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManger.SignOut();
            Response.Redirect("~/Account/Login.aspx");
            
        }

    }
}