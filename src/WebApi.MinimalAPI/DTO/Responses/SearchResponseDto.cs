namespace WebApi.DTO.Responses;
public record SearchResponseDto<T>(IEnumerable<T> Items, int Count);
