namespace BookStore.Tests;

public class Api
{
    public readonly Books Books;
    public readonly Orders Orders;

    public Api(Books books, Orders orders)
        => (Books, Orders)
        = (books, orders);
}
