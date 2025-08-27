using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class MaritalStatus
{
    public int MaritalStatusID { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
