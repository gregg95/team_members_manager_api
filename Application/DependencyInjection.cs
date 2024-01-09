using Application.Features.TeamMember.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static Application.Features.TeamMember.Commands.CreateTeamMemberCommandHandler;
using static Application.Features.TeamMember.Commands.UpdateTeamMemberCommandHandler;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IValidator<CreateTeamMemberCommand>, CreateTeamMemberCommandValidator>();
        services.AddScoped<IValidator<UpdateTeamMemberCommand>, UpdateTeamMemberCommandValidator>();
    }
}
