using System.ComponentModel.DataAnnotations;

namespace XoaiSay.Orders;

public class UpdateOrderStatusDto
{
    [Required]
    public OrderStatus Status { get; set; }
}
