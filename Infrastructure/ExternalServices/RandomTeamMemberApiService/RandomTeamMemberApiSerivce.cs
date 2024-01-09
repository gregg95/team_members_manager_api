using Newtonsoft.Json.Linq;

namespace Infrastructure.ExternalServices.RandomTeamMemberApiService;

internal sealed class RandomTeamMemberApiSerivce : IRandomTeamMemberApiService
{
    public async Task<RandomTeamMemberResponse> ImportAsync()
    {
        using HttpClient httpClient = new();

        HttpResponseMessage response = await httpClient.GetAsync("https://randomuser.me/api/");

        string responseBody = await response.Content.ReadAsStringAsync();

        var result = (JObject)JObject.Parse(responseBody)?["results"]?[0];

        if (result == null)
        {
            return null;
        }

        RandomTeamMemberResponse randomTeamMember = new()
        {
            Name = $"{result["name"]["first"].Value<string>()} {result["name"]["last"].Value<string>()}",
            Email = result["email"].Value<string>(),
            PhoneNumber = result["phone"].Value<string>(),
        };

        return randomTeamMember;
    }
}
