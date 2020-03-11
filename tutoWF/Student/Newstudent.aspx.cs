using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF
{
    public partial class Newstudent : System.Web.UI.Page
    {

        public int LoggedTeacherId { get; set; }
        public int UrlTeacherId { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            UrlTeacherId = Convert.ToInt32(Request.QueryString["id"]);


            if (!IsPostBack)
            {
                if (Session["Teacher"] != null)
                {
                    Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
                    LoggedTeacherId = loggedTeacher.Id;
                }
                Models.Teacher teacher = DAL.DAL.GetTeacherbyID(UrlTeacherId);
                lblCreateStudent.Text = $"Professeur : {teacher.FirstName} {teacher.Name}";
                ViewState["Referrer"] = Request.UrlReferrer.ToString();
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                var sanitizer = new HtmlSanitizer();
                Models.Student newStudent = BuildStudent();
                string confirm = sanitizer.Sanitize(txtPasswordConfirm.Text);
                

                if (newStudent.Password != confirm)
                {
                    this.lblMessage.Text = "Les mots de passe ne correspondent pas";
                    return;
                }

                List<string> emails = DAL.DAL.GetEmailFromStudent(UrlTeacherId);

                foreach (string emaildb in emails)
                {
                    if (newStudent.Email == emaildb)
                    {
                        this.lblMessage.Text = "Cet email existe déja";
                        return;
                    }
                }

                if (DAL.DAL.CreateStudent(newStudent))
                {
                    if (Session["Teacher"] != null) Response.Redirect(ViewState["Referrer"].ToString());
                    else Response.Redirect($"/Success?id={UrlTeacherId}");
                }

                else this.lblMessage.Text = "Une erreur est survenue, réessayez";
            }

            else return;
        }
        private Models.Student BuildStudent()
        {
            Models.Student newStudent = new Models.Student();

            var sanitizer = new HtmlSanitizer();

            newStudent.Name = sanitizer.Sanitize(txtName.Text);
            newStudent.FirstName = sanitizer.Sanitize(txtFirstName.Text);
            newStudent.Email = sanitizer.Sanitize(txtEmail.Text);
            newStudent.Password = sanitizer.Sanitize(txtPassword.Text);
            newStudent.Teacher_id = Convert.ToInt32(Request.QueryString["id"]);
            newStudent.Phone = sanitizer.Sanitize(txtPhone.Text);
            newStudent.Gender = Convert.ToBoolean(Convert.ToInt32(ddlGender.SelectedValue));
            newStudent.Adress = sanitizer.Sanitize(txtAdress.Text);

            return newStudent;
        }
    }
       
}

