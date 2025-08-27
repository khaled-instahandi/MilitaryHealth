using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class ConsultationRequest
{

    public int? DoctorID { get; set; }

    public string ApplicantFileNumber { get; set; } = null!;

    public string? ConsultationType { get; set; }

    public string? ReferredDoctor { get; set; }

    public byte[]? Result { get; set; }

    public string? Attachment { get; set; }

}
