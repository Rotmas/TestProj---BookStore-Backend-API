using Model;
using System.Net.Http.Json;

namespace BookStore.Tests;

public class Books
{
    readonly HttpClient Client;

    public Books(HttpClient client)
        => Client = client;

    public async Task<Book> GetById(string id)
        => await Client.GetFromJsonAsync<Book>($"/api/books/id/{id}") ?? throw new Exception("Unexpected");

    public async Task<Book[]> GetByTitle(string title)
        => await Client.GetFromJsonAsync<Book[]>($"/api/books/title/{title}") ?? throw new Exception("Unexpected");

    public async Task<Book[]> GetByPublicationDate(DateTime publicationDate)
        => await Client.GetFromJsonAsync<Book[]>($"/api/books/publicationDate/{publicationDate:yyyy-MM-dd}") ?? throw new Exception("Unexpected");
}
