using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XoaiSay.Permissions;
using XoaiSay.Products;

namespace XoaiSay.Products;

[Authorize(XoaiSayPermissions.Products.Default)]
public class ProductAppService : CrudAppService<DriedMangoProduct, ProductDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductDto>, IProductAppService
{
    public ProductAppService(IRepository<DriedMangoProduct, Guid> repository)
        : base(repository)
    {
        GetPolicyName = XoaiSayPermissions.Products.Default;
        GetListPolicyName = XoaiSayPermissions.Products.Default;
        CreatePolicyName = XoaiSayPermissions.Products.Create;
        UpdatePolicyName = XoaiSayPermissions.Products.Edit;
        DeletePolicyName = XoaiSayPermissions.Products.Delete;
    }

    [AllowAnonymous]
    public override Task<ProductDto> GetAsync(Guid id)
    {
        return base.GetAsync(id);
    }

    [AllowAnonymous]
    public override Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return base.GetListAsync(input);
    }

    protected override Task MapToEntityAsync(CreateUpdateProductDto updateInput, DriedMangoProduct entity)
    {
        entity.SetName(updateInput.Name);
        entity.SetDescription(updateInput.Description);
        entity.SetImageUrl(updateInput.ImageUrl ?? string.Empty);
        entity.SetPrice(updateInput.Price);
        entity.SetWeight(updateInput.WeightGrams);
        entity.SetStock(updateInput.StockQuantity);
        return Task.CompletedTask;
    }

    protected override DriedMangoProduct MapToEntity(CreateUpdateProductDto createInput)
    {
        return new DriedMangoProduct(
            GuidGenerator.Create(),
            createInput.Name,
            createInput.Description,
            createInput.Price,
            createInput.WeightGrams,
            createInput.ImageUrl ?? string.Empty,
            createInput.StockQuantity);
    }
}
