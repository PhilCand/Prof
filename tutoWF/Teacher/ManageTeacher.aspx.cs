using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Teacher
{
    public partial class ManageTeacher : System.Web.UI.Page
    {
        public string teacherId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];

            teacherId = loggedTeacher.Id.ToString();

            lblManageTeacher.Text = $"Interface de gestion du prof {loggedTeacher.FirstName} {loggedTeacher.Name}";

            hlPlanning.Attributes.Add("href", "/Teacher/ManagePlanning");

            lblNumber.Text = $"{loggedTeacher.Id}";
            lblNumberDesc.Text = $"Numéro à fournir à vos élèves : ";


            if (!IsPostBack)
            {
                BindDataSourceStudent();
            }

            gvStudent.HeaderRow.TableSection = TableRowSection.TableHeader;



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
        }

        protected void gvStudent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int editedStudentId = Int32.Parse(gvStudent.DataKeys[e.NewEditIndex].Value.ToString());
            Response.Redirect("/Student/EditStudent?id=" + editedStudentId);
        }

        protected void BindDataSourceStudent()
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
            SqlDataSource SqlDataSourceStudent = new SqlDataSource();
            SqlDataSourceStudent.ID = "SqlDataSourceStudent";
            this.Page.Controls.Add(SqlDataSourceStudent);
            SqlDataSourceStudent.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataSourceStudent.SelectCommand = $"SELECT [Name], [FirstName], [Email], [Id], [Phone] FROM [Student] WHERE [Teacher_id] = {loggedTeacher.Id}";
            gvStudent.DataSource = SqlDataSourceStudent;
            gvStudent.DataBind();
        }

    }

}