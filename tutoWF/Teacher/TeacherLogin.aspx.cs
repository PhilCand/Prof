using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Teacher
{
    public partial class TeacherLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_connecter_Click(object sender, EventArgs e)
        {
            Session.Clear();

            var sanitizer = new HtmlSanitizer();

            string email = sanitizer.Sanitize(txt_mail.Text);
            string password = sanitizer.Sanitize(txt_mdp.Text);

            int id = DAL.DAL.LoginTeacher(email, password);

            if (id<=0)
            {
                lblLoginError.Text = "Identifiant ou mot de passe incorrecte";
                return;
            }

            Session["Teacher"] = DAL.DAL.GetTeacherbyID(id);

            Response.Redirect($"/Teacher/ManageTeacher");
        }
    }
}