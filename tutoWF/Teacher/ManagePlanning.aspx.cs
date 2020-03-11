using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tutoWF.Models;

namespace tutoWF
{
    public partial class ManagePlanning : System.Web.UI.Page
    {
        public int teacherId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
            hdnTeacherId.Value = loggedTeacher.Id.ToString();           

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            int eventId = Int32.Parse(hfeventIdModal.Value);
            string title = tbModalTitle.Text;
            string desc = tbModalDesc.Text;

            DAL.DAL.UpdateEventTitleDesc(eventId, title, desc);


        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            int eventId = Int32.Parse(hfeventIdModal.Value);
            DAL.DAL.DeleteEvent(eventId);
        }

        protected void btn_freeEvent_Click(object sender, EventArgs e)
        {
            DAL.DAL.FreeEvent(hfeventIdModal.Value);
        }
    }
}