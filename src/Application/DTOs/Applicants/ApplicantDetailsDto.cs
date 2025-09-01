﻿
using Application.DTOs;
using System.ComponentModel.DataAnnotations;

public class ApplicantDetailsDto
{
    public int? ApplicantID { get; set; }
    public string? FileNumber { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int? MaritalStatusID { get; set; }

    public string? Job { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public decimal? BMI { get; set; }

    public string? BloodPressure { get; set; }

    public int? Pulse { get; set; }
    public bool? Tattoo { get; set; }

    public string? DistinctiveMarks { get; set; }
    public DateTime? CreatedAt { get; set; }

    public MaritalStatusDto? MaritalStatus { get; set; }
    public EyeExamDto EyeExam { get; set; }
    public SurgicalExamDto SurgicalExam { get; set; }
    public OrthopedicExamDto OrthopedicExamDto { get; set; }
    public InternalExamDto InternalExam { get; set; }
    public InvestigationDto Investigation { get; set; }
    public ConsultationDto Consultation { get; set; }

}
