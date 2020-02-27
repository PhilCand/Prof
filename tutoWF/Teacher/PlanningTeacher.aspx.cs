using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Teacher
{
    public partial class PlanningTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            Models.Teacher teacher = DAL.DAL.GetTeacherbyID(id);

            lblPlanning.Text = $"Vous êtes sur le planning du prof {teacher.FirstName} {teacher.Name}";

            if (Session["Student"] != null)
            {
                Models.Student loggedStudent = (Models.Student)Session["Student"];
                if (id == loggedStudent.Teacher_id)
                {
                    lblConnected.Text = "Vous êtes connecté";
                }
                else lblConnected.Text = "Vous n'êtes pas connecté";
            }
            else lblConnected.Text = "Vous n'êtes pas connecté";

        }
    }
}