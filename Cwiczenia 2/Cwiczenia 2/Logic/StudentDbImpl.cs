using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia2.DTO;
using Cwiczenia2.Models;
using Cwiczenia2.Utils;

namespace Cwiczenia2.Logic
{
    public class StudentDbImpl : StudentDb
    {

        public bool checkIfStudentExists(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(SystemConsts.DB_ADDRESS))
            using (SqlCommand com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "select * from Student where IndexNumber = @index";
                com.Parameters.AddWithValue("@index", indexNumber);
                Console.WriteLine("drugi index number " + indexNumber);
                Console.WriteLine("comand text " + com.CommandText);
                var reader = com.ExecuteReader();
                if (!reader.Read())
                {
                    Console.WriteLine("jestem tutaj22");
                    return true;
                }
                return false;
            }
        }

        public List<string> getStudentSemester(string id)
        {
            List<string> list = new List<string>();

            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))

            using (var command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "select e.Semester from Enrollment e join Student s on e.IdEnrollment = s.IdEnrollment where s.IndexNumber = @id;";

                SqlParameter param = new SqlParameter("@id", id);
                command.Parameters.Add(param);

                connection.Open();

                var read = command.ExecuteReader();

                while (read.Read())
                {
                    list.Add(read["semester"].ToString());
                }
                connection.Close();
            }
            return list;
        }



        public List<Student> getStudentsFromDb()
        {

            List<Student> studentsList = new List<Student>();


            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))

            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select * from Student";
                connection.Open();

                var read = command.ExecuteReader();
                while (read.Read())
                {
                    var st = new Student();
                    st.FirstName = read["FirstName"].ToString();
                    st.LastName = read["LastName"].ToString();
                    st.IndexNumber = read["IndexNumber"].ToString();
                    st.bornDate = read["BirthDate"].ToString();
                    st.idEnrolment = (int)read["IdEnrollment"];
                    studentsList.Add(st);
                }
                connection.Close();
            }
            return studentsList;
        }

    }
}
