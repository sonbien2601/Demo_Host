using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XoaiSay.Orders;
using XoaiSay.Permissions;
using XoaiSay.Products;

namespace XoaiSay.Orders;

public class PurchaseOrderAppService : ApplicationService, IPurchaseOrderAppService
{
    private readonly IRepository<PurchaseOrder, Guid> _orderRepository;
    private readonly IRepository<DriedMangoProduct, Guid> _productRepository;

    public PurchaseOrderAppService(
        IRepository<PurchaseOrder, Guid> orderRepository,
        IRepository<DriedMangoProduct, Guid> productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    [AllowAnonymous]
    public async Task<PurchaseOrderDto> CreateAsync(CreatePurchaseOrderDto input)
    {
        var product = await _productRepository.GetAsync(input.ProductId);

        if (product.StockQuantity < input.Quantity)
        {
            throw new BusinessException(XoaiSayDomainErrorCodes.InsufficientStock)
                .WithData("Product", product.Name)
                .WithData("Requested", input.Quantity)
                .WithData("Available", product.StockQuantity);
        }

        product.SetStock(product.StockQuantity - input.Quantity);
        await _productRepository.UpdateAsync(product, autoSave: true);

        var totalPrice = product.Price * input.Quantity;

        var order = new PurchaseOrder(
            GuidGenerator.Create(),
            product.Id,
            input.CustomerName,
            input.CustomerEmail,
            input.PhoneNumber,
            input.ShippingAddress,
            input.Quantity,
            totalPrice,
            input.Notes ?? string.Empty);

        await _orderRepository.InsertAsync(order, autoSave: true);

        return ObjectMapper.Map<PurchaseOrder, PurchaseOrderDto>(order);
    }

    [Authorize(XoaiSayPermissions.PurchaseOrders.Default)]
    public async Task<PagedResultDto<PurchaseOrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _orderRepository.GetQueryableAsync();

        var sorting = input.Sorting.IsNullOrWhiteSpace()
            ? nameof(PurchaseOrder.CreationTime) + " DESC"
            : input.Sorting;

        queryable = queryable.OrderBy(sorting);

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        var items = await AsyncExecuter.ToListAsync(
            queryable
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));

        var dtos = items
            .Select(order => ObjectMapper.Map<PurchaseOrder, PurchaseOrderDto>(order))
            .ToList();

        return new PagedResultDto<PurchaseOrderDto>(totalCount, dtos);
    }

    [Authorize(XoaiSayPermissions.PurchaseOrders.UpdateStatus)]
    public async Task UpdateStatusAsync(Guid id, UpdateOrderStatusDto input)
    {
        var order = await _orderRepository.GetAsync(id);
        order.UpdateStatus(input.Status);
        await _orderRepository.UpdateAsync(order, autoSave: true);
    }
}
