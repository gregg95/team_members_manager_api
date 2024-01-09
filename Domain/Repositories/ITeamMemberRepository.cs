using Domain.Entities;

namespace Domain.Repositories;

public interface ITeamMemberRepository
{
    IEnumerable<TeamMember> GetAll();
    TeamMember GetById(Guid id);
    void Add(TeamMember teamMember);
}
