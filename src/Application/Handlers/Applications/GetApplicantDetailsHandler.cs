using Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetApplicantDetailsHandler : IRequestHandler<GetApplicantDetailsQuery, ApplicantDetailsDto?>
{
    private readonly IApplicantService _applicantService;

    public GetApplicantDetailsHandler(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    public async Task<ApplicantDetailsDto?> Handle(GetApplicantDetailsQuery request, CancellationToken cancellationToken)
    {
        var details = await _applicantService.GetApplicantDetailsAsync(
            request.Id.ToString(),
            cancellationToken
        );

        return details;
    }
}
