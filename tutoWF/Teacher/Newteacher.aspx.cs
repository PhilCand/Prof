using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tutoWF.Models;

namespace tutoWF
{
    public partial class Newteacher : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                DAL.DAL.LoadSubjects(lbSubject);
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                Models.Teacher newTeacher = new Models.Teacher();

                var sanitizer = new HtmlSanitizer();

                newTeacher.Name = sanitizer.Sanitize(txtName.Text);
                newTeacher.FirstName = sanitizer.Sanitize(txtFirstName.Text);
                newTeacher.Email = sanitizer.Sanitize(txtEmail.Text);
                newTeacher.Password = sanitizer.Sanitize(txtPassword.Text);
                string confirm = sanitizer.Sanitize(txtPasswordConfirm.Text);
                newTeacher.Phone = sanitizer.Sanitize(txtPhone.Text);
                newTeacher.Gender = Convert.ToBoolean(Convert.ToInt32(ddlGender.SelectedValue));
                newTeacher.Adress = sanitizer.Sanitize(txtAdress.Text);
                newTeacher.Description = sanitizer.Sanitize(txtDescription.Text);

                if (newTeacher.Password != confirm)
                {
                    this.lblMessage.Text = "Les mots de passe ne correspondent pas";
                    return;
                }

                List<string> emails = DAL.DAL.GetEmailFromTeacher();

                foreach (string emaildb in emails)
                {
                    if (newTeacher.Email == emaildb)
                    {
                        this.lblMessage.Text = "Cet email existe déja";
                        return;
                    }
                }

                if (DAL.DAL.CreateTeacher(newTeacher, lbSubject))
                {
                    Response.Redirect("/Success");
                }

                else lblMessage.Text = "Une erreur est apparue, réessayez";
            }
        }

    }
}


