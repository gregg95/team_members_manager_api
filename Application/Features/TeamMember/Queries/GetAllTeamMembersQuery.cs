using Domain.Errors;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.Features.TeamMember.Queries;

public record GetTeamMemberByIdQuery(Guid Id) : IRequest<TeamMemberDto>;

public class GetTeamMemberByIdQueryHandler :
    IRequestHandler<GetTeamMemberByIdQuery, TeamMemberDto>
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public GetTeamMemberByIdQueryHandler(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public Task<TeamMemberDto> Handle(
        GetTeamMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var teamMember = _teamMemberRepository.GetById(request.Id);

        if (teamMember == null)
        {
            throw new NotFoundException($"Team member with id {request.Id} not found");
        }

        return Task.FromResult(teamMember.Adapt<TeamMemberDto>());
    }
}