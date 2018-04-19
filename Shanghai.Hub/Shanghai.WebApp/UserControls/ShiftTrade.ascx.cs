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
    public delegate void OnShiftTradeActionTaken(object s, ShiftTradeActionEventArgs e);


    public partial class ShiftTrade : UserControl
    {
        public Shanghai.Data.Entities.ShiftTrade ShiftTraded
        {
            get
            {
                return ViewState["ShiftBeingTraded"] as Shanghai.Data.Entities.ShiftTrade;

            }
            set
            {
                ViewState["ShiftBeingTraded"] = value;
            }
        }
        public string ShiftDate
        {
            get
            {
                return ShiftDateL.Text;
            }
            set
            {
                ShiftDateL.Text = value;
            }
        }
        public string StartTime
        {
            get
            {
                return StartTimeL.Text;
            }
            set
            {
                StartTimeL.Text = value;
            }
        }
        public string EndTime
        {
            get
            {
                return EndTimeL.Text;
            }
            set
            {
                EndTimeL.Text = value;
            }
        }
        public string InitialEmployee
        {
            get
            {
                return InitialEmployeeL.Text;
            }
            set
            {
                InitialEmployeeL.Text = value;
            }
        }
        public string NewEmployee
        {
            get
            {
                return NewEmployeeL.Text;
            }
            set
            {
                NewEmployeeL.Text = value;
            }
        }

        public Employee loggedInEmp
        {
            get
            {
                return ViewState["shiftTradeLoggedInEmp"] as Employee;
            }
            set
            {
                ViewState["shiftTradeLoggedInEmp"] = value;
            }
        }

        public string Reasons
        {
            get
            {
                return ReasonsTB.Text;
            }
            set
            {
                ReasonsTB.Text = value;
            }
        }
        public int ShiftID
        {
            get
            {
                if (ViewState["TadingShiftID"] == null)
                    return 0;
                return (int)ViewState["TadingShiftID"];
            }
            set
            {
                ViewState["TadingShiftID"] = value;
            }
        }

        public event OnShiftTradeActionTaken ActionTaken;
        protected void Page_Load(object sender, EventArgs e)
        {
            TradePanel.Visible = true;
            //Get New Or Existing Shift Trade From BLL (Pass ShiftID into BLL. See if a shift trade exists! if it does, return that trade!

            // if it doesnt exist return: new ShiftTrade();
            //Put values in appropriate spots!

            
        }

        public void SetShiftNewOrExistingShiftTrade(int shiftid, Employee emp)
        {
            loggedInEmp = emp;
            ShiftTradingProcessController sysmg = new ShiftTradingProcessController();
            var shiftTrade = sysmg.ShiftTradeRequest_Get(shiftid);
            if (shiftTrade != null)
            {
                
                ShiftDateL.Text = shiftTrade.Shift.ShiftDate.ToLongDateString();
                StartTimeL.Text = shiftTrade.Shift.StartTime.ToLongTimeString().ToString();
                EndTimeL.Text = shiftTrade.Shift.EndTime.ToLongTimeString().ToString();
                EmployeeController empmg = new EmployeeController();
                int orempid = shiftTrade.OriginalEmployee;
                InitialEmployeeL.Text = empmg.Employee_GetByEmployeeID(orempid).FullName;
                ReasonsTB.Text = shiftTrade.Reason;
                ReasonsTB.ReadOnly = true;
                if(loggedInEmp.EmployeeID == shiftTrade.OriginalEmployee)
                {
                    ApproveTrade.Visible = false;
                    AcceptTrade.Visible = false;
                    PostTrade.Visible = false;
                }
                else
                {
                    AcceptTrade.Visible = true;
                    ApproveTrade.Visible = false;
                    PostTrade.Visible = false;
                    DeleteOffer.Visible = false;
                }
                if (this.Page.User.IsInRole("Managers") || this.Page.User.IsInRole("Administrators"))
                {
                    ApproveTrade.Visible = true;
                }
                ShiftTraded = shiftTrade;

            }
            else
            {
                Shift shift = sysmg.GetShiftByID(shiftid);
                if (shift != null)
                {
                    ShiftDate = shift.ShiftDate.ToLongDateString();
                    StartTime = shift.StartTime.ToShortTimeString();
                    EndTime = shift.EndTime.ToShortTimeString();
                    InitialEmployee = shift.Employee.FullName;
                    if (loggedInEmp.EmployeeID == shift.EmployeeID)
                    {
                        ApproveTrade.Visible = false;
                        AcceptTrade.Visible = false;
                        DeleteOffer.Visible = false;
                    }
                    ShiftTraded = shiftTrade;
                }
            }
        }

        #region Trade Processing four buttons


        protected void PostTrade_Click(object sender, EventArgs e)
        {
            //Create New Shift Trade In DataBase
            ShiftTradingProcessController sysmr = new ShiftTradingProcessController();

            Data.Entities.ShiftTrade newShiftTrade = new Data.Entities.ShiftTrade();
            newShiftTrade.ShiftID = ShiftID;
            var emp = loggedInEmp.EmployeeID;
            newShiftTrade.OriginalEmployee = emp;

            newShiftTrade.IsApproved = false;
            newShiftTrade.TimePosted = DateTime.Now;
            newShiftTrade.Reason = Reasons;
            sysmr.Create_ShiftTrade(newShiftTrade);
            ActionTaken(this, new ShiftTradeActionEventArgs());
        }

        protected void AcceptTrade_Click(object sender, EventArgs e)
        {
            //Update Shift Trade = Set New Employee To Current Employee
            ShiftTradingProcessController sysmr = new ShiftTradingProcessController();
            var emp = loggedInEmp.EmployeeID;
            ShiftTraded.NewEmployee = emp;
            sysmr.Update_ShiftTrade(ShiftTraded);
            ActionTaken(this, new ShiftTradeActionEventArgs());
        }

        protected void ApproveTrade_Click(object sender, EventArgs e)
        {
            //Update Shift Trade = Set Approving Employee, Set isApproved,
            ShiftTradingProcessController sysmr = new ShiftTradingProcessController();
            var emp = loggedInEmp.EmployeeID;
            ShiftTraded.ApprovingEmployee = emp;
            ShiftTraded.IsApproved = true;
            
            sysmr.Update_ShiftTrade(ShiftTraded);

            //Update Shift With New Employee
            sysmr.Update_Shift(ShiftTraded);
            ActionTaken(this, new ShiftTradeActionEventArgs());
        }
        
        protected void Cancel_Click(object sender, EventArgs e)
        {
            ActionTaken(this, new ShiftTradeActionEventArgs());
            //Close Panel
        }
        #endregion

        protected void DeleteOffer_Click(object sender, EventArgs e)
        {
            ShiftTradingProcessController sysmgr = new ShiftTradingProcessController();
            sysmgr.deleteTrade(ShiftTraded.TradeID);
            ActionTaken(this, new ShiftTradeActionEventArgs());
        }
    }

    
}