using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace XoaiSay.Products;

public class DriedMangoProduct : AuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int WeightGrams { get; private set; }
    public string ImageUrl { get; private set; }
    public int StockQuantity { get; private set; }

    private DriedMangoProduct()
    {
        Name = string.Empty;
        Description = string.Empty;
        ImageUrl = string.Empty;
    }

    public DriedMangoProduct(
        Guid id,
        string name,
        string description,
        decimal price,
        int weightGrams,
        string imageUrl,
        int stockQuantity)
        : base(id)
    {
        SetName(name);
        SetDescription(description);
        SetImageUrl(imageUrl);
        SetPrice(price);
        SetWeight(weightGrams);
        SetStock(stockQuantity);
    }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DriedMangoProductConsts.MaxNameLength);
    }

    public void SetDescription(string description)
    {
        Description = Check.Length(
            Check.NotNull(description, nameof(description)),
            nameof(description),
            DriedMangoProductConsts.MaxDescriptionLength);
    }

    public void SetPrice(decimal price)
    {
        Price = Check.Range(price, nameof(price), 0m, decimal.MaxValue);
    }

    public void SetWeight(int grams)
    {
        WeightGrams = Check.Range(grams, nameof(grams), 1, 100000);
    }

    public void SetImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl.IsNullOrWhiteSpace()
            ? string.Empty
            : Check.Length(imageUrl, nameof(imageUrl), DriedMangoProductConsts.MaxImageUrlLength);
    }

    public void SetStock(int stock)
    {
        StockQuantity = Check.Range(stock, nameof(stock), 0, int.MaxValue);
    }
}
