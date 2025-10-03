using XoaiSay.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace XoaiSay.Permissions;

public class XoaiSayPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(XoaiSayPermissions.GroupName);

        var productPermission = myGroup.AddPermission(XoaiSayPermissions.Products.Default, L("Permission:Products"));
        productPermission.AddChild(XoaiSayPermissions.Products.Create, L("Permission:Products.Create"));
        productPermission.AddChild(XoaiSayPermissions.Products.Edit, L("Permission:Products.Edit"));
        productPermission.AddChild(XoaiSayPermissions.Products.Delete, L("Permission:Products.Delete"));

        var orderPermission = myGroup.AddPermission(XoaiSayPermissions.PurchaseOrders.Default, L("Permission:PurchaseOrders"));
        orderPermission.AddChild(XoaiSayPermissions.PurchaseOrders.UpdateStatus, L("Permission:PurchaseOrders.UpdateStatus"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<XoaiSayResource>(name);
    }
}
