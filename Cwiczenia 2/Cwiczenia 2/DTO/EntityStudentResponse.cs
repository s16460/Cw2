using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.DTO
{
    public class EntityStudentResponse
{

        public string IndexNumber { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int IdEnrollment { get; set; }
        public string Studies { get; set; }

        public int Semestr { get; set; }
    }
}
