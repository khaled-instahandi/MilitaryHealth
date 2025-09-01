using MediatR;

public class GetApplicantQuery : IRequest<ApplicantDetailsDto?>
{
    public string Id { get; }
    public GetApplicantQuery(string id) => Id = id;
}
