using Model;
using System.Net.Http.Json;

namespace BookStore.Tests;

public class Orders
{
    readonly HttpClient Client;

    public Orders(HttpClient client)
        => Client = client;

    public async Task<Order> GetById(string id)
        => await Client.GetFromJsonAsync<Order>($"/api/orders/id/{id}") 
        ?? throw new Exception("Unexpected");

    public async Task<Order[]> GetBySubmissionDate(DateTime submissionDate)
        => await Client.GetFromJsonAsync<Order[]>($"/api/orders/submissionDate/{submissionDate:yyyy-MM-dd}") 
        ?? throw new Exception("Unexpected");

    public async Task Save(Order order) =>
        _ = await Client.PostAsJsonAsync($"/api/orders/save/{order}", order)
        ?? throw new Exception("Unexpected");

}