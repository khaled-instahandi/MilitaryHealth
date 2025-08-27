using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class SpecializationDto
{
    public int SpecializationID { get; set; }

    public string Description { get; set; } = null!;

}
