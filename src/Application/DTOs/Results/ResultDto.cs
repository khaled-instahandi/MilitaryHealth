using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class ResultDto
{
    public int ResultID { get; set; }

    public string Description { get; set; } = null!;

  
}
