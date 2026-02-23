using Microsoft.AspNetCore.Mvc;
using OrderSubscription.Api.DTOs;
using OrderSubscription.Api.Models;
using OrderSubscription.Api.Mappings;
using OrderSubscription.Api.Responses;

namespace OrderSubscription.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    // Fake in-memory storage (training purpose)
    private static readonly List<Order> Orders = new();

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = Orders.Where(o => o.DeletedAt == null);
         return Ok(ApiResponse<object>.Ok(orders.ToDtoList()));
    }

    /// <summary>
    /// Get order by id
    /// </summary>
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id && o.DeletedAt == null);
        if (order == null)
            return NotFound();

        return Ok(ApiResponse<object>.Ok(order.ToDto(), "Order updated successfully"));
    }

    /// <summary>
    /// Create new order
    /// </summary>
    [HttpPost]
    public IActionResult Create(CreateOrderDto dto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = $"ORD-{DateTime.UtcNow.Ticks}",
            TotalAmount = dto.TotalAmount,
            Status = "Pending"
        };

        Orders.Add(order);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order.ToDto());
    }

    /// <summary>
    /// Update new order
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateOrderDto dto)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id && o.DeletedAt == null);
        if (order == null)
            return NotFound();

        order.TotalAmount = dto.TotalAmount;
        order.Status = dto.Status;
        order.UpdatedAt = DateTime.UtcNow;

        return Ok(order.ToDto());
    }

    /// <summary>
    /// Delete order
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id && o.DeletedAt == null);
        if (order == null)
            return NotFound();

        order.DeletedAt = DateTime.UtcNow;
        return NoContent();
    }

    // [HttpGet("crash")]
    // public IActionResult Crash()
    // {
    //     throw new Exception("Something went wrong. Please try again later.");
    // }
}
