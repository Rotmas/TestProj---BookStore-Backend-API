using Logic;
using Microsoft.AspNetCore.Mvc;
using Model;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    readonly Orders Orders;

    public OrdersController(Orders orders)
        => Orders = orders;

    [SwaggerResponse(200, type: typeof(Order))]
    [SwaggerResponse(404)]
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var order = await Orders.GetById(id);
        return new OkObjectResult(order);
    }

    [SwaggerResponse(200, type: typeof(Order[]))]
    [HttpGet("submissionDate/{submissionDate}")]
    public async Task<IActionResult> GetBySubmissionDate(DateTime submissionDate)
    {
        var order = await Orders.GetBySubmissionDate(submissionDate);
        return new OkObjectResult(order);
    }

    [SwaggerResponse(200, type: typeof(Order[]))]
    [HttpPost("save/{save}")]
    public async Task<IActionResult> Save(Order order)
    {
        await Orders.Save(order);
        return Ok();
    }
}