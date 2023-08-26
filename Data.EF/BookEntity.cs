namespace Data.EF;

public class BookEntity
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public ICollection<OrderBookEntity> OrderBooks { get; set; } = new List<OrderBookEntity>();
}