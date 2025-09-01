using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Archive
{
    public int ArchiveID { get; set; }

    public int ApplicantID { get; set; }

    public int DecisionID { get; set; }

    public string FileNumber { get; set; } = null!;

    public string ApplicantFileNumber { get; set; } = null!;

    public DateTime? ArchiveDate { get; set; }

    public string? DigitalCopy { get; set; }

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual Applicant ApplicantFileNumberNavigation { get; set; } = null!;
}
