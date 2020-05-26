using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.DTO
{
    public class EntitiEnrollemntReq
{
        public string IndexNumber { get; set; }

        public string FirstName { get; set; }
   
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int EnrolmentId { get; set; }

    }
}
