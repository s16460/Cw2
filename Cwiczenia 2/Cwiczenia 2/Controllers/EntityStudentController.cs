using Cwiczenia2.DTO;
using Cwiczenia2.EntityModel;
using Cwiczenia2.Logic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Controllers
{
    [ApiController]
    [Route("api/entityStudent")]
    public class EntityStudentController : ControllerBase
    {

        private readonly s16460Context context;
        private readonly IEntityStudent entityStudent;

        public EntityStudentController(s16460Context context, IEntityStudent entityStudent)
        {
            this.context = context;
            this.entityStudent = entityStudent;
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            /*List<EntityStudentResponse> students = context.Student.Select(s => new EntityStudentResponse
            {
                IndexNumber = s.IndexNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                BirthDate = s.BirthDate,
                IdEnrollment = s.IdEnrollment,
                Studies = s.IdEnrollmentNavigation.IdStudyNavigation.Name,
                Semestr = s.IdEnrollmentNavigation.Semester
            }).ToList();*/

            return entityStudent.getStudents(); 
        }


        [HttpPut]
        public IActionResult UpdateStudent(EntityUpdateStudent student)
        {

            return entityStudent.updateStudent(student);
        }


        [HttpDelete]
        public IActionResult DeleteStudent(string indexNumber)
        {
           
            return entityStudent.deleteStudent(indexNumber);

        }

        [HttpPut]
        [Route("promote")]
        public IActionResult PromoteStudents(PromotionsReq request)
        {
            
            return entityStudent.promoteStudents(request);

        }

        [HttpPost]
        [Route("enroll")]
        public IActionResult EnrollStudent(EnrolmentReq request)
        {

            return entityStudent.enrollStudent(request);

        }
    }
}
