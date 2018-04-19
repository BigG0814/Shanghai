using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shanghai.System.BLL;
using System.Drawing;
using Shanghai.Security.BLL;

namespace Shanghai.WebApp.POS
{
    public partial class SeatingChart : POSPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            // check to see if table is occupied
            if (!Page.IsPostBack)
            {
                // if not logged in  - redirect to employee log in - where they type in their name to get access
                //probably not going to do that
                if (LoggedInEmployee == null)
                {
                    Response.Redirect("~/POS/POSLogin.aspx");
                }
                else
                {
                    UserManager mgr = new UserManager();
                    if (mgr.GetManagerUsers().Contains(LoggedInEmployee.EmployeeID))
                    {
                        QuitPOSBtn.Visible = true;
                    }
                    else
                    {
                        QuitPOSBtn.Visible = false;
                    }
                }
                CurrentUser.Text = LoggedInEmployee.FullName;
                BillController tableChecker = new BillController();
                List<int> TablesOccupied = tableChecker.TablesOccupied();
                string TableNumber;
                int tableNum;
                string ServerName;
               
                foreach (int item in TablesOccupied)
                {
                    ServerName = tableChecker.TableOccupiedInfo(item);


                    if (item > 21)
                    {
                        tableNum = item - 21;
                        TableNumber = ("C" + (item - 21).ToString());
                    }
                    else if (item < 22 && item > 10)
                    {
                        tableNum = item - 10;
                        TableNumber = ("B" + (item - 10).ToString());
                    }
                    else
                    {
                        tableNum = item;
                        TableNumber = ("A" + item.ToString());
                    }
                    tableOccupied.Text = tableOccupied.Text + TableNumber;

                    #region massive switch statement
                    switch (TableNumber)
                    {
                        case "A1":
                            A1.BackColor = Color.AliceBlue;

                            ServerOfTableA1.Text = ServerName;
                            break;
                        case "A2":
                            A2.BackColor = Color.AliceBlue;
                            ServerOfTableA2.Text = ServerName;
                            break;
                        case "A3":
                            A3.BackColor = Color.AliceBlue;
                            ServerOfTableA3.Text = ServerName;
                            break;
                        case "A4":
                            A4.BackColor = Color.AliceBlue;
                            ServerOfTableA4.Text = ServerName;
                            break;
                        case "A5":
                            A5.BackColor = Color.AliceBlue;
                            ServerOfTableA5.Text = ServerName;
                            break;
                        case "A6":
                            A6.BackColor = Color.AliceBlue;
                            ServerOfTableA6.Text = ServerName;
                            break;
                        case "A7":
                            A7.BackColor = Color.AliceBlue;
                            ServerOfTableA7.Text = ServerName;
                            break;
                        case "A8":
                            A8.BackColor = Color.AliceBlue;
                            ServerOfTableA8.Text = ServerName;
                            break;
                        case "A9":
                            A9.BackColor = Color.AliceBlue;
                            ServerOfTableA9.Text = ServerName;
                            break;
                        case "A10":
                            A10.BackColor = Color.AliceBlue;
                            ServerOfTableA10.Text = ServerName;
                            break;
                        case "B1":
                            B1.BackColor = Color.AliceBlue;
                            ServerOfTableB1.Text = ServerName;
                            break;
                        case "B2":
                            B2.BackColor = Color.AliceBlue;
                            ServerOfTableB2.Text = ServerName;
                            break;
                        case "B3":
                            B3.BackColor = Color.AliceBlue;
                            ServerOfTableB3.Text = ServerName;
                            break;
                        case "B4":
                            B4.BackColor = Color.AliceBlue;
                            ServerOfTableB4.Text = ServerName;
                            break;
                        case "B5":
                            B5.BackColor = Color.AliceBlue;
                            ServerOfTableB5.Text = ServerName;
                            break;
                        case "B6":
                            B6.BackColor = Color.AliceBlue;
                            ServerOfTableB6.Text = ServerName;
                            break;
                        case "B7":
                            B7.BackColor = Color.AliceBlue;
                            ServerOfTableB7.Text = ServerName;
                            break;
                        case "B8":
                            B8.BackColor = Color.AliceBlue;
                            ServerOfTableB8.Text = ServerName;
                            break;
                        case "B9":
                            B9.BackColor = Color.AliceBlue;
                            ServerOfTableB9.Text = ServerName;
                            break;
                        case "B10":
                            B10.BackColor = Color.AliceBlue;
                            ServerOfTableB10.Text = ServerName;
                            break;
                        case "B11":
                            B11.BackColor = Color.AliceBlue;
                            ServerOfTableB11.Text = ServerName;
                            break;
                        case "C1":
                            C1.BackColor = Color.AliceBlue;
                            ServerOfTableC1.Text = ServerName;
                            break;
                        case "C2":
                            C2.BackColor = Color.AliceBlue;
                            ServerOfTableC2.Text = ServerName;
                            break;
                        case "C3":
                            C3.BackColor = Color.AliceBlue;
                            ServerOfTableC3.Text = ServerName;
                            break;
                        case "C4":
                            C4.BackColor = Color.AliceBlue;
                            ServerOfTableC4.Text = ServerName;
                            break;
                        case "C5":
                            C5.BackColor = Color.AliceBlue;
                            ServerOfTableC5.Text = ServerName;
                            break;
                        case "C6":
                            C6.BackColor = Color.AliceBlue;
                            ServerOfTableC6.Text = ServerName;
                            break;
                        case "C7":
                            C7.BackColor = Color.AliceBlue;
                            ServerOfTableC7.Text = ServerName;
                            break;
                        case "C8":
                            C8.BackColor = Color.AliceBlue;
                            ServerOfTableC8.Text = ServerName;
                            break;
                        case "C9":
                            C9.BackColor = Color.AliceBlue;
                            ServerOfTableC9.Text = ServerName;
                            break;
                        case "C10":
                            C10.BackColor = Color.AliceBlue;
                            ServerOfTableC10.Text = ServerName;
                            break;
                        case "C11":
                            C11.BackColor = Color.AliceBlue;
                            ServerOfTableC11.Text = ServerName;
                            break;

                    }
                    #endregion

                }


            }
        }
        protected void SelectTakeoutDropDown (object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int tableNumber = int.Parse(TakeoutDDL.SelectedValue);
            SelectedTableID = tableNumber;

            Response.Redirect("TableBill.aspx");

        }
        protected void SelectDeliveryDropDown(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int tableNumber = int.Parse(DeliveryDDL.SelectedValue);
            SelectedTableID = tableNumber;

            Response.Redirect("TableBill.aspx");

        }
        protected void switchuser(object sender, EventArgs e)
        {
            Logout();
            Response.Redirect("POSLogIn.aspx");
        }
        protected void logout(object sender, EventArgs e)
        {
            
            Response.Redirect("POSLogIn.aspx");
        }


        protected void goToBill(object sender, EventArgs e)
        {
            BillController tableChecker = new BillController();
            List<int> TablesOccupied = tableChecker.TablesOccupied();

            if (TableBillLink.Text != null)
            {

                int stringlength = (TableBillLink.Text).Length;
                string letter = (TableBillLink.Text).Substring(0, 1);
                int TableNumber;
                int.TryParse(TableBillLink.Text.Substring(1, stringlength - 1), out TableNumber);
                switch (letter)
                {
                    case "B":
                        TableNumber = TableNumber + 10;
                        break;
                    case "C":
                        TableNumber = TableNumber + 21;
                        break;
                    default:
                        break;
                }
                if (!(TablesOccupied.Contains(TableNumber)))
                {
                    // check to see if a employee has been selected
                    // if it has then set table employee
                    int empId = int.Parse(EmployeeDDL.SelectedValue);
                    if (empId == 0)
                    {
                        //return message user needs to select a employee
                    }
                    else
                    {
                    }
                }

                Response.Redirect("TableBill.aspx?tableID=" + TableNumber);
            }

        }

        protected void SelectTable(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TableBillLink.Text = button.ID;
            if (TableBillLink.Text != null)
            {

                int stringlength = (TableBillLink.Text).Length;
                string letter = (TableBillLink.Text).Substring(0, 1);
                int TableNumber;

                int.TryParse(TableBillLink.Text.Substring(1, stringlength - 1), out TableNumber);
                switch (letter)
                {
                    case "B":
                        TableNumber = TableNumber + 10;
                        break;
                    case "C":
                        TableNumber = TableNumber + 21;
                        break;
                    default:
                        break;
                }
                SelectedTableID = TableNumber;

                Response.Redirect("TableBill.aspx");
            }
        }

        protected void QuitPOSBtn_Click(object sender, EventArgs e)
        {
            var AuthenticationManger = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManger.SignOut();
            Response.Redirect("~/Account/Login.aspx");
        }
    }
}