using Model;

namespace Logic;

public class Books
{
    readonly IBookRepository Repository;

    public Books(IBookRepository books)
        => Repository = books;

    public Task<Book> GetById(string id)
        => Repository.GetById(id);

    public Task<IEnumerable<Book>> GetByTitle(string title) 
        => Repository.GetByTitle(title);

    public Task<IEnumerable<Book>> GetByPublicationDate(DateTime publicationDate)
        => Repository.GetByPublicationDate(publicationDate);
}
