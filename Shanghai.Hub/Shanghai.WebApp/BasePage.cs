using Shanghai.Data.Entities;
using Shanghai.Security.BLL;
using Shanghai.Security.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Shanghai.WebApp
{
    public class BasePage : Page
    {
        public Employee LoggedinEmployee
        {
            get
            {
                return Session["employeloginid"] as Employee;
            }
            set
            {
                Session["employeloginid"] = value;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (Request.IsAuthenticated)
            {
                UserManager sysmgr = new UserManager();
                LoggedinEmployee = sysmgr.GetEmployeeByLoggedInUser(User.Identity.Name);
            }
        }
    }
}