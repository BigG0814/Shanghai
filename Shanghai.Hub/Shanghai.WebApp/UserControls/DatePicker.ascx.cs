using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.UserControls
{
    public delegate void OnDateChanged(object sender, DateChangedEventArgs e);
    public partial class DatePicker : UserControl
    {
        public event OnDateChanged DateChanged;

        public string Text { get; set; }

        public string WeekEndingText
        {
            get
            {
                return TB_Week_Ending.Text;
            }
            set
            {
                TB_Week_Ending.Text = value;
            }
        }


        public DateTime WeekEnding
        {
            get
            {
                return (DateTime.Parse(ViewState["WeekEndingPick"].ToString()));
            }
            set
            {
                ViewState["WeekEndingPick"] = value;
            }
        }
        public DateTime WeekStarting
        {
            get
            {
                return (DateTime.Parse(ViewState["WeekStartingPick"].ToString()));
            }
            set
            {
                ViewState["WeekStartingPick"] = value;
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return Calender1.SelectedDate;
            }
            set
            {
                Calender1.SelectedDate = value;
            }
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Text != null)
            {
                TB_Week_Ending.Text = Text;
            }
            if (!Page.IsPostBack)
            {
                Calender1.SelectedDate = DateTime.Today;
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
                WeekStarting = weekending.AddDays(-6);
                WeekEnding = weekending;
                TB_Week_Ending.Text = WeekStarting.ToLongDateString() + " - " + weekending.ToLongDateString();
            }
            
        }

        protected void CalImg_Click(object sender, ImageClickEventArgs e)
        {
            if (Calender1.Visible)
            {
                Calender1.Visible = false;
            }
            else
            {
                Calender1.Visible = true;
            }
        }

        protected void Calender1_SelectionChanged(object sender, EventArgs e)
        {
            Calender1.Visible = false;
            DateTime weekending = Calender1.SelectedDate;
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
            WeekEnding = weekending;
            WeekStarting = weekending.AddDays(-6);
            TB_Week_Ending.Text = WeekStarting.ToLongDateString() + " - " + weekending.ToLongDateString();
            DateChangedEventArgs dateChangedEventArgument = new DateChangedEventArgs(weekending, WeekStarting);
            DateChanged(this, dateChangedEventArgument);

        }
    }
}