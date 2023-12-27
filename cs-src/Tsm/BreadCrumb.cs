namespace Tsm;

public record class BreadCrumb
{
    public required string State { get; init; }
    public string? Representation { get; init; }
}