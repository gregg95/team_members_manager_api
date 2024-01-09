namespace Infrastructure.ExternalServices.RandomTeamMemberApiService;

public interface IRandomTeamMemberApiService
{
    Task<RandomTeamMemberResponse> ImportAsync();
}
