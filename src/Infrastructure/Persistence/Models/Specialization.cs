using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Specialization
{
    public int SpecializationID { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
