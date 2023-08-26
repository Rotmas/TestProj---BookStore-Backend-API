namespace Data.EF;
public class OrderEntity
{
    public string Id { get; set; } = string.Empty;
    public DateTime SubmissionDate { get; set; }
    public ICollection<OrderBookEntity> OrderBooks { get; set; } = new List<OrderBookEntity>();
}
