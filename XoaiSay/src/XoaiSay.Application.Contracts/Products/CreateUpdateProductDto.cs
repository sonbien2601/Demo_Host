using System.ComponentModel.DataAnnotations;

namespace XoaiSay.Products;

public class CreateUpdateProductDto
{
    [Required]
    [StringLength(DriedMangoProductConsts.MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(DriedMangoProductConsts.MaxDescriptionLength)]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "1000000")]
    public decimal Price { get; set; }

    [Range(1, 100000)]
    public int WeightGrams { get; set; }

    [StringLength(DriedMangoProductConsts.MaxImageUrlLength)]
    public string? ImageUrl { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}
