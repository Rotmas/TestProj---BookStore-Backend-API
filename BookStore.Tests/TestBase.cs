using Data.EF;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Tests;

public class TestBase : IntegrationTestBase<Program>
{
    public TestBase ()
    {
        var context = Server<BookStoreDbContext>();
        context.Books.RemoveRange(context.Books);
        context.Orders.RemoveRange(context.Orders);
        context.OrderBooks.RemoveRange(context.OrderBooks);
        context.SaveChanges();
    }


    protected override IServiceCollection ClientServices(IServiceCollection services) => base.ClientServices(services)
        .AddSingleton<Api>()
        .AddSingleton<Books>()
        .AddSingleton<Orders>();

    protected Api Api
        => Client<Api>();
}