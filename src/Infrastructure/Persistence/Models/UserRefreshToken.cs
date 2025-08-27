using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class UserRefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public string? JwtId { get; set; }

    public DateTime ExpiresOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? RevokedOn { get; set; }

    public virtual User User { get; set; } = null!;
}
