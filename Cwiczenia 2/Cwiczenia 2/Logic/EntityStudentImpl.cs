﻿using Cwiczenia2.DTO;
using Cwiczenia2.EntityModel;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
    public class EntityStudentImpl : IEntityStudent
    {

        private readonly s16460Context context;

        public EntityStudentImpl(s16460Context context)
        {
            this.context = context;
        }


        public List<EntityStudentResponse> getStudents()
        {
            List<EntityStudentResponse> students = context.Student.Select(s => new EntityStudentResponse
            {
                IndexNumber = s.IndexNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                BirthDate = s.BirthDate,
                IdEnrollment = s.IdEnrollment,
                Studies = s.IdEnrollmentNavigation.IdStudyNavigation.Name,
                Semestr = s.IdEnrollmentNavigation.Semester
            }).ToList();

            return students;
        }

        public Student updateStudent(EntityUpdateStudent student)
        {
            Student studentFromDB = context.Student.Where(s => s.IndexNumber == student.IndexNumber).FirstOrDefault();

            Console.WriteLine(studentFromDB);
            if (studentFromDB == null)
            {
                return null;
            }

            studentFromDB.FirstName = student.FirstName;
            studentFromDB.LastName = student.LastName;
            studentFromDB.BirthDate = student.BirthDate;
            try
            {
                context.Student.Update(studentFromDB);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return studentFromDB;
        }

        public String deleteStudent(string indexNumber)
        {
            Student studentFromDB = context.Student.Where(s => s.IndexNumber == indexNumber).FirstOrDefault();


            if (studentFromDB == null)
            {
                return null;
            }
            try
            {
                context.Student.Remove(studentFromDB);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            return "Student usuniety";
        }

        public String promoteStudents(PromotionsReq request)
        {
            var enrolments = context.Enrollment.Where(e => e.Semester == request.Semester).Where(e => e.IdStudyNavigation.Name == request.Studies).ToList();
            if (enrolments.Count == 0 || enrolments == null)
            {
                return null;
            }
            enrolments.ForEach(e => e.Semester = e.Semester + 1);
            enrolments.ForEach(e => context.Enrollment.Update(e));
            context.SaveChanges();

            return "udało się zwiekszyć lvl studentów";
        }

        public String enrollStudent(EnrolmentReq request)
        {
            var studentToCheck = context.Student.Where(s => s.IndexNumber == request.IndexNumber).FirstOrDefault();

            if (studentToCheck != null)
            {
                return null;
            }

            string salt = "";
            byte[] randomBytes = new byte[256];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                salt = Convert.ToBase64String(randomBytes);
            }

            var valueBytes = KeyDerivation.Pbkdf2(
                password: request.password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256);

            //hashed password
            request.password = Convert.ToBase64String(valueBytes);

            var studies = context.Studies.Where(s => s.Name == request.Studies).FirstOrDefault();
            if (studies == null)
            {
                return null;
            }

            int maxenrolment = context.Enrollment.Max(e => e.IdEnrollment) + 1;

            var enrolment = new Enrollment
            {
                IdEnrollment = maxenrolment,
                IdStudy = studies.IdStudy,
                Semester = 1,
                StartDate = DateTime.Now
            };
            context.Enrollment.Add(enrolment);
            context.SaveChanges();

            Student student = new Student{
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = maxenrolment,
                Password = request.password,
                Salt = salt
            };
			
			try 
			{
            context.Student.Add(student);
            context.SaveChanges();
			} 
			catch(Exception e) 
			{
				Console.WriteLine(e.StackTrace);
                return null;
			}

            Console.WriteLine(student.ToString());
            return "student o indexie " + student.IndexNumber + " utworzony";
        }
    }
}
