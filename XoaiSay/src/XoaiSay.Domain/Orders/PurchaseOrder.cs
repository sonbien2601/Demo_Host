using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace XoaiSay.Orders;

public class PurchaseOrder : AuditedAggregateRoot<Guid>
{
    public Guid ProductId { get; private set; }
    public string CustomerName { get; private set; }
    public string CustomerEmail { get; private set; }
    public string PhoneNumber { get; private set; }
    public string ShippingAddress { get; private set; }
    public string Notes { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public OrderStatus Status { get; private set; }

    private PurchaseOrder()
    {
        CustomerName = string.Empty;
        CustomerEmail = string.Empty;
        PhoneNumber = string.Empty;
        ShippingAddress = string.Empty;
        Notes = string.Empty;
    }

    public PurchaseOrder(
        Guid id,
        Guid productId,
        string customerName,
        string customerEmail,
        string phoneNumber,
        string shippingAddress,
        int quantity,
        decimal totalPrice,
        string notes)
        : base(id)
    {
        SetProduct(productId);
        SetCustomer(customerName, customerEmail, phoneNumber, shippingAddress);
        SetQuantity(quantity);
        SetTotalPrice(totalPrice);
        SetNotes(notes);
        Status = OrderStatus.Pending;
    }

    public void SetProduct(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Product id cannot be empty.", nameof(productId));
        }

        ProductId = productId;
    }

    public void SetCustomer(
        string customerName,
        string customerEmail,
        string phoneNumber,
        string shippingAddress)
    {
        CustomerName = Check.NotNullOrWhiteSpace(customerName, nameof(customerName), PurchaseOrderConsts.MaxCustomerNameLength);
        CustomerEmail = Check.Length(
            Check.NotNullOrWhiteSpace(customerEmail, nameof(customerEmail), PurchaseOrderConsts.MaxEmailLength),
            nameof(customerEmail),
            PurchaseOrderConsts.MaxEmailLength);
        PhoneNumber = Check.Length(
            Check.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber), PurchaseOrderConsts.MaxPhoneNumberLength),
            nameof(phoneNumber),
            PurchaseOrderConsts.MaxPhoneNumberLength);
        ShippingAddress = Check.Length(
            Check.NotNullOrWhiteSpace(shippingAddress, nameof(shippingAddress), PurchaseOrderConsts.MaxAddressLength),
            nameof(shippingAddress),
            PurchaseOrderConsts.MaxAddressLength);
    }

    public void SetNotes(string notes)
    {
        Notes = notes.IsNullOrWhiteSpace()
            ? string.Empty
            : Check.Length(notes, nameof(notes), PurchaseOrderConsts.MaxNotesLength);
    }

    public void SetQuantity(int quantity)
    {
        Quantity = Check.Range(quantity, nameof(quantity), 1, 1000);
    }

    public void SetTotalPrice(decimal totalPrice)
    {
        TotalPrice = Check.Range(totalPrice, nameof(totalPrice), 0m, decimal.MaxValue);
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }
}
