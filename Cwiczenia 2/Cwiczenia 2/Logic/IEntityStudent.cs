using Cwiczenia2.DTO;
using Cwiczenia2.EntityModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
    public interface IEntityStudent
{

        IActionResult getStudents();
        IActionResult updateStudent(EntityUpdateStudent student);
        IActionResult deleteStudent(string indexNumber);
        IActionResult promoteStudents(PromotionsReq request);
        IActionResult enrollStudent(EnrolmentReq request);


}
}
