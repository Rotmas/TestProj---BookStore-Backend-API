using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.EF;

public class EFBookRepository : IBookRepository
{
    private readonly BookStoreDbContext _context;

    public EFBookRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Book> GetById(string id)
    {
        var entity = await _context.Books.FindAsync(id);
        return entity == null ? throw new NotFoundException("NotFoundException") : MapToModel(entity);
    }

    public async Task<IEnumerable<Book>> GetByTitle(string title)
    {
        var entities = await _context.Books
            .Where(b => b.Title.Contains(title))
            .ToListAsync();
        return entities.Select(MapToModel);
    }

    public async Task<IEnumerable<Book>> GetByPublicationDate(DateTime publicationDate)
    {
        var entities = await _context.Books
            .Where(b => b.PublicationDate.Date == publicationDate.Date)
            .ToListAsync();
        return entities.Select(MapToModel);
    }

    public async Task Save(Book book)
    {
        var entity = await _context.Books.FindAsync(book.Id);
        if (entity == null)
        {
            _context.Books.Add(MapToEntity(book));
        }
        else
        {
            _context.Entry(entity).CurrentValues.SetValues(MapToEntity(book));
        }
        await _context.SaveChangesAsync();
    }

    private Book MapToModel(BookEntity entity)
    {
        return new Book
        {
            Id = entity.Id,
            Title = entity.Title,
            PublicationDate = entity.PublicationDate
        };
    }

    private static BookEntity MapToEntity(Book model)
    {
        return new BookEntity
        {
            Id = model.Id,
            Title = model.Title,
            PublicationDate = model.PublicationDate
        };
    }
}
