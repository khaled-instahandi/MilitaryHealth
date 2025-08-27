using Application.DTOs.EyeExams;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public  class OrthopedicExamRequest
{


    public string ApplicantFileNumber { get; set; } = null!;

    public int? DoctorID { get; set; }

    public string? Musculoskeletal { get; set; }

    public string? NeurologicalSurgery { get; set; }

    public int? ResultID { get; set; }

    public string? Reason { get; set; }

    //public virtual ApplicantDto? Applicant { get; set; }

    //public virtual DoctorDto? Doctor { get; set; }

    //public virtual ResultDto? Result { get; set; }
}
