using Cwiczenia2.DTO;
using Cwiczenia2.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EnrolmentsController : ControllerBase {

        private readonly IEnrolmentDb enrolmentDb;

        public EnrolmentsController(IEnrolmentDb enrolmentDb)
        {
            this.enrolmentDb = enrolmentDb;
        }
        [HttpPost]
        public IActionResult EnrollStudent(EnrolmentReq request)
        {
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

            var resp = enrolmentDb.enrolStudent(request, salt);
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
