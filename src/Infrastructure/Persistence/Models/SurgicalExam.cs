using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class SurgicalExam
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

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<FinalDecision> FinalDecisions { get; set; } = new List<FinalDecision>();

    public virtual Result? Result { get; set; }
}
