﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tutoWF.Student
{
    public partial class ManageStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Student loggedStudent = (Models.Student)Session["Student"];

            lblManageStudent.Text = $"Interface de gestion de l'élève {loggedStudent.FirstName} {loggedStudent.Name}";

        }
    }
}