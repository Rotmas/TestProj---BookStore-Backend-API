namespace Model;

public class Order
{
    public string Id { get; init; } = string.Empty;
    public DateTime SubmissionDate { get; init; }
    public IEnumerable<Book> Books { get; init; } = Enumerable.Empty<Book>();
}
