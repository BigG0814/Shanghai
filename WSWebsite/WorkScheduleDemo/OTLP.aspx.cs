using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkSchedule.Data.Entities;
using WorkSchedule.Data.POCOs;
using WorkScheduleSystem.BLL;

public partial class WorkScheduleDemo_OTLP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void register_Click(object sender, EventArgs e)
    {
        Employees employeeItem = new Employees();
        employeeItem.FirstName = FirstNameBox.Text;
        employeeItem.LastName = LastNameBox.Text;
        employeeItem.HomePhone = HomePhoneBox.Text;
        //EmployeeController sysmgr = new EmployeeController();
        //int newEmployeeid = sysmgr.Employee_Add(item);
        //Message.Text = "Add successful.";

        List<SkillSet> Skill = new List<SkillSet>();

        foreach(ListViewItem item in EmployeeSkillRegistrationListView.Items)
        {
            CheckBox SkillCheckBox = item.FindControl("SkillCheckBox1") as CheckBox;
            Label SkillIDLabel1 = item.FindControl("SkillIdLabel") as Label;
            RadioButtonList LevelRadioButtonList1 = item.FindControl("LevelRadioButtonList1") as RadioButtonList;
            TextBox YearTextBox1 = item.FindControl("YearTextBox1") as TextBox;
            TextBox WageTextBox1 = item.FindControl("WageTextBox1") as TextBox;

            //if (SkillCheckBox.Checked == false && (LevelRadioButtonList1.SelectedIndex != -1 || YearTextBox1.Text != "" || WageTextBox1.Text != ""))
            //{
            //    string msg = "please select the skill, level, and0 enter the YOE, wage.";
            //    Infomsgs.Add(msg);
            //    LoadMessageDisplay(Infomsgs, "alert alert-danger");
            //    return;
            //}
            //else if (SkillCheckBox.Checked && (LevelRadioButtonList1.SelectedIndex.Equals(-1) || YearTextBox1.Text == "" || WageTextBox1.Text == ""))
            //{
            //    string msg = "please select the skill, level, and0 enter the YOE, wage.";
            //    Infomsgs.Add(msg);
            //    LoadMessageDisplay(Infomsgs, "alert alert-danger");
            //    return;
            //}
            if (SkillCheckBox.Checked && LevelRadioButtonList1.SelectedIndex != -1 && YearTextBox1.Text != "" || WageTextBox1.Text != "")
            {
                int skillid = int.Parse((item.FindControl("SkillIdLabel") as Label).Text);
                int level = int.Parse((item.FindControl("LevelRadioButtonList1") as RadioButtonList).SelectedValue);
                int yoe = int.Parse((item.FindControl("YearTextBox1") as TextBox).Text);
                decimal wage = decimal.Parse((item.FindControl("WageTextBox1") as TextBox).Text);

                SkillSet newSkillset = new SkillSet();
                newSkillset.SkillId = skillid;
                newSkillset.SkillLevel = level;
                newSkillset.YOE = yoe;
                newSkillset.HourlyWage = wage;

                Skill.Add(newSkillset);

            }
        }
        //eom
        if (Skill.Count > 0)
        {
            EmployeeSkillController sysmgr = new EmployeeSkillController();

            sysmgr.Register_Employee(employeeItem, Skill);

            EmployeeSkillRegistrationListView.DataBind();

        }
    }


    protected void cancel_Click(object sender, EventArgs e)
    {

    }
}