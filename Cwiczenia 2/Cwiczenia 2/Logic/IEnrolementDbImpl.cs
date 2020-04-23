using Cwiczenia2.DTO;
using Cwiczenia2.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
    public class IEnrolementDbImpl : IEnrolmentDb
    {

        public EnrolmentResp enrolStudent(EnrolmentReq req)
        {
            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))

            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;


                command.CommandText = SystemConsts.DB_GET_STUDY_ID_BY_STUDY_NAME;
                command.Parameters.AddWithValue("@StudyName", req.Studies);

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

                command.CommandText = SystemConsts.DB_SELECT_ALL_FROM_ENROLMENT_BY_SEMESTER_AND_STUDIES_ID;
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@Semester", 1);
                command.Parameters.AddWithValue("@IdStudies", StudiesId);

                int IdEnrollment = 0;
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IdEnrollment = Int32.Parse(reader["IdEnrollment"].ToString());

                }
                else
                {
                    command.CommandText = "select max(IdEnrollment) as EnrolmentMaxId from Enrollment where Semester = @Semester";
                    reader = command.ExecuteReader();

                    IdEnrollment = Int32.Parse(reader["EnrolmentMaxId"].ToString());
                    
                    command.CommandText = SystemConsts.DB_INSERT_ENROLMENT;
                    DateTime todayDate = DateTime.Today;
                    command.Parameters.AddWithValue("@TodayDate", todayDate);
                    command.Parameters.AddWithValue("@EnrolmentId", IdEnrollment);
                    /*DateTime todayDate = DateTime.Today;
                    SqlParameter param3 = new SqlParameter("@TodayDate", todayDate);
                    command.Parameters.Add(param3);
                    SqlParameter param4 = new SqlParameter("@EnrolmentId", IdEnrollment);
                    command.Parameters.Add(param3);*/
                    command.ExecuteNonQuery();

                }
                reader.Close();

                command.CommandText = SystemConsts.DB_SELECT_ALL_FROM_STUDENTS_BY_INDEX_NUMBER;
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

                command.CommandText = SystemConsts.DB_INSERT_STUDENT;
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
            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                
                getPromotiont(command, req.Studies, req.Semester);
 
                command.CommandText = "EXECUTE dbo.StudentsPromotion @Studies, @Semester";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Studies", req.Studies);
                command.Parameters.AddWithValue("@Semester", req.Semester);
                command.ExecuteNonQuery();

                return getPromotiont(command, req.Studies, req.Semester+1);

            }
        }

        private PromotionsResp getPromotiont(SqlCommand command, String studies, int semester)
        {
            command.CommandText = SystemConsts.DB_QUERRY_GET_ENROLMENT_BY_NAME_AND_STUDY_ID;
            command.Parameters.AddWithValue("@semestr", semester);
            command.Parameters.AddWithValue("@name", studies);

            var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                reader.Close();
                return null;
            }

            var resp = new PromotionsResp();
            
           
                resp.IdEnrollment = Int32.Parse(reader["IdEnrollment"].ToString());
                resp.Semester = Int32.Parse(reader["Semester"].ToString());
                resp.IdStudy = Int32.Parse(reader["IdStudy"].ToString());
                resp.StartDate = DateTime.Parse(reader["StartDate"].ToString());
            
            reader.Close();
            return resp;
        }
    }
}
