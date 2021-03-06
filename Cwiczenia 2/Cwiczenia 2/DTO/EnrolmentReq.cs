﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.DTO
{
    public class EnrolmentReq
{
    [Required] //annotation
    public string IndexNumber { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public string Studies { get; set; }

    [Required]
    public string password { get; set; }


}
}
