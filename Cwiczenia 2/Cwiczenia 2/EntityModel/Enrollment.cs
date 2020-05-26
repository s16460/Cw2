using System;
using System.Collections.Generic;

namespace Cwiczenia2.EntityModel
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Student = new HashSet<Student>();
        }

        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        public Studies IdStudyNavigation { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
