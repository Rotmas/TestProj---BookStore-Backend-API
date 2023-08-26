using Model;
using System.Net;

namespace BookStore.Tests;

public class BookStoreTests : TestBase
{
    //GetBookById

    [Fact]
    public async Task Returns404WhenGettingNonExistingBookById()
    {
        //Arrange

        //Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => Api.Books.GetById("non-existing"));
        //Assert
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task ReturnsExistingBookById()
    {
        //Arrange
        var saved = new Book()
        {
            Id = "1",
            PublicationDate = DateTime.Now,
            Title = "Alice in Wonderland"
        };
        await Server<IBookRepository>().Save(saved);
        //Act
        var book = await Api.Books.GetById("1");
        //Assert
        Assert.Equal(saved, book);
    }

    //GetBooksByTitle

    [Fact]
    public async Task ReturnsEmptyArrayWhenGettingNonExistingBookByTitle()
    {
        //Arrange

        //Act
        var books = await Api.Books.GetByTitle("Alice in Wonderland");
        //Assert
        Assert.Empty(books);
    }

    [Fact]
    public async Task ReturnsExistingBookByTitle()
    {
        //Arrange
        var saved = new Book()
        {
            Id = "1",
            PublicationDate = DateTime.Now,
            Title = "Alice in Wonderland"
        };
        await Server<IBookRepository>().Save(saved);
        //Act
        var books = await Api.Books.GetByTitle("Alice in Wonderland");
        //Assert
        Assert.Single(books);
        Assert.Equal(saved, books.Single());
    }

    //GetBooksByPublicationDate

    [Fact]
    public async Task ReturnsEmptyArrayWhenGettingNonExistingBookByPublicationDate()
    {
        //Arrange
        var datetime = DateTime.Now;
        //Act
        var books = await Api.Books.GetByPublicationDate(datetime);
        //Assert
        Assert.Empty(books);
    }

    [Fact]
    public async Task ReturnsExistingBooksByPublicationDate()
    {
        //Arrange
        var publicationDate = DateTime.Now;
        var saved = new Book()
        {
            Id = "1",
            PublicationDate = publicationDate,
            Title = "Alice in Wonderland"
        };
        await Server<IBookRepository>().Save(saved);
        //Act
        var book = await Api.Books.GetByPublicationDate(publicationDate);
        //Assert
        Assert.Single(book);
        Assert.Equal(saved, book.Single());
    }

    //GetOrderById

    [Fact]
    public async Task Returns404WhenGettingNonExistingOrderById()
    {
        //Arrange

        //Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => Api.Orders.GetById("non-existing"));
        //Assert
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task ReturnsExistingOrderById()
    {
        //Arrange
        var book = new Book()
        {
            Id = "1",
            PublicationDate = DateTime.Now,
            Title = "Alice in Wonderland"
        };
        await Server<IBookRepository>().Save(book);

        var savedOrder = new Order()
        {
            Id = "1",
            SubmissionDate = DateTime.Now,
            Books = new[] { book }
        };
        await Server<IOrderRepository>().Save(savedOrder);
        //Act
        var order = await Api.Orders.GetById("1");
        //Assert
        Assert.Equal(savedOrder.Id, order.Id);
        Assert.Equal(savedOrder.SubmissionDate, order.SubmissionDate);
        Assert.Single(order.Books);
        Assert.Equal(book, order.Books.Single());
    }

    //GetOrderBySubmissionDate

    [Fact]
    public async Task ReturnsEmptyArrayWhenGettingNonExistingBookBySubmissionDate()
    {
        //Arrange
        var submissionDate = DateTime.Now;
        //Act
        var orders = await Api.Orders.GetBySubmissionDate(submissionDate);
        //Assert
        Assert.Empty(orders);
    }

    [Fact]
    public async Task ReturnsExistingOrdersBySubmissionDate()
    {
        //Arrange
        var time = DateTime.Now;

        var book = new Book()
        {
            Id = "1",
            PublicationDate = time,
            Title = "Alice in Wonderland"
        };
        await Server<IBookRepository>().Save(book);

        var savedOrder = new Order()
        {
            Id = "1",
            SubmissionDate = time,
            Books = new[] { book }
        };
        await Server<IOrderRepository>().Save(savedOrder);
        //Act
        var order = await Api.Orders.GetBySubmissionDate(time);
        //Assert
        Assert.Single(order);
    }

    //SaveOrder

    [Fact]
    public async Task SaveOrder()
    {
        //Arrange
        var time = DateTime.Now;

        var book = new Book()
        {
            Id = "1",
            PublicationDate = time,
            Title = "Alice in Wonderland"
        };
        await Server<IBookRepository>().Save(book);

        var savedOrder = new Order()
        {
            Id = "1",
            SubmissionDate = time,
            Books = new[] { book }
        };
        //Act
        await Api.Orders.Save(savedOrder);
        var order = await Api.Orders.GetBySubmissionDate(time);
        //Assert
        Assert.Single(order);
    }
}
