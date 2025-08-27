using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class RefractionTypeDto
{
    public int RefractionTypeID { get; set; }

    public string Description { get; set; } = null!;

}
