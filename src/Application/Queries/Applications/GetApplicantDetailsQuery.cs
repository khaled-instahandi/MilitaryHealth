using MediatR;

public class GetApplicantDetailsQuery : IRequest<ApplicantDetailsDto?>
{
    public string Id { get; }
    public GetApplicantDetailsQuery(string id) => Id = id;
}
