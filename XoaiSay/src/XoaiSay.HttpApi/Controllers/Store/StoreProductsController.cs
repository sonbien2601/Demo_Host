using System;
using System.Threading.Tasks;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using XoaiSay.Products;

namespace XoaiSay.Controllers.Store;

[ApiController]
[Route("api/store/products")]
public class StoreProductsController : XoaiSayController
{
    private readonly IProductAppService _productAppService;

    public StoreProductsController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    [AllowAnonymous]
    public Task<PagedResultDto<ProductDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        if (input.MaxResultCount <= 0)
        {
            input.MaxResultCount = 50;
        }

        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(ProductDto.Name);
        }

        return _productAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public Task<ProductDto> GetAsync(Guid id)
    {
        return _productAppService.GetAsync(id);
    }
}
