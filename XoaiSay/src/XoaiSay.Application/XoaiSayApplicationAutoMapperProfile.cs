using AutoMapper;
using XoaiSay.Orders;
using XoaiSay.Products;

namespace XoaiSay;

public class XoaiSayApplicationAutoMapperProfile : Profile
{
    public XoaiSayApplicationAutoMapperProfile()
    {
        CreateMap<DriedMangoProduct, ProductDto>();
        CreateMap<PurchaseOrder, PurchaseOrderDto>();
    }
}
