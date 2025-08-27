using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class RefractionType
{
    public int RefractionTypeID { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<EyeExam> EyeExams { get; set; } = new List<EyeExam>();
}
