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

            lblPlanning.Text = $"Planning du professeur {teacher.FirstName} {teacher.Name}";

            if (Session["Student"] != null)
            {
                Models.Student loggedStudent = (Models.Student)Session["Student"];
                if (id == loggedStudent.Teacher_id)
                {
                    lblConnected.Text = "Vous êtes connecté";
                    hdnStudentId.Value = loggedStudent.Id.ToString();
                }
                else lblConnected.Text = $"Vous n'êtes pas élève de ce professeur, <a href='/Student/Newstudent?id={id}'>créer un compte<a>";
            }
            else lblConnected.Text = $"Vous êtes visiteur, <a href='/Teacher/HomeTeacher?id={id}'> connectez-vous </a> ou <a href='/Student/Newstudent?id={id}'>créez un compte<a> pour réserver";

            if(!IsPostBack)ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:SetCalendarDate(); ", true);

        }

        protected void btn_bookEvent_Click(object sender, EventArgs e)
        {
            int eventId = Int32.Parse(hfeventIdModal.Value);
            int studentId = Int32.Parse(hdnStudentId.Value);
            DAL.DAL.BookEvent(eventId, studentId);
            ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Réservation effectuée');", true);

        }

        protected void btn_freeEvent_Click(object sender, EventArgs e)
        {
            DAL.DAL.FreeEvent(hfeventIdModal.Value);
            ClientScript.RegisterStartupScript(GetType(), "alert", "showSuccessAlert('Réservation annulée');", true);

        }
    }
}