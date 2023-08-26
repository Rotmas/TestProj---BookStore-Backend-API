using Model;

namespace Data.Memory;

public class InMemoryBookRepository : IBookRepository
{
    readonly Dictionary<string, Book> Books = new();

    public Task<Book> GetById(string id)
    {
        if (!Books.TryGetValue(id, out var book)) throw new NotFoundException($"{nameof(Book)} with id '{id}' not found");

        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetByPublicationDate(DateTime publicationDate)
        => Task.FromResult(Books.Values.Where(book => book.PublicationDate.IsSameDay(publicationDate)));

    public Task<IEnumerable<Book>> GetByTitle(string title)
        => Task.FromResult(Books.Values.Where(book => book.Title == title));

    public Task Save(Book book)
    {
        Books[book.Id] = book;
        return Task.CompletedTask;
    }
}
