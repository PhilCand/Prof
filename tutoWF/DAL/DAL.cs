using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using tutoWF.Models;

namespace tutoWF.DAL
{
    public class DAL
    {
        public static string _connectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();

        public static List<string> GetEmailFromStudent(int teacherId)
        {
            List<string> emails = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                String query = $"SELECT Email FROM Student WHERE Teacher_id = {teacherId}";
                SqlCommand myCommand = new SqlCommand(query, connection);

                SqlDataReader rdr = myCommand.ExecuteReader();

                while (rdr.Read())
                {
                    string emaildb = rdr["Email"].ToString();
                    emails.Add(emaildb);
                }
            }

            return emails;
        }

        internal static Models.Teacher GetTeacherbyID(int id)
        {
            Models.Teacher teacher = new Models.Teacher();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                String query = $"SELECT * FROM Teacher WHERE Id={id}";
                SqlCommand myCommand = new SqlCommand(query, connection);

                SqlDataReader rdr = myCommand.ExecuteReader();

                while (rdr.Read())
                {
                    teacher.Name = rdr["Name"].ToString();
                    teacher.Id = (int)rdr["Id"];
                    teacher.Phone = rdr["Phone"].ToString();
                    teacher.Picture = rdr["Picture"].ToString();
                    teacher.Password = rdr["Password"].ToString();
                    teacher.Adress = rdr["Adress"].ToString();
                    teacher.Description = rdr["Description"].ToString();
                    teacher.Email = rdr["Email"].ToString();
                    teacher.Gender = (bool)rdr["Gender"];
                    teacher.FirstName = rdr["FirstName"].ToString();

                }
            }

            return teacher;
        }

        internal static Models.Student GetStudentbyID(int studentId)
        {
            Models.Student student = new Models.Student();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                String query = $"SELECT * FROM Student WHERE Id={studentId}";
                SqlCommand myCommand = new SqlCommand(query, connection);

                SqlDataReader rdr = myCommand.ExecuteReader();

                while (rdr.Read())
                {
                    student.Name = rdr["Name"].ToString();
                    student.Id = (int)rdr["Id"];
                    student.Phone = rdr["Phone"].ToString();
                    student.Picture = rdr["Picture"].ToString();
                    student.Password = rdr["Password"].ToString();
                    student.Adress = rdr["Adress"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Gender = (bool)rdr["Gender"];
                    student.FirstName = rdr["FirstName"].ToString();
                    student.Teacher_id = (int)rdr["Teacher_id"];

                }
            }

            return student;
        }

        internal static int LoginTeacher(string email, string password)
        {
            String query = "";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                query = $"SELECT Id FROM Teacher WHERE Email='{email}' AND Password='{password}'";

                SqlCommand myCommand = new SqlCommand(query, connection);

                object result = myCommand.ExecuteScalar();

                if (result != null)
                {
                    int id = int.Parse(result.ToString());
                    return id;
                }
                else return -1;
            }
        }

        internal static int LoginStudent(string email, string password, int id)
        {
            String query = "";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                query = $"SELECT Id FROM Student WHERE Email='{email}' AND Password='{password}' AND Teacher_id ={id}";

                SqlCommand myCommand = new SqlCommand(query, connection);

                object result = myCommand.ExecuteScalar();

                if (result != null)
                {
                    int studentId = int.Parse(result.ToString());
                    return studentId;
                }
                else return -1;
            }
        }

        public static List<string> GetEmailFromTeacher()
        {
            List<string> emails = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                String query = $"SELECT Email FROM Teacher";
                SqlCommand myCommand = new SqlCommand(query, connection);

                SqlDataReader rdr = myCommand.ExecuteReader();

                while (rdr.Read())
                {
                    string emaildb = rdr["Email"].ToString();
                    emails.Add(emaildb);
                }
            }

            return emails;
        }

        public static bool CreateStudent(Models.Student newStudent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "INSERT INTO Student (Name, FirstName, Email, Password, Phone, Gender, Adress, Teacher_id) VALUES (@name,@firstname,@email, @password, @phone,@gender,@adress, @teacher_id)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", newStudent.Name);
                    command.Parameters.AddWithValue("@firstname", newStudent.FirstName);
                    command.Parameters.AddWithValue("@email", newStudent.Email);
                    command.Parameters.AddWithValue("@password", newStudent.Password);
                    command.Parameters.AddWithValue("@phone", newStudent.Phone);
                    command.Parameters.AddWithValue("@gender", newStudent.Gender);
                    command.Parameters.AddWithValue("@adress", newStudent.Adress);
                    command.Parameters.AddWithValue("@teacher_id", newStudent.Teacher_id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0) return false;

                    else return true;

                }
            }

        }

        public static bool CreateTeacher(Models.Teacher newTeacher, ListBox lbSubject)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "INSERT INTO Teacher (Name, FirstName, Email, Password, Phone, Gender, Adress, Description) VALUES (@name,@firstname,@email, @password, @phone,@gender,@adress, @description); select max(id) from Teacher";
                int newTeacherId;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", newTeacher.Name);
                    command.Parameters.AddWithValue("@firstname", newTeacher.FirstName);
                    command.Parameters.AddWithValue("@email", newTeacher.Email);
                    command.Parameters.AddWithValue("@password", newTeacher.Password);
                    command.Parameters.AddWithValue("@phone", newTeacher.Phone);
                    command.Parameters.AddWithValue("@gender", newTeacher.Gender);
                    command.Parameters.AddWithValue("@adress", newTeacher.Adress);
                    command.Parameters.AddWithValue("@description", newTeacher.Description);

                    connection.Open();

                    newTeacherId = (int)command.ExecuteScalar();
                }

                String query2 = "";

                foreach (ListItem li in lbSubject.Items)
                {
                    if (li.Selected)
                    {
                        query2 += $"INSERT INTO Teacher_Subject (Teacher_id, Subject_id) VALUES ({newTeacherId},{li.Value});";
                    }
                }

                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0 && newTeacherId > 0) return true;
                    else return false;
                }


            }
        }

        public static void LoadSubjects(ListBox lb)
        {

            DataTable subjects = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Name, Id FROM Subject", con);
                    adapter.Fill(subjects);

                    lb.DataSource = subjects;
                    lb.DataTextField = "Name";
                    lb.DataValueField = "Id";
                    lb.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }
            }

        }
    }
}
