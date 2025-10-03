using System;
using System.ComponentModel.DataAnnotations;

namespace XoaiSay.Orders;

public class CreatePurchaseOrderDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [StringLength(PurchaseOrderConsts.MaxCustomerNameLength)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(PurchaseOrderConsts.MaxEmailLength)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(PurchaseOrderConsts.MaxPhoneNumberLength)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(PurchaseOrderConsts.MaxAddressLength)]
    public string ShippingAddress { get; set; } = string.Empty;

    [StringLength(PurchaseOrderConsts.MaxNotesLength)]
    public string? Notes { get; set; }

    [Range(1, 1000)]
    public int Quantity { get; set; }
}
