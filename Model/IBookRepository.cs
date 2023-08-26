namespace Model;

public interface IBookRepository
{
    Task<Book> GetById(string id);
    Task<IEnumerable<Book>> GetByTitle(string title);
    Task<IEnumerable<Book>> GetByPublicationDate(DateTime publicationDate);
    Task Save(Book book);
}
