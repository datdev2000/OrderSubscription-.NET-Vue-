namespace OrderSubscription.Api.DTOs;
using System.ComponentModel.DataAnnotations;

public class CreateOrderDto
{
    [Required(ErrorMessage = "TotalAmount is required")]
    [Range(1, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
    public decimal TotalAmount { get; set; }
}
