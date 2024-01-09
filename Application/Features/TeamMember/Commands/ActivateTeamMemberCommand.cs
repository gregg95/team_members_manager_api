using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Infrastructure.Presistence;
using MediatR;

namespace Application.Features.TeamMember.Commands;

public record ActivateTeamMemberCommand(Guid Id) : IRequest<Unit>;

public class ActivateTeamMemberCommandHandler : IRequestHandler<ActivateTeamMemberCommand, Unit>
{    
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ApplicationDbContext _dbContext;

    public ActivateTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(ActivateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.TeamMember teamMember = _teamMemberRepository.GetById(request.Id);

        if (teamMember is null)
        {
            throw new NotFoundException($"Team member with id {request.Id} does not exists");
        }

        teamMember.Activate();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}