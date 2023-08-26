using Logic;
using Microsoft.AspNetCore.Mvc;
using Model;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BooksController : ControllerBase
{
    readonly Books Books;

    public BooksController(Books books)
        => Books = books;

    [SwaggerResponse(200, type: typeof(Book))]
    [SwaggerResponse(404)]
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var book = await Books.GetById(id);
        return new OkObjectResult(book);
    }

    [SwaggerResponse(200, type: typeof(Book[]))]
    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var book = await Books.GetByTitle(title);
        return new OkObjectResult(book);
    }

    [SwaggerResponse(200, type: typeof(Book[]))]
    [HttpGet("publicationDate/{publicationDate}")]
    public async Task<IActionResult> GetByPublicationDate(DateTime publicationDate)
    {
        var book = await Books.GetByPublicationDate(publicationDate);
        return new OkObjectResult(book);
    }
}
