using Domain.Enums;

namespace Application.Features.TeamMember;

public class TeamMemberDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public TeamMemberStatus Status { get; set; }
    public DateTime CreatedAtDateTime { get; set; }
}
