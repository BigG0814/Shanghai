using Shanghai.Data.Entities;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.BackEnd_Page
{
    public partial class Edit_Job_Types : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx", true);

            }

        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            int jobId = int.Parse(JobTypeDDL.SelectedValue);

            var shiftController = new ShiftController();
            JobType job = shiftController.Get_JobTypeById(jobId);

            JobNameTextBox.Text = job.Description;
            if (job.isActive == true)
                ActiveYNCB.Checked = true;
            else
                ActiveYNCB.Checked = false;
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            MessageUserControl1.TryRun(() =>
            {
                var shiftController = new ShiftController();
                JobType newJob = new JobType();
                newJob.Description = NewJobNameTextBox.Text;
                newJob.isActive = true;

                shiftController.AddJobType(newJob);

                NewJobNameTextBox.Text = "";

                JobTypeDDL.DataBind();
            }, "Success", "Successfully added a new job type");
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            NewJobNameTextBox.Text = "";
        }

        protected void SubmitChanges_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                var changedJob = new JobType();
                changedJob.Description = JobNameTextBox.Text;
                changedJob.isActive = ActiveYNCB.Checked;
                changedJob.JobTypeID = int.Parse(JobTypeDDL.SelectedValue);

                var shiftController = new ShiftController();
                shiftController.EditJobType(changedJob);

                JobTypeDDL.DataBind();

                JobNameTextBox.Text = "";
                ActiveYNCB.Checked = false;
            }, "Success", "Successfully updated Job Type information");
        }
    }
}