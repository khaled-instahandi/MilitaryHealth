using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class AuditLog
{
    public int AuditId { get; set; }

    public int? UserId { get; set; }

    public string EntityName { get; set; } = null!;

    public string EntityKey { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string? ChangedColumns { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public DateTime Timestamp { get; set; }
}
