using Application.DTOs;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Identity;

public class DoctorService : IDoctorService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public DoctorService(AppDbContext db,
                         UserManager<ApplicationUser> userManager,
                         RoleManager<IdentityRole<int>> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<DoctorDto> CreateDoctorWithUserAsync(DoctorRequest req, CancellationToken ct)
    {
        var doctor = new Doctor
        {
            FullName = req.FullName,
            SpecializationID = req.SpecializationID,
            ContractTypeID = req.ContractTypeID,
            Code = req.Code,
            
        };

        _db.Doctors.Add(doctor);
        await _db.SaveChangesAsync(ct);

        var user = new ApplicationUser
        {
            UserName = req.Username,
            FullName = req.FullName,

            Status =req?.Status,
            Email = req?.Username,
            DoctorID = doctor.DoctorID,
            
        };

        var result = await _userManager.CreateAsync(user, req?.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

        if (!await _roleManager.RoleExistsAsync("Doctor"))
            await _roleManager.CreateAsync(new IdentityRole<int>("Doctor"));

        await _userManager.AddToRoleAsync(user, "Doctor");

        return new DoctorDto
        {
            DoctorID = doctor.DoctorID,
            FullName = doctor.FullName,
            SpecializationID = doctor.SpecializationID,
            ContractTypeID = doctor.ContractTypeID,
            Code = doctor.Code,
            
        };
    }
}