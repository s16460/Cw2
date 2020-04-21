using Cwiczenia2.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
    public class IEnrolementDbImpl : IEnrolmentDb
    {

        private string dbAddress = "Data Source=db-mssql;Initial Catalog=s16460;Integrated Security=True";

        public EnrolmentResp enrolStudent(EnrolmentReq req)
        {
            using (SqlConnection connection = new SqlConnection(dbAddress))

            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;


                command.CommandText = "select IdStudy from Studies where Name = @StudyName";
                SqlParameter param = new SqlParameter("@StudyName", req.Studies);
                command.Parameters.Add(param);

                var reader = command.ExecuteReader();
                int StudiesId = 0;

                if (!reader.Read())
                {
                    reader.Close();
                    transaction.Rollback();
                    return null;
                }
                else
                {

                    StudiesId = Int32.Parse(reader["IdStudy"].ToString());
                }

                reader.Close();

                command.CommandText = "select * from Enrollment where Semester=1 and IdStudy=@IdStudies";
                SqlParameter param2 = new SqlParameter("@IdStudies", StudiesId);
                command.Parameters.Add(param2);
                int IdEnrollment = 0;
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IdEnrollment = Int32.Parse(reader["IdEnrollment"].ToString());

                }
                else
                {
                    command.CommandText = "select max(IdEnrollment) as EnrolmentMaxId from Enrollment where Semester = 1";
                    reader = command.ExecuteReader();

                    IdEnrollment = Int32.Parse(reader["EnrolmentMaxId"].ToString());
                    DateTime todayDate = DateTime.Today;
                    command.CommandText = "insert into Enrollment (IdEnrollment, IdStudy, Semester, StartDate)values (@EnrolmentId, @IdStudies, 1, @TodayDate);";
                    SqlParameter param3 = new SqlParameter("@TodayDate", todayDate);
                    command.Parameters.Add(param3);
                    SqlParameter param4 = new SqlParameter("@EnrolmentId", IdEnrollment);
                    command.Parameters.Add(param3);
                    command.ExecuteNonQuery();

                }
                reader.Close();

                command.CommandText = "select * from Student where IndexNumber=@IndexNumber";
                command.Parameters.Clear();
                SqlParameter param5 = new SqlParameter("@IndexNumber", req.IndexNumber);
                command.Parameters.Add(param5);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    transaction.Rollback();
                    return null;

                }

                reader.Close();

                command.CommandText = "insert into student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values(@index, @name, @LastName, @BirthDate, @EnrolemntId)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@index", req.IndexNumber);
                command.Parameters.AddWithValue("@name", req.FirstName);
                command.Parameters.AddWithValue("@LastName", req.LastName);
                command.Parameters.AddWithValue("@BirthDate", req.BirthDate);
                command.Parameters.AddWithValue("@EnrolemntId", IdEnrollment);

                command.ExecuteNonQuery();

                var response = new EnrolmentResp()
                {
                    IdEnrollment = IdEnrollment,
                    Semester = 1
                };

                transaction.Commit();
                return response;
            }
        }

        public PromotionsResp promoteStudents(PromotionsReq req)
        {
            using (SqlConnection connection = new SqlConnection(dbAddress))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "select IdEnrollment, Semester, StartDate, S.IdStudy IdStudy from Enrollment E join Studies S on S.IdStudy = E.IdStudy where Semester = @semestr and S.Name = @name";
                command.Parameters.AddWithValue("@semestr", req.Semester);
                command.Parameters.AddWithValue("@name", req.Studies);

                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    return null;
                }

                reader.Close();

                command.CommandText = "EXECUTE dbo.StudentsPromotion @Studies, @Semester";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Studies", req.Studies);
                command.Parameters.AddWithValue("@Semester", req.Semester);
                command.ExecuteNonQuery();

                command.CommandText = "select IdEnrollment, Semester, StartDate, S.IdStudy IdStudy from Enrollment E join Studies S on S.IdStudy = E.IdStudy where Semester = @semestr and S.Name = @name";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@semestr", req.Semester + 1);
                command.Parameters.AddWithValue("@name", req.Studies);

                reader = command.ExecuteReader();

                var resp = new PromotionsResp();
                if (reader.Read())
                {
                    resp.IdEnrollment = Int32.Parse(reader["IdEnrollment"].ToString());
                    resp.Semester = Int32.Parse(reader["Semester"].ToString());
                    resp.IdStudy = Int32.Parse(reader["IdStudy"].ToString());
                    resp.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                }

                return resp;

            }
        }
    }
}
