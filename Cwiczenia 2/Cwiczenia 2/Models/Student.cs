using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Models
{
    public class Student
    {
        public int IdStudenta { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public string bornDate { get; set; }
        public int idEnrolment { get; set; }

        public override string ToString()
        {
            return base.ToString() + " first name " + FirstName + " lastName " + LastName + " index number " + IndexNumber +
                " born date "+ bornDate + " enrolment "+idEnrolment.ToString() ;
        }

    }
}
