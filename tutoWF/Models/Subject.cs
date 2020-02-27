using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tutoWF.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }

        public Subject(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}