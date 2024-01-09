using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Presistence.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TeamMemberRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TeamMember teamMember)
    {
        _dbContext.TeamMembers.Add(teamMember);
    }

    public IEnumerable<TeamMember> GetAll()
    {
        return _dbContext.TeamMembers.AsEnumerable();
    }

    public TeamMember GetById(Guid id)
    {
        return _dbContext.TeamMembers.FirstOrDefault(x => x.Id == id);
    }
}
