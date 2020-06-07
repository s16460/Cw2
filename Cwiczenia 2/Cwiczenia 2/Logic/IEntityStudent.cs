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

        List<EntityStudentResponse> getStudents();
        Student updateStudent(EntityUpdateStudent student);
        String deleteStudent(string indexNumber);
        String promoteStudents(PromotionsReq request);
        String enrollStudent(EnrolmentReq request);


}
}
