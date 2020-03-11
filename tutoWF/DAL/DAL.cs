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

        #region STUDENT

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

        public static void DeleteStudent(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "DELETE FROM Student WHERE Id = @studentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                }
            }
        }

        public static bool UpdateStudent(Models.Student updatedStudent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "UPDATE Student SET Name = @name, FirstName = @firstname, Email = @email, Phone = @phone, Gender = @gender, Adress = @adress WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", updatedStudent.Id);
                    command.Parameters.AddWithValue("@name", updatedStudent.Name);
                    command.Parameters.AddWithValue("@firstname", updatedStudent.FirstName);
                    command.Parameters.AddWithValue("@email", updatedStudent.Email);
                    command.Parameters.AddWithValue("@phone", updatedStudent.Phone);
                    command.Parameters.AddWithValue("@gender", updatedStudent.Gender);
                    command.Parameters.AddWithValue("@adress", updatedStudent.Adress);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0) return false;

                    else return true;
                }
            }
        }

        public static bool IsStudentOfTeacher(int studentId, int teacherId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "SELECT * FROM Student WHERE Id=@studentId AND Teacher_id = @teacherId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);
                    command.Parameters.AddWithValue("@teacherId", teacherId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows) return true;
                    else return false;
                }
            }
        }

        #endregion

        #region TEACHER
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                String query2 = $"SELECT ts.Subject_id, s.Name FROM Teacher_Subject as ts JOIN Subject s ON ts.Subject_id = s.Id WHERE Teacher_id = {id};";
                SqlCommand myCommand2 = new SqlCommand(query2, connection);
                SqlDataReader rdr2 = myCommand2.ExecuteReader();
                while (rdr2.Read())
                {
                    Subject newSubject = new Subject(rdr2["Name"].ToString(), (int)rdr2["Subject_id"]);
                    teacher.Subjects.Add(newSubject);
                }
                return teacher;
            }
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

        internal static bool UpdateTeacher(Models.Teacher newTeacher, ListBox lbSubject)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "UPDATE Teacher SET Name = @name, FirstName = @firstname, Email = @email, Phone = @phone, Gender = @gender, Adress = @adress, Description = @description WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", newTeacher.Id);
                    command.Parameters.AddWithValue("@name", newTeacher.Name);
                    command.Parameters.AddWithValue("@firstname", newTeacher.FirstName);
                    command.Parameters.AddWithValue("@email", newTeacher.Email);
                    command.Parameters.AddWithValue("@phone", newTeacher.Phone);
                    command.Parameters.AddWithValue("@gender", newTeacher.Gender);
                    command.Parameters.AddWithValue("@adress", newTeacher.Adress);
                    command.Parameters.AddWithValue("@description", newTeacher.Description);

                    connection.Open();

                    int result = command.ExecuteNonQuery();
                }

                String query2 = $"DELETE FROM Teacher_Subject WHERE Teacher_id = {newTeacher.Id};";

                foreach (ListItem li in lbSubject.Items)
                {
                    if (li.Selected)
                    {
                        query2 += $"INSERT INTO Teacher_Subject (Teacher_id, Subject_id) VALUES ({newTeacher.Id},{li.Value});";
                    }
                }

                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0) return true;
                    else return false;
                }
            }
        }

        #endregion

        internal static bool UpdatePassword(string personType, int personId, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = $"UPDATE {personType} SET Password = @password WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", personId);
                    command.Parameters.AddWithValue("@personType", personType);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0) return false;

                    else return true;

                }
            }
        }

        #region EVENT

        public static void LoadSubjects(ListBox lb)
        {
            DataTable subjects = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Name, Id FROM Subject ORDER BY Name", con);
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

        public static Event[] GetEventsbyTeacherId(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<Event> Events = new List<Event>();
                connection.Open();
                String query = $"SELECT * FROM Event WHERE Teacher_id={id}";
                SqlCommand myCommand = new SqlCommand(query, connection);

                SqlDataReader rdr = myCommand.ExecuteReader();

                while (rdr.Read())
                {
                    Event newEvent = new Event();

                    newEvent.id = (int)rdr["Id"];
                    newEvent.title = rdr["Title"].ToString();
                    newEvent.start = (DateTime)rdr["Start_date"];
                    newEvent.end = (DateTime)rdr["End_date"];
                    newEvent.url = rdr["Url"].ToString();
                    newEvent.teacher_id = (int)rdr["Teacher_id"];
                    newEvent.student_id = GetValue<int>(rdr["Student_id"]);
                    newEvent.backgroundColor = rdr["BackgroundColor"].ToString();
                    newEvent.description = rdr["Description"].ToString();
                    newEvent.state = rdr["State"].ToString();

                    Events.Add(newEvent);
                }

                Event[] EventsArr = Events.ToArray();


                return EventsArr;
            }

        }

        public static int CreateEvent(Event newEvent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "INSERT INTO Event (Start_date, End_date, Teacher_id, BackgroundColor, State) VALUES (@start,@end,@teacher_id,'green', 'Libre'); SELECT MAX(id) FROM Event";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@start", newEvent.start);
                    command.Parameters.AddWithValue("@end", newEvent.end);
                    command.Parameters.AddWithValue("@teacher_id", newEvent.teacher_id);

                    connection.Open();
                    return (int)command.ExecuteScalar();

                }
            }
        }

        public static void UpdateEvent(Event updatedEvent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "UPDATE Event SET Start_date = @start, End_date = @end";
                if (updatedEvent.title != null) query += ", Title = @title";
                if (updatedEvent.description != null) query += ", Description = @description";
                if (updatedEvent.student_id != 0) query += ", Student_id = @student_id, BackgroundColor = 'grey', State = 'Réservé'";
                query += " WHERE id=@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@start", updatedEvent.start);
                    command.Parameters.AddWithValue("@end", updatedEvent.end);
                    command.Parameters.AddWithValue("@id", updatedEvent.id);
                    if (updatedEvent.title != null) command.Parameters.AddWithValue("@title", updatedEvent.title);
                    if (updatedEvent.description != null) command.Parameters.AddWithValue("@description", updatedEvent.description);
                    if (updatedEvent.student_id != 0) command.Parameters.AddWithValue("@student_id", updatedEvent.student_id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                }

            }

        }

        public static void UpdateEventTitleDesc(int eventId, string title, string desc)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "UPDATE Event SET Title = @title, Description = @description WHERE id=@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", eventId);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", desc);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                }
            }
        }

        public static void BookEvent(int eventId, int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "UPDATE Event SET Student_id = @student_id, State = 'Réservé', BackgroundColor = 'grey'  WHERE id=@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", eventId);
                    command.Parameters.AddWithValue("@student_id", studentId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteEvent(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "DELETE FROM Event WHERE id=@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                }
            }
        }

        public static Event GetEventbyId(int event_id, int teacher_id)
        {
            Event oneEvent = new Event();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                String query = $"SELECT * FROM Event WHERE Teacher_id={teacher_id} AND Id = {event_id}";
                SqlCommand myCommand = new SqlCommand(query, connection);

                SqlDataReader rdr = myCommand.ExecuteReader();

                while (rdr.Read())
                {
                    oneEvent.id = (int)rdr["Id"];
                    oneEvent.title = rdr["Title"].ToString();
                    oneEvent.start = (DateTime)rdr["Start_date"];
                    oneEvent.end = (DateTime)rdr["End_date"];
                    oneEvent.url = rdr["Url"].ToString();
                    oneEvent.teacher_id = (int)rdr["Teacher_id"];
                    oneEvent.student_id = GetValue<int>(rdr["Student_id"]);
                    oneEvent.backgroundColor = rdr["BackgroundColor"].ToString();
                    oneEvent.description = rdr["Description"].ToString();
                }
            }
            return oneEvent;
        }

        internal static void FreeEvent(object eventId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                String query = "UPDATE Event SET Student_id = NULL, State = 'Libre', BackgroundColor = 'green' WHERE id=@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", eventId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                }
            }
        }

        public static T GetValue<T>(object value)
        {
            if (value == null || value == DBNull.Value)
                return default(T);
            else
                return (T)value;
        }

        #endregion
    }
}
