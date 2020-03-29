using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia2.Controllers {

    [ApiController]
    [Route("api/students")]

    public class StudentController : ControllerBase {

        [HttpGet]
        public string GetStudents([FromQuery] string orderBy) {
            return $"Kowalski, Maliniak sortowanie = {orderBy}";
        }

        [HttpPost]
        public IActionResult AddStudent(Students student )
        {
            student.IndexNumber = $"s {new Random().Next(1, 20000)}";
            System.Diagnostics.Debug.WriteLine(student.IndexNumber);
            return Ok(student);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent([FromRoute] int id)
        {
            
            return Ok("aktualizacja dokonczona");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            
            return Ok("usuwanie ukonczone");
        }

        // opisuje status code
        [HttpGet("{id:int}")]   //:int -> validacja
        public IActionResult GetStudentBtId([FromRoute] int id) {
            if (id == 1)
            {
                return Ok("Kowalski");
            } else if(id == 2)
            {
                return Ok("blabla");
            }
            return NotFound($"nie znaleziono id o {id}");
        }
    }
}
