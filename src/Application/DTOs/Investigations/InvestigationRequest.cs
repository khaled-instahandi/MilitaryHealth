using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class InvestigationRequest
{

    public string ApplicantFileNumber { get; set; } = null!;

    public string? Type { get; set; }

    public string? Result { get; set; }

    public string? Attachment { get; set; }

    public string? Status { get; set; }

    public int DoctorID { get; set; }

}
