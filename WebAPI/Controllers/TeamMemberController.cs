using Application.Contracts;
using Application.Features.TeamMember;
using Application.Features.TeamMember.Commands;
using Application.Features.TeamMember.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Controller for managing team members.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TeamMemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamMemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all team members.
    /// </summary>
    /// <returns>List of team member data transfer objects.</returns>
    /// <response code="200">Returns the list of team members.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        List<TeamMemberDto> result = await _mediator.Send(new GetAllTeamMembersQuery());
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a team member by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team member.</param>
    /// <returns>The team member data transfer object.</returns>
    /// <response code="200">Returns the requested team member.</response>
    /// <response code="404">If the team member is not found.</response>
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        TeamMemberDto result = await _mediator.Send(new GetTeamMemberByIdQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Creates a new team member.
    /// </summary>
    /// <param name="request">The team member creation request.</param>
    /// <returns>The unique identifier of the created team member.</returns>
    /// <response code="201">Returns the unique identifier of the created team member.</response>
    /// <response code="400">If the request is invalid.</response>
    [HttpPost]
    public async Task<IActionResult> CreateTeamMemberAsync([FromForm] CreateTeamMemberRequest request)
    {
        Guid teamMemberId = await _mediator.Send(
            new CreateTeamMemberCommand(
                request.Name,
                request.Email,
                request.PhoneNumber,
                request.PhotoFile));

        return Created("", teamMemberId);
    }

    /// <summary>
    /// Updates an existing team member.
    /// </summary>
    /// <param name="id">The unique identifier of the team member.</param>
    /// <param name="request">The team member update request.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the update is successful.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If the team member is not found.</response>
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateTeamMemberAsync([FromRoute] Guid id, [FromBody] UpdateTeamMemberRequest request)
    {
        UpdateTeamMemberCommand command = new(id, request.Name, request.Email, request.PhoneNumber);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Activates a team member.
    /// </summary>
    /// <param name="id">The unique identifier of the team member.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the activation is successful.</response>
    /// <response code="404">If the team member is not found.</response>
    /// <response code="409">If the activation operation is invalid (already activated).</response>

    [HttpPatch]
    [Route("{id:guid}/activate")]
    public async Task<IActionResult> ActivateTeamMemberAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new ActivateTeamMemberCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Blocks a team member.
    /// </summary>
    /// <param name="id">The unique identifier of the team member.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the block is successful.</response>
    /// <response code="404">If the team member is not found.</response>
    /// <response code="409">If the block operation is invalid (already blocked).</response>
    [HttpPatch]
    [Route("{id:guid}/block")]
    public async Task<IActionResult> BlockTeamMemberAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new BlockTeamMemberCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Imports team member data using randomuser.me.
    /// </summary>
    /// <returns>The unique identifier of the imported team member.</returns>
    /// <response code="201">Returns the unique identifier of the imported team member.</response>
    /// <response code="504">If there is a timeout while reaching randomuser.me.</response>

    [HttpPost]
    [Route("import")]
    public async Task<IActionResult> ImportTeamMemberAsync()
    {
        Guid teamMemberId = await _mediator.Send(new ImportTeamMemberCommand());
        return Created("", teamMemberId);
    }
}
