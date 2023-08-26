namespace Data.EF;

public class OrderBookEntity
{
    public string OrderId { get; set; }
    public OrderEntity Order { get; set; }

    public string BookId { get; set; }
    public BookEntity Book { get; set; }
}
