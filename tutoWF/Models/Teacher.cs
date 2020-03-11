using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tutoWF.Models
{
    public class Teacher : Person
    {
        public string Description { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Student> Students { get; set; } = new List<Student>();
        
    }
}