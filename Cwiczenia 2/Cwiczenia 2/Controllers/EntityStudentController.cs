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
            
            return Ok(entityStudent.getStudents()); 
        }


        [HttpPut]
        public IActionResult UpdateStudent(EntityUpdateStudent student)
        {
            Student resp = entityStudent.updateStudent(student);
            if (resp == null) {
                return BadRequest("problem z updejtem studenta");
            }
            return Ok(resp);
        }


        [HttpDelete]
        public IActionResult DeleteStudent(string indexNumber)
        {
           
            String resp = entityStudent.deleteStudent(indexNumber);
            if (resp == null)
            { 
                return BadRequest("problem z usuwaniem");
            }

            return Ok(resp);

        }

        [HttpPut]
        [Route("promote")]
        public IActionResult PromoteStudents(PromotionsReq request)
        {
            
            String resp = entityStudent.promoteStudents(request);

            if (resp == null)
            {
                return BadRequest("problem z promocja");
            }

            return Ok(resp);

        }

        [HttpPost]
        [Route("enroll")]
        public IActionResult EnrollStudent(EnrolmentReq request)
        {
            String resp = entityStudent.enrollStudent(request);

            if (resp == null)
            {
                return BadRequest("problem wpisaniem studenta");
            }

            return Ok(resp);

        }
    }
}
