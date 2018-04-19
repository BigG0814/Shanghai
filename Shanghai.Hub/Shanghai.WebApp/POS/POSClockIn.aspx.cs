using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.POS
{
    public partial class POSClockIn : POSPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoggedInEmployee == null)
            {
                Response.Redirect("~/POS/POSLogIn.aspx");
            }
            bool ActiveShift = false;
            if (LoggedInEmployee == null)
            {
                ActiveShift = false;
            }
            else
            {
                WorkHoursController HoursCheck = new WorkHoursController();
                ActiveShift = HoursCheck.FindActiveWorkHours(LoggedInEmployee.EmployeeID);
            }
            SeatingLink.Visible = ActiveShift;
            CurrentUser.Text = LoggedInEmployee.FullName;
            if (!Page.IsPostBack)
            {
                BindShiftRepeater();
            }
                
            


        }

        private void BindShiftRepeater()
        {
            WorkHoursController sysmgr = new WorkHoursController();
            HoursRepeater.DataSource = sysmgr.GetWorkHoursByEmployee(LoggedInEmployee.EmployeeID);
            HoursRepeater.DataBind();
        }

        protected void StartShiftBtn_Init(object sender, EventArgs e)
        {
            bool ActiveShift = false;
            if (LoggedInEmployee == null)
            {
                ActiveShift = false;
            }
            else
            {
                WorkHoursController HoursCheck = new WorkHoursController();
                ActiveShift = HoursCheck.FindActiveWorkHours(LoggedInEmployee.EmployeeID);
            }
            if (ActiveShift)
                (sender as Button).Visible = false;
            else
                (sender as Button).Visible = true;
        }

        protected void EndShiftBtn_Init(object sender, EventArgs e)
        {
            bool ActiveShift = false;
            if (LoggedInEmployee == null)
            {
                ActiveShift = false;
            }
            else
            {
                WorkHoursController HoursCheck = new WorkHoursController();
                ActiveShift = HoursCheck.FindActiveWorkHours(LoggedInEmployee.EmployeeID);
            }
            
            if (ActiveShift)
                (sender as Button).Visible = true;
            else
                (sender as Button).Visible = false;
        }

        protected void StartShiftBtn_Click(object sender, EventArgs e)
        {
            WorkHoursController HoursCheck = new WorkHoursController();
            HoursCheck.StartWorkHours(LoggedInEmployee.EmployeeID);
            BindShiftRepeater();
            StartShiftBtn.Visible = false;
            EndShiftBtn.Visible = true;
            SeatingLink.Visible = true;
           
        }

        protected void EndShiftBtn_Click(object sender, EventArgs e)
        {
            WorkHoursController HoursCheck = new WorkHoursController();
            HoursCheck.EndWorkHours(LoggedInEmployee.EmployeeID);
            BindShiftRepeater();
            StartShiftBtn.Visible = true;
            EndShiftBtn.Visible = false;
            SeatingLink.Visible = false;
        }
    }
}