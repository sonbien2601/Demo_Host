using System;
using Volo.Abp.Application.Dtos;

namespace XoaiSay.Products;

public class ProductDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int WeightGrams { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
}
