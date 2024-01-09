using Domain.Repositories;
using Infrastructure.Presistence;
using Infrastructure.Shared.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TeamMember.Commands;

public record CreateTeamMemberCommand(
    string Name,
    string Email,
    string PhoneNumber, 
    IFormFile PhotoFile) : IRequest<Guid>;

public class CreateTeamMemberCommandHandler : IRequestHandler<CreateTeamMemberCommand, Guid>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(CreateTeamMemberCommand command, CancellationToken cancellationToken)
    {
        var teamMember = Domain.Entities.TeamMember.Create(
            command.Name,
            command.Email,
            command.PhoneNumber,
            _dateTimeProvider.UtcNow);

        if (command.PhotoFile is not null && command.PhotoFile.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", $"{teamMember.Id}.png");

            if (!Directory.Exists(new FileInfo(path).Directory.FullName))
            {
                Directory.CreateDirectory(path);
            }

            using var stream = new FileStream(path, FileMode.Create);
            await command.PhotoFile.CopyToAsync(stream, cancellationToken);
        }

        _teamMemberRepository.Add(teamMember);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return teamMember.Id;
    }

}