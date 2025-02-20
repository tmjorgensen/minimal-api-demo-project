using Domain.Models;

namespace WebApi.Endpoints.Activities.SearchActivity;

public record ActivitySearchResponseDto(IEnumerable<Activity> Items, int Count);
