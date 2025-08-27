﻿using Application.DTOs.EyeExams;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class EyeExamDto
{
    public int EyeExamID { get; set; }

    public int? ApplicantID { get; set; }

    public string ApplicantFileNumber { get; set; } = null!;

    public int? DoctorID { get; set; }

    public string? Vision { get; set; }

    public string? ColorTest { get; set; }

    public int? RefractionTypeID { get; set; }

    public decimal? RefractionValue { get; set; }

    public string? OtherDiseases { get; set; }

    public int? ResultID { get; set; }

    public string? Reason { get; set; }

    public virtual ApplicantDto? Applicant { get; set; }

    public virtual DoctorDto? Doctor { get; set; }

    public virtual Application.DTOs.EyeExams.RefractionTypeDto? RefractionType { get; set; }

    public virtual Application.DTOs.EyeExams.ResultDto? Result { get; set; }
}
