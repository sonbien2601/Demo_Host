using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace XoaiSay.Products;

public class DriedMangoDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<DriedMangoProduct, Guid> _productRepository;

    public DriedMangoDataSeedContributor(IRepository<DriedMangoProduct, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _productRepository.GetCountAsync() > 0)
        {
            return;
        }

        var products = new List<DriedMangoProduct>
        {
            new(
                id: Guid.Parse("64416619-4b16-4d5c-9c14-18b066cf3e1c"),
                name: "Xoài sấy mật ong",
                description: "Xoài cát chín tự nhiên, sấy lạnh giữ trọn hương vị, phủ mật ong hoa cà phê tạo vị ngọt thanh.",
                price: 95000m,
                weightGrams: 150,
                imageUrl: "https://images.unsplash.com/photo-1502746781977-ea598727eca5?auto=format&fit=crop&w=800&q=80",
                stockQuantity: 120),
            new(
                id: Guid.Parse("b73fa724-7d45-4fd9-b6c2-7cd7ecac0f0b"),
                name: "Xoài sấy vị chanh dây",
                description: "Kết hợp giữa xoài chín và nước cốt chanh dây tươi, mang đến vị chua ngọt hài hòa và thơm mát.",
                price: 99000m,
                weightGrams: 150,
                imageUrl: "https://images.unsplash.com/photo-1528832995084-5d0f4b64cd98?auto=format&fit=crop&w=800&q=80",
                stockQuantity: 90),
            new(
                id: Guid.Parse("f1f2250a-d3f7-4acd-91a3-6a6828e78452"),
                name: "Xoài sấy nguyên vị",
                description: "Giữ nguyên độ tươi ngon và màu sắc tự nhiên, không thêm đường, không chất bảo quản.",
                price: 89000m,
                weightGrams: 150,
                imageUrl: "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=800&q=80",
                stockQuantity: 150)
        };

        await _productRepository.InsertManyAsync(products, autoSave: true);
    }
}
