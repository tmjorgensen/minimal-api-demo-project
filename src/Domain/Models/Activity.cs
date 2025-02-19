namespace Domain.Models;
public record Activity(Guid Id, Guid ProjectId, string Title) : Entity(Id);
