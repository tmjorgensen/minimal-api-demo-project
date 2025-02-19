namespace Application.Models;
public record SearchRequest(Guid? PreviousId, bool? IsDescending, int? RowCount);
public record SearchResponse<T>(IEnumerable<T> Items, int TotalCount);
