using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Student
{
    public partial class EditStudent : System.Web.UI.Page
    {
        public bool legit { get; set; }
        public Models.Student EditedStudent { get; set; }
        public Models.Teacher LoggedTeacher { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            LoggedTeacher = (Models.Teacher)Session["Teacher"];
            if (Session["Teacher"] != null)
            {
                int teacherId = LoggedTeacher.Id;
                int studentId = Convert.ToInt32(Request.QueryString["id"]);
                legit = DAL.DAL.IsStudentOfTeacher(studentId, teacherId);
                EditedStudent = DAL.DAL.GetStudentbyID(studentId);
            }
            if (Session["Student"] != null)
            {
                int studentId = ((Models.Student)Session["Student"]).Id;
                EditedStudent = DAL.DAL.GetStudentbyID(studentId);
                legit = true;
            }           
            

            if (!IsPostBack)
            {                
                txtName.Text = EditedStudent.Name;
                txtFirstName.Text = EditedStudent.FirstName;
                txtEmail.Text = EditedStudent.Email;
                txtAdress.Text = EditedStudent.Adress;
                txtPhone.Text = EditedStudent.Phone;
                ddlGender.SelectedValue = EditedStudent.Gender ? "1" : "0";
            }
        }


        private Models.Student BuildStudent()
        {
            Models.Student newStudent = new Models.Student();

            var sanitizer = new HtmlSanitizer();

            newStudent.Id = EditedStudent.Id;
            newStudent.Name = sanitizer.Sanitize(txtName.Text);
            newStudent.FirstName = sanitizer.Sanitize(txtFirstName.Text);
            newStudent.Email = sanitizer.Sanitize(txtEmail.Text);
            newStudent.Teacher_id = Convert.ToInt32(Request.QueryString["id"]);
            newStudent.Phone = sanitizer.Sanitize(txtPhone.Text);
            newStudent.Gender = Convert.ToBoolean(Convert.ToInt32(ddlGender.SelectedValue));
            newStudent.Adress = sanitizer.Sanitize(txtAdress.Text);

            return newStudent;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var sanitizer = new HtmlSanitizer();
                Models.Student newStudent = BuildStudent();
                //string confirm = sanitizer.Sanitize(txtPasswordConfirm.Text);
                //int teacherId = Convert.ToInt32(Request.QueryString["id"]);
                int teacherId = EditedStudent.Teacher_id;


                if (txtEmail.Text != EditedStudent.Email)
                {
                    List<string> emails = DAL.DAL.GetEmailFromStudent(teacherId);

                    foreach (string emaildb in emails)
                    {
                        if (newStudent.Email == emaildb)
                        {
                            this.lblMessage.Text = "Cet email existe déja";
                            return;
                        }
                    }
                }

                if (DAL.DAL.UpdateStudent(newStudent))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Modifications enregistrées');", true);
                }

                else this.lblMessage.Text = "Une erreur est survenue, réessayez";
            }

            else return;
        }

        protected void submitmdp_Click(object sender, EventArgs e)
        {
            var sanitizer = new HtmlSanitizer();
            string password = sanitizer.Sanitize(txtPassword.Text);
            string confirm = sanitizer.Sanitize(txtPasswordConfirm.Text);

            if (password != confirm)
            {
                lblMessage.Text = "Les mots de passe ne correspondent pas";
                return;
            }

            if (DAL.DAL.UpdatePassword("Student", EditedStudent.Id, password))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Mot de passe modifié');", true);
            }

        }
    }
}
