using Application.DTOs.EyeExams;
using System;
using System.Collections.Generic;

namespace Application.DTOs;

public partial class SurgicalExamDto
{
    public int SurgicalExamID { get; set; }

    public string ApplicantFileNumber { get; set; } = null!;

    public int? DoctorID { get; set; }

    public string? GeneralSurgery { get; set; }

    public string? UrinarySurgery { get; set; }

    public string? VascularSurgery { get; set; }

    public string? ThoracicSurgery { get; set; }

    public int? ResultID { get; set; }

    public string? Reason { get; set; }

    public virtual DoctorDto? Doctor { get; set; }

    public virtual Application.DTOs.EyeExams.ResultDto? Result { get; set; }
}
