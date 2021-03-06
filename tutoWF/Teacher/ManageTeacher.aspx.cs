﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tutoWF.Models;

namespace tutoWF.Teacher
{
    public partial class ManageTeacher : System.Web.UI.Page
    {
        public string teacherId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
            teacherId = loggedTeacher.Id.ToString();
            hdnTeacherId.Value = teacherId;
            lblManageTeacher.Text = $"Interface de gestion du prof {loggedTeacher.FirstName} {loggedTeacher.Name}";
            hlPlanning.Attributes.Add("href", "/Teacher/ManagePlanning");
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:SetCalendarDate(); ", true);
            lblNumber.Text = $"{loggedTeacher.Id}";
            lblNumberDesc.Text = $"Numéro à fournir à vos élèves : ";

            if (!IsPostBack)
            {
                tbDateDebut.Text = DateTime.Today.ToString("yyyy-MM-dd");
                BindDataSourceStudent();
                BindDataSourceEvent();
            }

            //gvStudent.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        public void SqldsExample_Selecting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@DynamicVariable"].Value = (teacherId);
        }

        protected void gvStudent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int deleteStudentId = Int32.Parse(gvStudent.DataKeys[e.RowIndex].Value.ToString());
            DAL.DAL.DeleteStudent(deleteStudentId);
            BindDataSourceStudent();
            BindDataSourceEvent();
        }

        protected void gvStudent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int editedStudentId = Int32.Parse(gvStudent.DataKeys[e.NewEditIndex].Value.ToString());
            Response.Redirect("/Student/EditStudent?id=" + editedStudentId);
        }

        protected void btn_addStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Student/NewStudent?id=" + teacherId);
        }

        protected void BindDataSourceStudent(string sortExpression = "Id ASC")
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
            SqlDataSource SqlDataSourceStudent = new SqlDataSource();
            SqlDataSourceStudent.ID = "SqlDataSourceStudent";
            this.Page.Controls.Add(SqlDataSourceStudent);
            SqlDataSourceStudent.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataSourceStudent.SelectCommand = $"SELECT [Name], [FirstName], [Email], [Id], [Phone] FROM [Student] WHERE [Teacher_id] = {loggedTeacher.Id} ORDER BY {sortExpression}";
            gvStudent.DataSource = SqlDataSourceStudent;
            gvStudent.DataBind();
        }

        protected void BindDataSourceEvent(string sortExpression = "Start_date ASC")
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
            SqlDataSource SqlDataSourceEvent = new SqlDataSource();
            SqlDataSourceEvent.ID = "SqlDataSourceEvent";
            this.Page.Controls.Add(SqlDataSourceEvent);
            SqlDataSourceEvent.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataSourceEvent.SelectCommand = $"SELECT e.Id, e.Title, e.Start_date, e.End_date, e.State,s.Name, s.FirstName, s.Email from Event as e LEFT JOIN Student as s ON e.Student_id = s.Id WHERE e.Teacher_id = {teacherId} ";
            if (ddlState.SelectedValue != "all") SqlDataSourceEvent.SelectCommand += $"AND e.State = '{ddlState.SelectedValue}' ";
            if (tbDateDebut.Text != "") SqlDataSourceEvent.SelectCommand += $"AND e.Start_date >= '{tbDateDebut.Text}'";
            if (tbDateFin.Text != "") SqlDataSourceEvent.SelectCommand += $"AND e.End_date <= '{tbDateFin.Text} 23:59:59'";
            SqlDataSourceEvent.SelectCommand += $"ORDER BY {sortExpression}";
            gvEvent.DataSource = SqlDataSourceEvent;
            gvEvent.DataBind();
        }

        protected void gvEvent_Sorting(object sender, GridViewSortEventArgs e)
        {
            if ((string)ViewState["sortExpression"] == e.SortExpression + " " + "ASC")
                ViewState["sortExpression"] = e.SortExpression + " " + "DESC";

            else
                ViewState["sortExpression"] = e.SortExpression + " " + "ASC";

            BindDataSourceEvent((string)ViewState["sortExpression"]);
        }

        protected void gvStudent_Sorting(object sender, GridViewSortEventArgs e)
        {
            if ((string)ViewState["sortExpression"] == e.SortExpression + " " + "ASC")
                ViewState["sortExpression"] = e.SortExpression + " " + "DESC";

            else
                ViewState["sortExpression"] = e.SortExpression + " " + "ASC";

            BindDataSourceStudent((string)ViewState["sortExpression"]);
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataSourceEvent();
        }

        protected void tbDateDebut_TextChanged(object sender, EventArgs e)
        {
            BindDataSourceEvent();
        }

        protected void btn_editTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Teacher/EditTeacher");
        }

        protected void gvEvent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int EventId = Int32.Parse(gvEvent.DataKeys[e.RowIndex].Value.ToString());
            DAL.DAL.DeleteEvent(EventId);
            BindDataSourceEvent();
        }

        protected void gvEvent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int EventId = Int32.Parse(gvEvent.DataKeys[e.NewEditIndex].Value.ToString());
            DAL.DAL.FreeEvent(EventId);
            BindDataSourceEvent();
        }

        protected void btn_comment_Click(object sender, EventArgs e)
        {
            Button linkbutton = (Button)sender;  // get the link button which trigger the event
            GridViewRow row = (GridViewRow)linkbutton.NamingContainer; // get the GridViewRow that contains the linkbutton
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#CommentModal').modal()", true);//show the modal
            hf_event_id.Value =row.Cells[0].Text;
            Models.Event editedEvent = DAL.DAL.GetEventbyId(int.Parse(row.Cells[0].Text), int.Parse(teacherId));
            tbModalDesc.Text = editedEvent.description;
        }

        protected void btn_update_comment_Click(object sender, EventArgs e)
        {
            DAL.DAL.UpdateEventTitleDesc(int.Parse(hf_event_id.Value), tbModalDesc.Text);
            ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Modifications enregistrées');", true);

        }

        protected void gvEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime parsedDate = DateTime.Today;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text != "")
                {
                    parsedDate = DateTime.Parse(e.Row.Cells[1].Text);
                }
                if (parsedDate < DateTime.Today || e.Row.Cells[3].Text == "Libre")
                {
                    ((Button)e.Row.FindControl("btn_freeEvent")).Enabled = false;
                }
            }
        }
    }

}