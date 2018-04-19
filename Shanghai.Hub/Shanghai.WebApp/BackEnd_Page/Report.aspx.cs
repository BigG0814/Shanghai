using Shanghai.Data.Entities;
using Shanghai.Data.POCOs;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.BackEnd_Page
{
    public partial class Report : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!Page.IsPostBack)
            {
                Calendar1.Visible = false;
                Calendar2.Visible = false;
                EmployeeReport.ShowReportBody = false;
                EmployeeReportByDate.ShowReportBody = false;
                ReportViewer3.ShowReportBody = false;
                ReportViewer1.Visible = true;
                ReportViewer2.Visible = true;
              
            }
        }
        protected void Button1_Click(Object sender, EventArgs e)
        {

            EmployeeReport.ShowReportBody = true;
            ReportViewer3.ShowReportBody = true;

            EmployeeReportByDate.Visible = false;
            EmployeeReport.LocalReport.Refresh();
            EmployeeReport.DataBind();
            ReportViewer3.LocalReport.Refresh();
            ReportViewer3.DataBind();

        }

        protected void Button2_Click(Object sender, EventArgs e)
        {
            EmployeeReportByDate.ShowReportBody = true;
            EmployeeReportByDate.LocalReport.Refresh();
            EmployeeReportByDate.DataBind();
                  
           
        }

       

        protected void CalenderImg_Click(Object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
            
        }

        protected void Calendar1_SelectionChanged(Object sender, EventArgs e)
        {
            HireDTextBox.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
         }

        protected void ImageButton1_Click(Object sender, ImageClickEventArgs e)
        {
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }
        }

        protected void Calendar2_SelectionChanged(Object sender,EventArgs e)
        {
            TextBox1.Text = Calendar2.SelectedDate.ToShortDateString();
            Calendar2.Visible = false;
        }

       

        
        protected void Salebyemployee_DataBinding(Object sender, EventArgs e)
        {
            //DropDownList1.Items.Clear();
            //DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem { Text = "All", Value = "0", Selected = true });
            EmployeeReport.DataBind();
        }
    }
}