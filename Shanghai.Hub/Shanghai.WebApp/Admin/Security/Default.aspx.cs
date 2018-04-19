using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.Admin.Security
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx", true);
            }
            else if (!User.IsInRole(ConfigurationManager.AppSettings["manageRole"]))
            {
                Response.Redirect("~", true);
            }

        }
    }
}