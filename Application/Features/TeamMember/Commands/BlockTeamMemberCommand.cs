using Domain.Errors;
using Domain.Repositories;
using Infrastructure.Presistence;
using MediatR;

namespace Application.Features.TeamMember.Commands;

public record BlockTeamMemberCommand(Guid Id) : IRequest<Unit>;

public class BlockTeamMemberCommandHandler : IRequestHandler<BlockTeamMemberCommand, Unit>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ApplicationDbContext _dbContext;

    public BlockTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(BlockTeamMemberCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.TeamMember teamMember = _teamMemberRepository.GetById(request.Id);

        if (teamMember is null)
        {
            throw new NotFoundException($"Team member with id {request.Id} does not exists");
        }

        teamMember.Block();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}