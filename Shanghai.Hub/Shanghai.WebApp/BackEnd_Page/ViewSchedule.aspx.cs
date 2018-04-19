using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shanghai.WebApp.UserControls;
using Shanghai.System.BLL;
using Shanghai.Data.POCOs;
using System.Data.Entity;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Shanghai.System.DAL;
using Shanghai.Data.Entities;

namespace Shanghai.WebApp.Scheduling
{

    public partial class ViewSchedule : BasePage
    {
        public bool CopyRestrict { get; set; }
        public bool isManager
        {
            get
            {
                return (bool)ViewState["MockRole"];
            }
            set
            {
                ViewState["MockRole"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            
            //isManager = true;
            isManager = User.IsInRole(ConfigurationManager.AppSettings["manageRole"]);
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx", true);

            }
            else if (!Page.IsPostBack)
            {
                DateTime weekending = DateTime.Today;
                for (int i = 0; i < 6; i++)
                {
                    if (weekending.DayOfWeek == DayOfWeek.Sunday)
                    {
                        i = 6;
                    }
                    else
                    {
                        weekending = weekending.AddDays(1);
                    }
                }
                DateTime start = weekending.AddDays(-6);
                DateTime end = weekending;
                if (isManager)
                {
                    BindAllWeeks(start, end);
                }
                else
                {
                    EmployeeBindAllWeeks(start, end);
                }
            }
            if (!isManager)
            {
                setfinalized.Visible = false;
                setnextweeksched.Visible = false;
                setUnfinalized.Visible = false;
            }
        }

        protected void FinalizeWeekShifts_Click(object sender, EventArgs e)
        {
            ShiftController sysmgr = new ShiftController();
            List<ShiftGroup> shifts = sysmgr.GetShiftsByWeek(DatePicker.SelectedDate, DatePicker.SelectedDate.AddDays(7));
            foreach (ShiftGroup shiftgroup in shifts)
            {
                foreach (ShiftSummary sum in shiftgroup.Shifts)
                {
                    sysmgr.SetShiftToFinalized(sum.ShiftID);
                }
            }
            BindSchedualByDatePicker();
        }

        protected void UnFinalizeWeekShifts_Click(object sender, EventArgs e)
        {
            ShiftController sysmgr = new ShiftController();
            List<ShiftGroup> shifts = sysmgr.GetShiftsByWeek(DatePicker.SelectedDate, DatePicker.SelectedDate.AddDays(7));
            foreach (ShiftGroup shiftgroup in shifts)
            {
                foreach (ShiftSummary sum in shiftgroup.Shifts)
                {
                    sysmgr.SetShiftToUnFinalized(sum.ShiftID);
                }
            }
            BindSchedualByDatePicker();
        }
        protected void SetNextWeekSchedule_Click(object sender, EventArgs e)
        {
            if (CopyRestrict == true)
            {
                MessageUserControl.ShowInfo("You just copied this week schedule");
            }
            else
            {
                ShiftController sysmgr = new ShiftController();
                List<ShiftGroup> shifts = sysmgr.GetShiftsByWeek(DatePicker.SelectedDate, DatePicker.SelectedDate.AddDays(7));
                //Pass in list of shifts
                foreach (ShiftGroup shiftGroup in shifts)
                {

                    foreach (ShiftSummary sum in shiftGroup.Shifts)
                    {
                        Shift newShift = new Shift();
                        newShift.EmployeeID = sum.EmployeeId;
                        newShift.EndTime = sum.EndTime.AddDays(7);
                        newShift.StartTime = sum.StartTime.AddDays(7);
                        newShift.JobTypeID = sum.JobTypeID;
                        newShift.ShiftDate = shiftGroup.Date.AddDays(7);

                        sysmgr.Create_Shift(newShift);



                    }
                }
            }
            CopyRestrict = true;

        }

        #region Switch Weeks
        protected void PreviousWeek_Click(object sender, EventArgs e)
        {
            CopyRestrict = false;
            HiddenField mon = null;
            foreach (RepeaterItem item in MondayRepeater.Items)
            {
                mon = item.FindControl("ShiftDate") as HiddenField;
            }
            DateTime theMonday = DateTime.Parse(mon.Value).AddDays(-7);
            DateTime weekEndDay = theMonday.AddDays(6);
            DatePicker.WeekEndingText = theMonday.ToLongDateString() + " - " + weekEndDay.ToLongDateString();
            BindAllWeeks(theMonday, weekEndDay);
            DatePicker.SelectedDate = theMonday;

        }
        protected void NextWeek_Click(object sender, EventArgs e)
        {
            CopyRestrict = false;
            HiddenField mon = null;
            foreach (RepeaterItem item in MondayRepeater.Items)
            {
                mon = item.FindControl("ShiftDate") as HiddenField;
            }
            DateTime theMonday = DateTime.Parse(mon.Value).AddDays(7);
            DateTime weekEndDay = theMonday.AddDays(6);
            DatePicker.WeekEndingText = theMonday.ToLongDateString() + " - " + weekEndDay.ToLongDateString();
            BindAllWeeks(theMonday, weekEndDay);
            DatePicker.SelectedDate = theMonday;
        }
        #endregion
        private void BindAllWeeks(DateTime start, DateTime end)
        {
            ShiftController sysmgr = new ShiftController();
            List<ShiftGroup> shifts = sysmgr.GetShiftsByWeek(start, end);

            MondayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday);
            TuesdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday);
            WednesdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday);
            ThursdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday);
            FridayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday);
            SaturdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Saturday);
            SundayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Sunday);
            MondayRepeater.DataBind();
            TuesdayRepeater.DataBind();
            WednesdayRepeater.DataBind();
            ThursdayRepeater.DataBind();
            FridayRepeater.DataBind();
            SaturdayRepeater.DataBind();
            SundayRepeater.DataBind();
        }
        private void EmployeeBindAllWeeks(DateTime start, DateTime end)
        {
            ShiftController sysmgr = new ShiftController();
            List<ShiftGroup> shifts = sysmgr.GetFinalizedShiftListByWeek(start, end);

            MondayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday);
            TuesdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday);
            WednesdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday);
            ThursdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday);
            FridayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday);
            SaturdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Saturday);
            SundayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Sunday);
            MondayRepeater.DataBind();
            TuesdayRepeater.DataBind();
            WednesdayRepeater.DataBind();
            ThursdayRepeater.DataBind();
            FridayRepeater.DataBind();
            SaturdayRepeater.DataBind();
            SundayRepeater.DataBind();
        }

        private void BindAllWeeks(DateTime start, DateTime end, int EmployeeID)
        {
            ShiftController sysmgr = new ShiftController();
            List<ShiftGroup> shifts = sysmgr.GetShiftsByWeekByEmployee(start, end, EmployeeID);
            MondayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday);
            TuesdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday);
            WednesdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday);
            ThursdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday);
            FridayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday);
            SaturdayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Saturday);
            SundayRepeater.DataSource = shifts.Where(x => x.Date.DayOfWeek == DayOfWeek.Sunday);
            MondayRepeater.DataBind();
            TuesdayRepeater.DataBind();
            WednesdayRepeater.DataBind();
            ThursdayRepeater.DataBind();
            FridayRepeater.DataBind();
            SaturdayRepeater.DataBind();
            SundayRepeater.DataBind();
            if (shifts == null)
            {
                MessageUserControl.ShowInfo("No Results", "You don't have any shifts this week");
            }
        }

        protected void DatePicker_DateChanged(object s, DateChangedEventArgs e)
        {
            DateTime start = e.WeekStarting;
            DateTime end = e.WeekEnding;
            BindAllWeeks(start, end);
            //retrieve shifts where date >= start and <= end
        }

        protected void AddShift_icon_Click(object s, EventArgs e)
        {
            AddShiftPanel.Visible = true;

            LinkButton sender = s as LinkButton;
            RepeaterItem item = sender.Parent as RepeaterItem;
            DateTime date = DateTime.Parse((item.FindControl("ShiftDate") as HiddenField).Value);

            string argument = sender.CommandArgument;
            string jobID = argument.Split(':')[0];
            string jobDescription = argument.Split(':')[1];
            UserControl_AddShifts.SetDateSetJob(date.ToLongDateString(), jobDescription, jobID);
            lblJavaScript.Text = "<script language=\"JavaScript\">ShowPopup()</script>";

        }

        protected void AddShift_Click(object s, EventArgs e)
        {

            UserControl_AddShifts.AddShift();
            AddShiftPanel.Visible = false;
            BindSchedualByDatePicker();
            lblJavaScript.Text = "";
        }

        private void BindSchedualByDatePicker()
        {
            DateTime weekending = DatePicker.SelectedDate;
            for (int i = 0; i < 6; i++)
            {
                if (weekending.DayOfWeek == DayOfWeek.Sunday)
                {
                    i = 6;
                }
                else
                {
                    weekending = weekending.AddDays(1);
                }
            }
            DateTime start = weekending.AddDays(-6);
            DateTime end = weekending;
            BindAllWeeks(start, end);
        }

        protected void CancelAddShift_Click(object s, EventArgs e)
        {
            UserControl_AddShifts.ClearForm();
            AddShiftPanel.Visible = false;
            lblJavaScript.Text = "";
        }


        protected void DeleteShift(object sender, CommandEventArgs e)
        {
            ShiftController shiftmgr = new ShiftController();
            shiftmgr.DeleteShift(int.Parse(e.CommandArgument.ToString()));
            BindSchedualByDatePicker();
        }

        protected void InnerRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string itemcount = (((Repeater)sender).Items.Count + 1).ToString();

            Label cntrl = (((Repeater)sender).Parent as RepeaterItem).FindControl("JobTypeCountLabel") as Label;
            var TradeBtn = e.Item.FindControl("TradeBtn") as HtmlControl;
            int ShiftID = int.Parse(((e.Item.FindControl("linkBtn_Trade") as LinkButton)).CommandArgument);
            SetTradeBtnPropertiesByShift(ShiftID, TradeBtn);
            Label empName = e.Item.FindControl("EmpName") as Label;
            if (empName.Text == LoggedinEmployee.FullName)
            {
                empName.Font.Bold = true;
            }
            cntrl.Text = "(" + itemcount + ")";
            HiddenField sd = (((Repeater)sender).Parent as RepeaterItem).FindControl("ShiftDate") as HiddenField;

        }


        protected void SetTradeBtnPropertiesByShift(int ShiftID, HtmlControl ctl)
        {
            ShiftController sysmgr = new ShiftController();
            ShiftTradingProcessController mgr = new ShiftTradingProcessController();
            var shift = sysmgr.GetShiftByID(ShiftID);
            var shiftTrade = mgr.ShiftTradeRequest_Get(ShiftID);
            if (shiftTrade == null)
            {
                if (LoggedinEmployee.EmployeeID == shift.EmployeeID)
                {
                    ctl.Visible = true;
                    ctl.Attributes.CssStyle.Add("color", "black");
                }
                else
                {
                    ctl.Visible = false;
                }
            }
            else
            {
                ctl.Visible = true;
                ctl.Attributes.CssStyle.Add("color", "red");
                if (shiftTrade.NewEmployee.HasValue)
                {
                    ctl.Attributes.CssStyle.Add("color", "green");
                }
            }

        }



        protected void ViewSelfSchedule_Checked(object sender, EventArgs e)
        {
            HiddenField mon = null;
            foreach (RepeaterItem item in MondayRepeater.Items)
            {
                mon = item.FindControl("ShiftDate") as HiddenField;
            }
            DateTime theMonday = DateTime.Parse(mon.Value);
            DateTime weekEndDay = theMonday.AddDays(6);
            if (SelfScheduleCB.Checked)
            {
                int logemp = LoggedinEmployee.EmployeeID;

                BindAllWeeks(theMonday, weekEndDay, logemp);
            }
            else
            {
                BindAllWeeks(theMonday, weekEndDay);
            }
        }

        protected void PostShiftTrade(object sender, CommandEventArgs e)
        {
            if (LoggedinEmployee == null)
                throw new Exception("No Employee!");
            int shiftid = int.Parse(e.CommandArgument.ToString());
            UserControl_ShiftTrade.ShiftID = shiftid;
            UserControl_ShiftTrade.SetShiftNewOrExistingShiftTrade(shiftid, LoggedinEmployee);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "ShiftTradePopup();", true);
        }

        protected void UserControl_ShiftTrade_ActionTaken(object sender, ShiftTradeActionEventArgs e)
        {
            BindAllWeeks(DatePicker.WeekStarting, DatePicker.WeekEnding.Date);
        }
    }
}