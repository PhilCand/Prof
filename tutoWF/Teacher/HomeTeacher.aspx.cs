using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Teacher
{
    public partial class HomeTeacher : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            if (id <= 0) Response.Redirect("/Default");

            Models.Teacher teachersPage = DAL.DAL.GetTeacherbyID(id);

            lblTitle.Text = $"Portail de connection du professeur {teachersPage.FirstName} {teachersPage.Name}";

            if (Session["Teacher"] != null)
            {
                Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
                if (id == loggedTeacher.Id)
                {
                    lblNumber.Text = $"{teachersPage.Id}";
                    lblNumberDesc.Text = $"Numéro à fournir à vos élèves pour acceder à votre planning";
                }
            }

            else if (Session["Student"] != null)
            {
                Models.Student loggedStudent = (Models.Student)Session["Student"];
                if (id == loggedStudent.Teacher_id)
                {
                    Response.Redirect($"/Teacher/PlanningTeacher?id={id}");
                }
            }

        }

        protected void btn_connectStudent_Click(object sender, EventArgs e)
        {
            Session.Clear();

            int id = Convert.ToInt32(Request.QueryString["id"]);

            var sanitizer = new HtmlSanitizer();

            string email = sanitizer.Sanitize(txt_mail.Text);
            string password = sanitizer.Sanitize(txt_mdp.Text);

            int studentId = DAL.DAL.LoginStudent(email, password, id);

            if (studentId <= 0)
            {
                lblLoginError.Text = "Identifiant ou mot de passe incorrecte";
                return;
            }

            Session["Student"] = DAL.DAL.GetStudentbyID(studentId);

            Response.Redirect($"/Teacher/PlanningTeacher?id={id}");

        }
    }
}