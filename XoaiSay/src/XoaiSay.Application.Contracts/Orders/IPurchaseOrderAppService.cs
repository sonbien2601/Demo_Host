using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace XoaiSay.Orders;

public interface IPurchaseOrderAppService : IApplicationService
{
    Task<PurchaseOrderDto> CreateAsync(CreatePurchaseOrderDto input);

    Task<PagedResultDto<PurchaseOrderDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task UpdateStatusAsync(Guid id, UpdateOrderStatusDto input);
}
