using System.ComponentModel.DataAnnotations;
namespace OrderSubscription.Api.DTOs;

public class UpdateOrderDto
{
    [Required]
    [Range(1, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;
}
