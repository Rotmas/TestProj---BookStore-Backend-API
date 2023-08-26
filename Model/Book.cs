namespace Model;

public record Book
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public DateTime PublicationDate { get; init; }
}
