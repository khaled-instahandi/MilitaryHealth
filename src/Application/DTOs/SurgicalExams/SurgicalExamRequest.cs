using System;
using System.Collections.Generic;

namespace Application.DTOs;

public partial class SurgicalExamRequest
{

    public string ApplicantFileNumber { get; set; } = null!;

    public int? DoctorID { get; set; }

    public string? GeneralSurgery { get; set; }

    public string? UrinarySurgery { get; set; }

    public string? VascularSurgery { get; set; }

    public string? ThoracicSurgery { get; set; }

    public int? ResultID { get; set; }

    public string? Reason { get; set; }

 
}
