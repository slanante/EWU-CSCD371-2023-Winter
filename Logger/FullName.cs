namespace Logger;
// Chose to define FullName as a value type so we can modify copies
// of the original object ensuring that the original FullName remains
// unchanged
public record struct FullName(string FirstName, string LastName, string? MiddleName = null)
{
    // Chose to make the type immutable since we are only performing 
    // operations on copies of the object
    public string FirstName { get; } = FirstName??throw new ArgumentNullException(nameof(FirstName));
    public string LastName { get; } = LastName??throw new ArgumentNullException(nameof(LastName));
    public string? MiddleName { get; } = MiddleName;

    public string Name => $"{FirstName} {MiddleName} {LastName}";
    
}
