using Application.Models;
using Application.UseCases.Activities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Requests;
using WebApi.DTO.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("activities")]
[Authorize]
public class ActivitiesController : ControllerBase
{
    private const string GetResource = "GetActivity";

    [HttpGet]
    public async Task<IActionResult> SearchAsync(
        [FromQuery] Guid? projectId,
        [FromQuery] string? title,
        [FromQuery] Guid? previousId,
        [FromQuery] bool? desc,
        [FromQuery] int? take,
        SearchActivitiesUseCase useCase,
        CancellationToken ct)
    {
        var result = await useCase.InvokeAsync(
            new(projectId, title, previousId, desc, take),
            ct
            ).ConfigureAwait(false);

        var response = new SearchResponse<Activity>(result.Items, result.TotalCount);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateActivityRequestDto req, 
        ICreateActivityUseCase useCase,
        CancellationToken ct)
    {
        var newId = await useCase.InvokeAsync(new(req.ProjectId, req.Title), ct).ConfigureAwait(false);

        return CreatedAtRoute(GetResource, new { id = newId }, new CreatedDto(newId));
    }

    [HttpGet]
    [Route("{id}", Name = GetResource)]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid id, 
        IGetActivityUseCase useCase,
        CancellationToken ct)
    {
        var item = await useCase.InvokeAsync(id, ct).ConfigureAwait(false);

        return Ok(item);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(
        Guid id, 
        UpdateActivityRequestDto req, 
        IUpdateActivityUseCase useCase,
        CancellationToken ct)
    {
        await useCase.InvokeAsync(new(id, req.ProjectId, req.Title), ct).ConfigureAwait(false);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(
        Guid id, 
        IDeleteActivityUseCase useCase,
        CancellationToken ct)
    {
        await useCase.InvokeAsync(id, ct).ConfigureAwait(false);

        return NoContent();
    }
}
