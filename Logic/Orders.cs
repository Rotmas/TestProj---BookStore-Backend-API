using Model;

namespace Logic;

public class Orders
{
    readonly IOrderRepository Repository;

    public Orders(IOrderRepository orders)
        => Repository = orders;

    public Task<Order> GetById(string id)
        => Repository.GetById(id);

    public Task<IEnumerable<Order>> GetBySubmissionDate(DateTime submissionDate)
        => Repository.GetBySubmissionDate(submissionDate);

    public Task Save(Order order)
        => Repository.Save(order);
}
