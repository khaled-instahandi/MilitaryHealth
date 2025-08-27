using Application.DTOs.Doctors;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class DoctorDto
{
    public int DoctorID { get; set; }

    public string FullName { get; set; } = null!;

    public int? SpecializationID { get; set; }

    public int? ContractTypeID { get; set; }

    public string? Code { get; set; }
    public Application.DTOs.Doctors.SpecializationDto? Specialization { get; set; }
    public Application.DTOs.Doctors.ContractTypeDto? ContractType { get; set; }



}
