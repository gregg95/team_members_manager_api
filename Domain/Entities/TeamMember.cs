using Domain.Enums;
using Domain.Primitives;
using InvalidOperationException = Domain.Errors.InvalidOperationException;

namespace Domain.Entities;

public sealed class TeamMember : Entity
{
    private TeamMember(Guid id,
        string name,
        string email,
        string phoneNumber,
        TeamMemberStatus status,
        DateTime createdAtDateTime) : base(id)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = status;
        CreatedAtDateTime = createdAtDateTime;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public TeamMemberStatus Status { get; private set; }
    public DateTime CreatedAtDateTime { get; private set; }

    public static TeamMember Create(
        string name,
        string email,
        string phoneNumber, 
        DateTime createdAtDateTime)
    {
        return new TeamMember(
            Guid.NewGuid(),
            name,
            email,
            phoneNumber,
            TeamMemberStatus.Active,    
            createdAtDateTime);
    }

    public void Activate()
    {
        if (Status == TeamMemberStatus.Active)
        {
            throw new InvalidOperationException("Team member is already active");
        }

        Status = TeamMemberStatus.Active;
    }

    public void Block()
    {
        if (Status == TeamMemberStatus.Blocked)
        {
            throw new InvalidOperationException("Team member is already blocked");
        }

        Status = TeamMemberStatus.Blocked;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}