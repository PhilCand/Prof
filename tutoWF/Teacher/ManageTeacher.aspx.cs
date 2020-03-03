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
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];

            lblManageTeacher.Text = $"Interface de gestion du prof {loggedTeacher.FirstName} {loggedTeacher.Name}";

            hlPlanning.Attributes.Add("href", "/Teacher/ManagePlanning");

            lblNumber.Text = $"{loggedTeacher.Id}";
            lblNumberDesc.Text = $"Numéro à fournir à vos élèves pour acceder à votre planning";                   


        }
    }
}