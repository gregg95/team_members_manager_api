using Domain.Errors;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Presistence;
using MediatR;
using PhoneNumbers;
using System.Text.RegularExpressions;
using System.Threading;

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
    private readonly IValidator<UpdateTeamMemberCommand> _validator;

    public UpdateTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository,
        ApplicationDbContext dbContext,
        IValidator<UpdateTeamMemberCommand> validator)
    {
        _teamMemberRepository = teamMemberRepository;
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Unit> Handle(UpdateTeamMemberCommand command, CancellationToken cancellationToken)
    {
        ValidateCommand(command);

        Domain.Entities.TeamMember teamMember = _teamMemberRepository.GetById(command.Id);

        if (teamMember is null)
        {
            throw new NotFoundException($"Team member with id {command.Id} does not exists");
        }

        teamMember.UpdateName(command.Name);
        teamMember.UpdateEmail(command.Email);
        teamMember.UpdatePhoneNumber(command.PhoneNumber);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private void ValidateCommand(UpdateTeamMemberCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    public class UpdateTeamMemberCommandValidator : AbstractValidator<UpdateTeamMemberCommand>
    {
        public UpdateTeamMemberCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 200).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(command => command.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.");
        }
        
        //Example phone number validation
        private bool BeValidPhoneNumber(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var numberProto = phoneNumberUtil.Parse(phoneNumber, null);
                return phoneNumberUtil.IsValidNumber(numberProto);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}