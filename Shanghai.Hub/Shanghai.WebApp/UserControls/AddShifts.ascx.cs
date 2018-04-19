using Shanghai.Data.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.UserControls
{
    public partial class AddShifts : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BindDropDown();
        }

        public void AddShift()
        {
            ShiftController sysmgr = new ShiftController();

            Shift newShift = new Shift();
            newShift.EmployeeID = int.Parse(EmployeeDDL.SelectedValue);
            newShift.ShiftDate = DateTime.Parse(DateLabel.Text);
            newShift.JobTypeID = int.Parse(JobIDHdnField.Value);
            DateTime Date = DateTime.Parse(DateLabel.Text);
            DateTime startTime = DateTime.Parse(StartTimeTB.Text);
            DateTime endTime = DateTime.Parse(EndTimeTB.Text);

            newShift.StartTime = new DateTime(Date.Year, Date.Month, Date.Day, startTime.Hour, startTime.Minute, startTime.Second);
            newShift.EndTime = new DateTime(Date.Year, Date.Month, Date.Day, endTime.Hour, endTime.Minute, endTime.Second);

            List<Shift> oldShifts = new List<Shift>();
            oldShifts = sysmgr.EmployeeDayJobTypeShift_Get(newShift.EmployeeID, newShift.JobTypeID, Date);
            bool exist = false;
            foreach (var item in oldShifts)
            {
                if (newShift.StartTime < item.EndTime)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "showPopUp();", true);
                MessageLabel.Text = "Warning: The employee shift has a conflict";
            }
            else
            {
                MessageLabel.Text = "";
                sysmgr.Create_Shift(newShift);
                ClearForm();
            }




        }

        public void ClearForm()
        {
            //BindDropDown();
            //EmployeeDDL.SelectedIndex = 0;
            StartTimeTB.Text = null;
            EndTimeTB.Text = null;
        }

        //public void BindDropDown()
        //{
        //    EmployeeController sysmgr = new EmployeeController();
        //    List<Employee> result = sysmgr.List_Employee();
        //    EmployeeDDL.DataSource = result;
        //    EmployeeDDL.DataTextField = "FullName";
        //    EmployeeDDL.DataValueField = "EmployeeID";
        //    EmployeeDDL.DataBind();
        //}

        public void SetDateSetJob(string date, string job, string jobid)
        {
            DateLabel.Text = date;
            JobTypeLabel.Text = job;
            JobIDHdnField.Value = jobid;
        }
    }
}