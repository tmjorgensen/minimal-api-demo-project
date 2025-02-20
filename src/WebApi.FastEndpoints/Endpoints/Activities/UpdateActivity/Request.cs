namespace WebApi.Endpoints.Activities.UpdateActivity;

public record UpdateActivityRequestDto(Guid? ProjectId, string? Title);
