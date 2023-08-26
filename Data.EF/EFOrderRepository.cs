using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF;

public class EFOrderRepository : IOrderRepository
{
    private readonly BookStoreDbContext _context;

    public EFOrderRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetById(string id)
    {
        var entity = await _context.Orders.Include(o => o.OrderBooks).SingleOrDefaultAsync(o => o.Id == id);
        return entity == null ? throw new NotFoundException("NotFoundException") : MapToModel(entity);
    }

    public async Task<IEnumerable<Order>> GetBySubmissionDate(DateTime submissionDate)
    {
        var entities = await _context.Orders
            .Where(o => o.SubmissionDate.Date == submissionDate.Date)
            .Include(o => o.OrderBooks)
            .ToListAsync();
        return entities.Select(MapToModel);
    }

    public async Task Save(Order order)
    {
        var entity = await _context.Orders.FindAsync(order.Id);
        if (entity == null)
        {
            _context.Orders.Add(MapToEntity(order));
        }
        else
        {
            _context.Entry(entity).CurrentValues.SetValues(MapToEntity(order));
        }
        await _context.SaveChangesAsync();
    }

    private Order MapToModel(OrderEntity entity)
    {
        return new Order
        {
            Id = entity.Id,
            SubmissionDate = entity.SubmissionDate,
            Books = entity.OrderBooks.Select(ob => new Book
            {
                Id = ob.Book.Id,
                Title = ob.Book.Title,
                PublicationDate = ob.Book.PublicationDate
            })
        };
    }

    private static OrderEntity MapToEntity(Order model)
    {
        return new OrderEntity
        {
            Id = model.Id,
            SubmissionDate = model.SubmissionDate,
            OrderBooks = model.Books.Select(b => new OrderBookEntity
            {
                OrderId = model.Id,
                BookId = b.Id
            }).ToList()
        };
    }
}
