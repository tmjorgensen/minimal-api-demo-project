using Application.Models;
using Application.UseCases.Projects;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Requests;
using WebApi.DTO.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("projects")]
[Authorize]
public class ProjectsController(
    ICreateProjectUseCase createProjectUseCase,
    IDeleteProjectUseCase deleteProjectUseCase,
    IGetProjectUseCase getProjectUseCase,
    ISearchProjectsUseCase searchProjectsUseCase,
    IUpdateProjectUseCase updateProjectUseCase
    ) : ControllerBase
{
    private const string GetResource = "GetProject";

    private readonly ICreateProjectUseCase _createProjectUseCase = createProjectUseCase;
    private readonly IDeleteProjectUseCase _deleteProjectUseCase = deleteProjectUseCase;
    private readonly IGetProjectUseCase _getProjectUseCase = getProjectUseCase;
    private readonly ISearchProjectsUseCase _searchProjectsUseCase = searchProjectsUseCase;
    private readonly IUpdateProjectUseCase _updateProjectUseCase = updateProjectUseCase;

    [HttpGet]
    public async Task<IActionResult> SearchAsync(
        [FromQuery] string? title,
        [FromQuery] Guid? previousId,
        [FromQuery] bool? desc,
        [FromQuery] int? take,
        CancellationToken ct)
    {
        var result = await _searchProjectsUseCase.InvokeAsync(
            new(title, previousId, desc, take),
            ct
            ).ConfigureAwait(false);

        var response = new SearchResponse<Project>(result.Items, result.TotalCount);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProjectRequestDto req, CancellationToken ct)
    {
        var newId = await _createProjectUseCase.InvokeAsync(new(req.Title), ct).ConfigureAwait(false);

        return CreatedAtRoute(GetResource, new { id = newId }, new CreatedDto(newId));
    }

    [HttpGet]
    [Route("{id}", Name = GetResource)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var item = await _getProjectUseCase.InvokeAsync(id, ct).ConfigureAwait(false);

        return Ok(item);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProjectRequestDto req, CancellationToken ct)
    {
        await _updateProjectUseCase.InvokeAsync(new(id, req.Title), ct).ConfigureAwait(false);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
    {
        await _deleteProjectUseCase.InvokeAsync(id, ct).ConfigureAwait(false);

        return NoContent();
    }
}
