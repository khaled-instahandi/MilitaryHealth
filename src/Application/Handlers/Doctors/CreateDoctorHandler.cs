using MediatR;

namespace Application.DTOs;
public class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, DoctorDto>
{
    private readonly IDoctorService _doctorService;

    public CreateDoctorHandler(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public async Task<DoctorDto> Handle(CreateDoctorCommand request, CancellationToken ct)
    {
        return await _doctorService.CreateDoctorWithUserAsync(request.Request, ct);
    }
}