using System;
using System.Collections.Generic;

namespace Cwiczenia2.EntityModel
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }

        public Enrollment IdEnrollmentNavigation { get; set; }

        public override string ToString()
        {
            return IndexNumber + " " + FirstName + " " + LastName + " " + BirthDate + " " + IdEnrollment;
        }
    }
}
