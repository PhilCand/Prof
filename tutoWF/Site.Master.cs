using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tutoWF.Models;

namespace tutoWF
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["Teacher"] != null)
            {
                Models.Teacher loggedTeacher = (Models.Teacher)Session["Teacher"];
                lblNameConnected.Text = loggedTeacher.Email;
            }

            if (Session["Student"] != null)
            {
                Models.Student loggedStudent = (Models.Student)Session["Student"];
                lblNameConnected.Text = loggedStudent.Email;

            }


        }
    }
}