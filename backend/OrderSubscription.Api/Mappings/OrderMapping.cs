using OrderSubscription.Api.DTOs;
using OrderSubscription.Api.Models;

namespace OrderSubscription.Api.Mappings;

public static class OrderMapping
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }

    public static IEnumerable<OrderDto> ToDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(o => o.ToDto());
    }
}
