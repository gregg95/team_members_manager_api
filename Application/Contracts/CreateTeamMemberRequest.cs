using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

public class CreateTeamMemberRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile PhotoFile { get; set; }
}
