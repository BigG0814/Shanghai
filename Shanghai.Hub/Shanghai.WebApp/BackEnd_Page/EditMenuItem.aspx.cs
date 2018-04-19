using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.BackEnd_Page
{
    public partial class EditMenuItem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx", true);

            }
        }

        protected void BindEditMenu(object sender, EventArgs e)
        {
          
            EditMenu_CategoryDDL.Items.Clear();
            EditMenu_CategoryDDL.DataBind();
            EditMenu_CategoryDDL.Items.Insert(0, new ListItem { Text = "All", Value = "0", Selected=true});
            EditMenuListView.DataBind();
            
        }
    }
}