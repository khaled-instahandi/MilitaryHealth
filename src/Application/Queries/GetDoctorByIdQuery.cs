using Application.DTOs;
using MediatR;

public sealed record GetDoctorByIdQuery(int DoctorId) : IRequest<DoctorDto?>;
