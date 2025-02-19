namespace WebApi.DTO.Requests;
public record CreateActivityRequestDto(Guid ProjectId, string Title);
public record UpdateActivityRequestDto(Guid? ProjectId, string? Title);
