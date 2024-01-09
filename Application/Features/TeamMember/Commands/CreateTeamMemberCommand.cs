using Domain.Repositories;
using FluentValidation;
using Infrastructure.Presistence;
using Infrastructure.Shared.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using System.Text.RegularExpressions;

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
    private readonly IValidator<CreateTeamMemberCommand> _validator;

    public CreateTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        IValidator<CreateTeamMemberCommand> validator)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateTeamMemberCommand command, CancellationToken cancellationToken)
    {
        ValidateCommand(command);

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

    private void ValidateCommand(CreateTeamMemberCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    public class CreateTeamMemberCommandValidator : AbstractValidator<CreateTeamMemberCommand>
    {
        public CreateTeamMemberCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 200).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(command => command.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.");
        }
    }
}