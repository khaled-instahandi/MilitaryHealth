using Application.DTOs;
using Infrastructure.Persistence.Models;

public record CreateDoctorCommand(DoctorRequest Request) : ICommand<DoctorDto>;
