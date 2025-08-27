using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppIdentityDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<ApplicationUser>(e =>
        {
            e.ToTable("Users"); // جدولك القديم
            e.Property(u => u.Id).HasColumnName("UserID");
            e.Property(u => u.UserName).HasColumnName("Username");
            e.Property(u => u.FullName).HasColumnName("FullName");
            e.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            e.Property(u => u.Email).HasColumnName("Email");
            e.Property(u => u.PhoneNumber).HasColumnName("PhoneNumber");
            e.Property(u => u.RefreshToken).HasColumnName("RefreshToken");
            e.Property(u => u.RefreshTokenExpiry).HasColumnName("RefreshTokenExpiry");
            e.Property(u => u.LastLogin).HasColumnName("LastLogin");
        });

        b.Entity<UserRefreshToken>(e =>
        {
            e.ToTable("UserRefreshTokens");
            e.HasKey(x => x.Id);
            e.Property(x => x.Token).IsRequired();
            e.HasOne<ApplicationUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId);
        });

        b.Entity<IdentityRole<int>>().ToTable("AspNetRoles");
        b.Entity<IdentityUserRole<int>>().ToTable("AspNetUserRoles");
        b.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
        b.Entity<IdentityUserLogin<int>>().ToTable("AspNetUserLogins");
        b.Entity<IdentityRoleClaim<int>>().ToTable("AspNetRoleClaims");
        b.Entity<IdentityUserToken<int>>().ToTable("AspNetUserTokens");
    }
}
