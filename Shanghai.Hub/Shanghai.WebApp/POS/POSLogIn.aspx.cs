using Shanghai.System.BLL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.POS
{
    public partial class POSLogIn : POSPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoggedInEmployee = null;
            }
        }

        protected void ValidateEmployee_Click(Object sender, EventArgs e)
        {
            //EmployeeController CheckID = new EmployeeController();
           
            //string result;
            int emplogin;
            if (int.TryParse(EmployeeIDBox.Text, out emplogin))
            {

                //    result = (CheckID.Employee_GetByEmployeeID(empId)).ToString();
                if ((Login(emplogin)) == false)
                {
                    MessageUserControl.ShowError("Login", "This user does not exist.");
                    // message saying employee does not exist
                }
                else
                {
                    WorkHoursController HoursCheck = new WorkHoursController();
                    bool ActiveShift = HoursCheck.FindActiveWorkHours(LoggedInEmployee.EmployeeID);
                    if (ActiveShift)
                    {
                        Response.Redirect("SeatingChart.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/POS/POSClockIn.aspx");
                    }
                }
            }
            else
            {
                MessageUserControl.ShowError("Login", "Please Enter A Valid Code");
                // Please enter a valid number
            }
        }
    }
}