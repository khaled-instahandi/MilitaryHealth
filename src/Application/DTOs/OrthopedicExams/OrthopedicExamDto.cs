using Application.DTOs.EyeExams;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public  class OrthopedicExamDto
{
    public int OrthopedicExamID { get; set; }

    public int? ApplicantID { get; set; }

    public string ApplicantFileNumber { get; set; } = null!;

    public int? DoctorID { get; set; }

    public string? Musculoskeletal { get; set; }

    public string? NeurologicalSurgery { get; set; }

    public int? ResultID { get; set; }

    public string? Reason { get; set; }

    public virtual ApplicantDto? Applicant { get; set; }

    public virtual DoctorDto? Doctor { get; set; }

    public virtual Application.DTOs.EyeExams.ResultDto? Result { get; set; }
}
