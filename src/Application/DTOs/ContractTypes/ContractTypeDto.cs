using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class ContractTypeDto
{
    public int ContractTypeID { get; set; }

    public string Description { get; set; } = null!;

}
