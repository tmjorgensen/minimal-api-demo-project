using Application.Models;
using Domain.Models;

namespace Infrastructure.Services;
public class RepositoryBase<T> where T : Entity
{
    protected List<T> Items { get; } = [];

    protected RepositoryBase() { }

    public Task CreateAsync(T item, CancellationToken ct)
    {
        if (Items.Any(x => x.Id == item.Id))
            throw new Exception("Primary key violation!");

        Items.Add(item with { });

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item != null) Items.Remove(item);

        return Task.CompletedTask;
    }

    public Task<T> GetAsync(Guid id, CancellationToken ct)
    {
        var item = Items.First(x => x.Id == id);

        return Task.FromResult(item with { });
    }

    public Task UpdateAsync(T item, CancellationToken ct)
    {
        var existingItem = Items.First(x => x.Id == item.Id);

        Items.Remove(existingItem);

        Items.Add(item with { });

        return Task.CompletedTask;
    }

    protected Task<SearchResponse<T>> SearchAsync(IQueryable<T> query, SearchRequest request, CancellationToken ct)
    {
        var isDescending = request.IsDescending ?? false;
        var rowCount = request.RowCount ?? 10;

        if (request.PreviousId != null)
            query = isDescending
                ? query.Where(x => x.Id < request.PreviousId.Value)
                : query.Where(x => x.Id > request.PreviousId.Value);

        query = isDescending
            ? query.OrderByDescending(x => x.Id)
            : query.OrderBy(x => x.Id);

        query = query.Take(rowCount);

        IEnumerable<T> result = query
            .ToArray()
            .Select(x => x with { })
            .ToArray();

        var response = new SearchResponse<T>(result, Items.Count);

        return Task.FromResult(response);
    }
}
