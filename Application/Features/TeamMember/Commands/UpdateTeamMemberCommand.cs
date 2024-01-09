using Domain.Errors;
using Domain.Repositories;
using Infrastructure.Presistence;
using MediatR;

namespace Application.Features.TeamMember.Commands;

public record UpdateTeamMemberCommand(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber) : IRequest<Unit>;

public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand, Unit>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ApplicationDbContext _dbContext;

    public UpdateTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.TeamMember teamMember = _teamMemberRepository.GetById(request.Id);

        if (teamMember is null)
        {
            throw new NotFoundException($"Team member with id {request.Id} does not exists");
        }

        teamMember.UpdateName(request.Name);
        teamMember.UpdateEmail(request.Email);
        teamMember.UpdatePhoneNumber(request.PhoneNumber);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}