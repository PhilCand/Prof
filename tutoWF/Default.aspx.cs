using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnGoToTeacher_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(tbTeacherId.Text);

            Response.Redirect($"/Teacher/HomeTeacher?id={id}");
        }

    }
}