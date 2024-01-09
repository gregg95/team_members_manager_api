using Domain.Repositories;
using Infrastructure.ExternalServices.RandomTeamMemberApiService;
using Infrastructure.Presistence;
using Infrastructure.Shared.Providers;
using MediatR;

namespace Application.Features.TeamMember.Commands;

public record ImportTeamMemberCommand() : IRequest<Guid>;

public class ImportTeamMemberCommandHandler : IRequestHandler<ImportTeamMemberCommand, Guid>
{
    private readonly IRandomTeamMemberApiService _randomTeamMemberApiService;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ImportTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext,
        IRandomTeamMemberApiService randomTeamMemberApiService,
        IDateTimeProvider dateTimeProvider)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
        _randomTeamMemberApiService = randomTeamMemberApiService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(ImportTeamMemberCommand request, CancellationToken cancellationToken)
    {
        RandomTeamMemberResponse randomTeamMember = await _randomTeamMemberApiService.ImportAsync();

        var teamMember = Domain.Entities.TeamMember.Create(
            randomTeamMember.Name,
            randomTeamMember.Email,
            randomTeamMember.PhoneNumber, 
            _dateTimeProvider.UtcNow);

        _teamMemberRepository.Add(teamMember);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return teamMember.Id;
    }

}