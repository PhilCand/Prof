using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Student
{
    public partial class ManageStudent : System.Web.UI.Page
    {

        public Models.Student LoggedStudent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggedStudent = (Models.Student)Session["Student"];
            Models.Teacher teacher = DAL.DAL.GetTeacherbyID(LoggedStudent.Teacher_id);

            lblManageStudent.Text = $"Interface de gestion de l'élève {LoggedStudent.FirstName} {LoggedStudent.Name}";
            lblTeacher.Text = $" {teacher.Name} {teacher.FirstName} {teacher.Email}";

            if (!IsPostBack)
            {
                tbDateDebut.Text = DateTime.Today.ToString("yyyy-MM-dd");
                BindDataSourceEvent();
            }
        }

        protected void btn_editStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Student/EditStudent");
        }

        protected void BindDataSourceEvent(string sortExpression = "Start_date ASC")
        {
            //Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
            SqlDataSource SqlDataSourceEvent = new SqlDataSource();
            SqlDataSourceEvent.ID = "SqlDataSourceEvent";
            this.Page.Controls.Add(SqlDataSourceEvent);
            SqlDataSourceEvent.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataSourceEvent.SelectCommand = $"SELECT e.Id, e.Title, e.Description, e.Start_date, e.End_date from Event as e LEFT JOIN Student as s ON e.Student_id = s.Id WHERE e.Student_id = {LoggedStudent.Id} ";
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataSourceEvent();
        }

        protected void tbDateDebut_TextChanged(object sender, EventArgs e)
        {
            BindDataSourceEvent();
        }

        protected void gvEvent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eventId = Int32.Parse(gvEvent.DataKeys[e.RowIndex].Value.ToString());
            DAL.DAL.FreeEvent(eventId);
            BindDataSourceEvent();
        }

        protected void gvEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DateTime parsedDate = DateTime.Today;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text != "")
                {
                    parsedDate = DateTime.Parse(e.Row.Cells[3].Text);
                }
                if (parsedDate < DateTime.Today)
                {
                    ((Button)e.Row.FindControl("btn_cancel")).Enabled = false;
                }
            }
        }

        protected void btn_planningTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Teacher/PlanningTeacher?id={LoggedStudent.Teacher_id}");
        }
    }
}