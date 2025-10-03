using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using XoaiSay.Orders;
using XoaiSay.Permissions;

namespace XoaiSay.Controllers.Store;

[ApiController]
[Route("api/store/orders")]
public class StoreOrdersController : XoaiSayController
{
    private readonly IPurchaseOrderAppService _purchaseOrderAppService;

    public StoreOrdersController(IPurchaseOrderAppService purchaseOrderAppService)
    {
        _purchaseOrderAppService = purchaseOrderAppService;
    }

    [HttpPost]
    [AllowAnonymous]
    public Task<PurchaseOrderDto> CreateAsync([FromBody] CreatePurchaseOrderDto input)
    {
        return _purchaseOrderAppService.CreateAsync(input);
    }

    [HttpGet]
    [Authorize(XoaiSayPermissions.PurchaseOrders.Default)]
    public Task<PagedResultDto<PurchaseOrderDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        return _purchaseOrderAppService.GetListAsync(input);
    }

    [HttpPut("{id}/status")]
    [Authorize(XoaiSayPermissions.PurchaseOrders.UpdateStatus)]
    public Task UpdateStatusAsync(Guid id, [FromBody] UpdateOrderStatusDto input)
    {
        return _purchaseOrderAppService.UpdateStatusAsync(id, input);
    }
}
