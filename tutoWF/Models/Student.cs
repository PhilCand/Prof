using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tutoWF.Models
{
    public class Student : Person
    {
        public int Teacher_id { get; set; }
    }
}