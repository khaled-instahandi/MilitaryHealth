using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class DoctorRequest
{
    public int DoctorID { get; set; }

    public string FullName { get; set; } = null!;

    public int? SpecializationID { get; set; }

    public int? ContractTypeID { get; set; }

    public string? Code { get; set; }

   
}
