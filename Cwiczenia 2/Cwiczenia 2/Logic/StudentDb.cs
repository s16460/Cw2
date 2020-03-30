using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia2.Models;

namespace Cwiczenia2.Logic
{
    public interface StudentDb {

    List<Student> getStudentsFromDb();

    List<String> getStudentSemester(string id);

    }
}
