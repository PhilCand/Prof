using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tutoWF.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string Adress { get; set; }
        public string Picture { get; set; }
    }
}