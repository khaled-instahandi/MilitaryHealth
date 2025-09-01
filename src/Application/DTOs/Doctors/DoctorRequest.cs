using Application.DTOs.Users;
using System;
using System.Collections.Generic;

namespace Application.DTOs;

public partial class DoctorRequest
{
    //public int DoctorID { get; set; }

    public string FullName { get; set; } = null!;
    public string Status { get; set; }

    public int? SpecializationID { get; set; }

    public int? ContractTypeID { get; set; }

    public string? Code { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}
