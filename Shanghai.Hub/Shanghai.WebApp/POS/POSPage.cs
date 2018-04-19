using Shanghai.Data.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Shanghai.WebApp.POS
{
    public class POSPage : Page, IPostBackEventHandler
    {
        public void RaisePostBackEvent(string eventArgument)
        {
            if(eventArgument == "REDIRECT")
            {
                Response.Redirect("~/POS/POSLogIn.aspx");
            }
        }


        public Employee LoggedInEmployee
        {
            get
            {
                return Session["LoggedInEmployee"] as Employee;
            }
            set
            {
                Session["LoggedInEmployee"] = value;
            }
        }
        public int SelectedTableID
        {
            get
            {
                if (Session["map_SelectedTable"] == null)
                    return 0;
                return int.Parse(Session["map_SelectedTable"].ToString());
            }
            set
            {
                Session["map_SelectedTable"] = value;
            }
        }


        public bool Login(int employeeCode)
        {
            EmployeeController sysmgr = new EmployeeController();
            LoggedInEmployee = sysmgr.GetEmployeeByLoginCode(employeeCode);
            if (LoggedInEmployee == null)
                return false;
            return true;
        }

        public void Logout()
        {
            LoggedInEmployee = null;
        }
        
    }
}