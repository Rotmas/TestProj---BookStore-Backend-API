using Model;

namespace Data.Memory;

public class InMemoryOrderRepository : IOrderRepository
{
    readonly Dictionary<string, Order> Orders = new();

    public Task<Order> GetById(string id)
    {
        if (!Orders.TryGetValue(id, out var order)) throw new NotFoundException($"{nameof(Order)} with id '{id}' not found");

        return Task.FromResult(order);
    }

    public Task<IEnumerable<Order>> GetBySubmissionDate(DateTime submissionDate)
        => Task.FromResult(Orders.Values.Where(order => order.SubmissionDate.IsSameDay(submissionDate)));

    public Task Save(Order order)
    {
        Orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
