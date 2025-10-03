using System;
using Volo.Abp.Application.Dtos;

namespace XoaiSay.Orders;

public class PurchaseOrderDto : AuditedEntityDto<Guid>
{
    public Guid ProductId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
}
