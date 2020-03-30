using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia2.Logic;
using Cwiczenia2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cwiczenia2.Controllers {

    [ApiController]
    [Route("api/students")]

    public class StudentController : ControllerBase {

        private readonly StudentDb studentDb = new StudentDbImpl();

        [HttpGet]
        public IActionResult GetStudents() {

            List<Student> list = studentDb.getStudentsFromDb();
            //list.ForEach(x => Console.WriteLine(x.ToString()));

            return Ok(list);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student ) {
            student.IndexNumber = $"s {new Random().Next(1, 20000)}";
            System.Diagnostics.Debug.WriteLine(student.IndexNumber);
            return Ok(student);
        }
        //zadanie 7 z 3 zestawu
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent([FromRoute] int id) {

            return Ok("aktualizacja dokonczona");
        }
        //zadanie 7 z 3 zestawu
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent([FromRoute] int id) {

            return Ok("usuwanie ukonczone");
        }

        [HttpGet("getStudentSemester/{id}")]
        public IActionResult GetSemesterByStudentId([FromRoute] string id) {
            List<String> list = studentDb.getStudentSemester(id);
            return Ok(list);
        }

        // opisuje status code
        [HttpGet("{id:int}")]   //:int -> validacja
        public IActionResult GetStudentBtId([FromRoute] int id) {
            if (id == 1) {
                return Ok("Kowalski");
            }
            else if (id == 2) {
                return Ok("blabla");
            }
            return NotFound($"nie znaleziono id o {id}");
        }


    }
}
