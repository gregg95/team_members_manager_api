using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.Features.TeamMember.Queries;

public record GetAllTeamMembersQuery() : IRequest<List<TeamMemberDto>>;

public class GetAllTeamMembersQueryHandler :
    IRequestHandler<GetAllTeamMembersQuery, List<TeamMemberDto>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public GetAllTeamMembersQueryHandler(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public Task<List<TeamMemberDto>> Handle(
        GetAllTeamMembersQuery request,
        CancellationToken cancellationToken)
    {
        var teamMembers = _teamMemberRepository.GetAll();

        var result = teamMembers.Adapt<List<TeamMemberDto>>();

        return Task.FromResult(result);
    }
}