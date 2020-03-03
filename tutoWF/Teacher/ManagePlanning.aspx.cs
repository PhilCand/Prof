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
    }
}