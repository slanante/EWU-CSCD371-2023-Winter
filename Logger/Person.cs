namespace Logger;

public record Person : IEntity
{
    public Guid Id { get; init; }

    public FullName FullName { get; init; }

    // Implemented explicitly to avoid conflicts betweeen implementing classes
    string IEntity.Name => FullName.Name;
}