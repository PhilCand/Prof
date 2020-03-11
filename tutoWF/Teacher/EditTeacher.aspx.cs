using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tutoWF.Models;

namespace tutoWF.Teacher
{
    public partial class EditTeacher : System.Web.UI.Page
    {
        public Models.Teacher editedTeacher { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int editedTeacherId = ((Models.Teacher)Session["Teacher"]).Id;
            editedTeacher = DAL.DAL.GetTeacherbyID(editedTeacherId);

            lblMessage.Text = "";

            if (!IsPostBack)
            {
                DAL.DAL.LoadSubjects(lbSubject);
                txtName.Text = editedTeacher.Name;
                txtFirstName.Text = editedTeacher.FirstName;
                txtEmail.Text = editedTeacher.Email;
                txtPhone.Text = editedTeacher.Phone;
                txtAdress.Text = editedTeacher.Adress;
                txtDescription.Text = editedTeacher.Description;
                ddlGender.SelectedValue = editedTeacher.Gender ? "1" : "0";

                List<ListItem> selected = new List<ListItem>();

                foreach (Subject subject in editedTeacher.Subjects)
                {
                    foreach(ListItem li in lbSubject.Items)
                    {
                        if (li.Text.Contains(subject.Name)) li.Selected = true;
                    }
                }

            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Models.Teacher newTeacher = new Models.Teacher();

                var sanitizer = new HtmlSanitizer();

                newTeacher.Id = editedTeacher.Id;
                newTeacher.Name = sanitizer.Sanitize(txtName.Text);
                newTeacher.FirstName = sanitizer.Sanitize(txtFirstName.Text);
                newTeacher.Email = sanitizer.Sanitize(txtEmail.Text);
                newTeacher.Phone = sanitizer.Sanitize(txtPhone.Text);
                newTeacher.Gender = Convert.ToBoolean(Convert.ToInt32(ddlGender.SelectedValue));
                newTeacher.Adress = sanitizer.Sanitize(txtAdress.Text);
                newTeacher.Description = sanitizer.Sanitize(txtDescription.Text);

                if (txtEmail.Text != editedTeacher.Email)
                {
                    List<string> emails = DAL.DAL.GetEmailFromTeacher();

                    foreach (string emaildb in emails)
                    {
                        if (newTeacher.Email == emaildb)
                        {
                            this.lblMessage.Text = "Cet email existe déja";
                            return;
                        }
                    }
                }

                if (DAL.DAL.UpdateTeacher(newTeacher, lbSubject))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Modifications enregistrées');", true);
                }

                else lblMessage.Text = "Une erreur est survenue, réessayez";
            }
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

            if (DAL.DAL.UpdatePassword("Teacher", editedTeacher.Id, password))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Mot de passe modifié');", true);
            }
        }
    }
}