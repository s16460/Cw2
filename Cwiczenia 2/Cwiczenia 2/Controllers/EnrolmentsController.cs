using Cwiczenia2.DTO;
using Cwiczenia2.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cwiczenia2.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrolmentsController : ControllerBase {

        private readonly IEnrolmentDb enrolmentDb;

        public EnrolmentsController(IEnrolmentDb enrolmentDb)
        {
            this.enrolmentDb = enrolmentDb;
        }
        [HttpPost]
        public IActionResult EnrollStudent(EnrolmentReq request)
        {
            Console.WriteLine(request.BirthDate);
            var resp = enrolmentDb.enrolStudent(request);
            if (resp == null)
            {
                return BadRequest("nie istnieja studia lub taki student juz istnieje");
            }
            return CreatedAtAction("enrolemnt created",resp);
            //return Ok(resp);
        }



        [Route("promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(PromotionsReq request)
        {
            var resp = enrolmentDb.promoteStudents(request);
            if (resp == null)
                return NotFound("zły semestr lub student");

            return Ok(resp);
        }

    }
}
