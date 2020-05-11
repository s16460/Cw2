using Cwiczenia2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
    public interface IEnrolmentDb
{
    EnrolmentResp enrolStudent(EnrolmentReq req, string salt);

    PromotionsResp promoteStudents(PromotionsReq req);


}
}
