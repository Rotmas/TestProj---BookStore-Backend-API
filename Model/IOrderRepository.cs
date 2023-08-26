namespace Model;

public interface IOrderRepository
{
    Task<Order> GetById(string id);
    Task<IEnumerable<Order>> GetBySubmissionDate(DateTime submissionDate);
    Task Save(Order order);
}