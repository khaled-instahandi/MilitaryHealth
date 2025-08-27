using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class MaritalStatusDto
{
    public int MaritalStatusID { get; set; }

    public string Description { get; set; } = null!;

}
