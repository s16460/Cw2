using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Utils
{
    public class SystemConsts
    {

        public static string DB_ADDRESS = "Data Source=db-mssql;Initial Catalog=s16460;Integrated Security=True";
        public static string DB_QUERRY_GET_ENROLMENT_BY_NAME_AND_STUDY_ID = "select IdEnrollment, Semester, StartDate, E.IdStudy from Enrollment E join Studies S on S.IdStudy = E.IdStudy where Semester = @semestr and S.Name = @name";
        public static string DB_INSERT_STUDENT = "insert into student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment, Password, Salt) values(@index, @name, @LastName, @BirthDate, @EnrolemntId, @Password, @Salt)";
        public static string DB_GET_STUDY_ID_BY_STUDY_NAME = "select IdStudy from Studies where Name = @StudyName";
        public static string DB_SELECT_ALL_FROM_STUDENTS_BY_INDEX_NUMBER = "select * from Student where IndexNumber=@IndexNumber";
        public static string DB_INSERT_ENROLMENT = "insert into Enrollment (IdEnrollment, IdStudy, Semester, StartDate) values (@EnrolmentId, @IdStudies, @Semester, @TodayDate)";
        public static string DB_SELECT_ALL_FROM_ENROLMENT_BY_SEMESTER_AND_STUDIES_ID = "select * from Enrollment where Semester = @Semester and IdStudy = @IdStudies";
        public static string DB_GET_SALT = "select salt from Student where indexNumber = @index";
        public static string DB_SELECT_ALL_FROM_STUDENT_BY_INDEX_NUMBER_AND_PASSWORD = "select * from Student where IndexNumber = @index and password = @password";
        public static string DB_SELECT_ALL_FROM_REFRESH_TOKENS = "select * from RefreshTokens where Token = @token";
        public static string DB_INSERT_REFRESH_TOKEN = "INSERT INTO RefreshTokens VALUES (@token)";

    }
}
