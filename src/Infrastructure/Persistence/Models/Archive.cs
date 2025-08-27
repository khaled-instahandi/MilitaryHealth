using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Archive
{
    public int ArchiveID { get; set; }

    public int? ApplicantID { get; set; }

    public string ApplicantFileNumber { get; set; } = null!;

    public string? FileNumber { get; set; }

    public DateOnly? ArchiveDate { get; set; }

    public string? DigitalCopy { get; set; }

    public virtual Applicant? Applicant { get; set; }
}
