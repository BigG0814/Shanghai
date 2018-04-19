using Microsoft.AspNet.Identity;
using Shanghai.Data.Entities;
using Shanghai.Security.BLL;
using Shanghai.Security.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.BackEnd_Page
{
    public partial class Add_Employee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx", true);

            }

            if (!IsPostBack)
            {
                Calendar1.Visible = false;
                Calendar2.Visible = false;
            }
        }
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                Employee employeeItem = new Employee();
                employeeItem.FName = FNameTextBox.Text;
                employeeItem.LName = LNameTextBox.Text;

                employeeItem.UserName = UserNameTextBox.Text;
                employeeItem.Street = AddressTextBox.Text;
                employeeItem.Province = ProvinceTextBox.Text;
                employeeItem.City = CityTextBox.Text;
                employeeItem.Country = CountryTextBox.Text;
                employeeItem.PostalCode = POTextBox.Text.ToUpper();
                employeeItem.Email = EmailTextBox.Text;
                employeeItem.CellPhone = CellPhoneTextBox1.Text + CellPhoneTextBox2.Text + CellPhoneTextBox3.Text;
                DateTime HireDate = DateTime.Parse(HireDTextBox.Text);
                employeeItem.HireDate = HireDate;
                employeeItem.ContactName = ContactNameTextBox.Text;
                employeeItem.ContactRelation = CRelationTextBox.Text;
                employeeItem.ContactPhone = CNumber1.Text + CNumber2.Text + CNumber3.Text;
                employeeItem.ActiveYN = true;
                EmployeeController sys = new EmployeeController();
                int employeeID = sys.Add_new_Employee(employeeItem);

                // KW- this code also adds the user to the AspNetUsers table
                var manager = new UserManager();
                var user = new UserProfile();
                user.UserName = UserNameTextBox.Text;
                user.Email = EmailTextBox.Text;
                user.EmployeeId = employeeID;
                string role = RoleDDL.SelectedItem.Text;
                manager.AddUser(user, role);

                // if all the above code executes successfully - this will run and clear fields
                FNameTextBox.Text = "";
                LNameTextBox.Text = "";
                UserNameTextBox.Text = "";
                AddressTextBox.Text = "";
                ProvinceTextBox.Text = "";
                CityTextBox.Text = "";
                CountryTextBox.Text = "";
                POTextBox.Text = "";
                EmailTextBox.Text = "";
                CellPhoneTextBox1.Text = "";
                CellPhoneTextBox2.Text = "";
                CellPhoneTextBox3.Text = "";
                HireDTextBox.Text = "";
                ContactNameTextBox.Text = "";
                CRelationTextBox.Text = "";
                CNumber1.Text = "";
                CNumber2.Text = "";
                CNumber3.Text = "";

                DropDownList1.DataBind();
            }, "Success", "Successfully added Employee information");
        }


        protected void clear_Click(object sender, EventArgs e)
        {
            FNameTextBox.Text = "";
            LNameTextBox.Text = "";
            UserNameTextBox.Text = "";
            AddressTextBox.Text = "";
            ProvinceTextBox.Text = "";
            CityTextBox.Text = "";
            CountryTextBox.Text = "";
            POTextBox.Text = "";
            EmailTextBox.Text = "";
            CellPhoneTextBox1.Text = "";
            CellPhoneTextBox2.Text = "";
            CellPhoneTextBox3.Text = "";
            HireDTextBox.Text = "";
            ContactNameTextBox.Text = "";
            CRelationTextBox.Text = "";
            CNumber1.Text = "";
            CNumber2.Text = "";
            CNumber3.Text = "";

        }

        protected void CalenderImg_Click(object sender, ImageClickEventArgs e)
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

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            HireDTextBox.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;


        }

        protected void search(object sender, EventArgs e)
        {

            int employeeid = int.Parse(DropDownList1.SelectedValue);

            EmployeeController sysmgr = new EmployeeController();

            Employee info = sysmgr.Employee_GetByEmployeeID(employeeid);
            if (info == null)
            {

                MessageUserControl1.ShowInfo("Please select a Employee");
                EmployeeLoginCode.Visible = false;
                codePrompt.Visible = false;
            }
            else
            {
                editActiveYN.Checked = false;
                EmployeeLoginCode.Visible = true;
                codePrompt.Visible = true;
                EmployeeLoginCode.Text = info.LoginCode.ToString();
                Label1.Text = info.EmployeeID.ToString();
                editFname.Text = info.FName;
                editLname.Text = info.LName;
                editUname.Text = info.UserName;
                editAddress.Text = info.Street;
                editCity.Text = info.City;
                editProvince.Text = info.Province;
                editCountry.Text = info.Country;
                editPC.Text = info.PostalCode;
                editEmail.Text = info.Email;
                editPhone.Text = info.CellPhone;
                editHiredate.Text = info.HireDate.ToString();
                editContactname.Text = info.ContactName;
                editContactnumber.Text = info.ContactPhone;
                editRelationship.Text = info.ContactRelation;
            }


        }
        protected void CalenderImg1_click(object sender, ImageClickEventArgs e)
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

        protected void Calendar2_SelectionChanged(Object sender, EventArgs e)
        {
            editHiredate.Text = Calendar2.SelectedDate.ToShortDateString();
            Calendar2.Visible = false;
        }

        //edit


        protected void UpdateEmployee_Click(object sender, EventArgs e)
        {
            MessageUserControl1.TryRun(() =>
            {
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = int.Parse(Label1.Text);
                newEmployee.FName = editFname.Text;
                newEmployee.LName = editLname.Text;
                newEmployee.UserName = editUname.Text;
                newEmployee.Street = editAddress.Text;
                newEmployee.City = editCity.Text;
                newEmployee.Province = editProvince.Text;
                newEmployee.Country = editCountry.Text;
                newEmployee.PostalCode = editPC.Text.ToUpper();
                newEmployee.Email = editEmail.Text;
                newEmployee.CellPhone = editPhone.Text;
                newEmployee.HireDate = DateTime.Parse(editHiredate.Text);
                newEmployee.ContactName = editContactname.Text;
                newEmployee.ContactPhone = editContactnumber.Text;
                newEmployee.ContactRelation = editRelationship.Text;
                if (editActiveYN.Checked == true)
                {
                    newEmployee.ActiveYN = false;
                }
                else
                {
                    newEmployee.ActiveYN = true;
                }

                var manager = new UserManager();
                var user = new UserProfile();
                user.UserName = newEmployee.UserName;
                user.Email = newEmployee.Email;
                user.EmployeeId = newEmployee.EmployeeID;
                if (DropDownList2.SelectedIndex == 0)
                    throw new Exception("You must select a role");
                string role = DropDownList2.SelectedItem.Text;
                manager.UpdateUser(user, role);

                EmployeeController sysmgr = new EmployeeController();
                sysmgr.Employee_Update(newEmployee);


                // if all the above code executes successfully - this will run and clear fields
                Label1.Text = "";
                editFname.Text = "";
                editLname.Text = "";
                editUname.Text = "";
                editAddress.Text = "";
                editCity.Text = "";
                editProvince.Text = "";
                editCountry.Text = "";
                editPC.Text = "";
                editEmail.Text = "";
                editPhone.Text = "";
                editHiredate.Text = "";
                editContactname.Text = "";
                editContactnumber.Text = "";
                editRelationship.Text = "";
                editActiveYN.Checked = false;
                DropDownList2.SelectedIndex = 0;

            }, "Success", "Sucessfully Editing Employee information");
            DropDownList1.SelectedIndex = 0;
        }

    }


}
