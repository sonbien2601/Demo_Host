namespace XoaiSay.Permissions;

public static class XoaiSayPermissions
{
    public const string GroupName = "XoaiSay";

    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class PurchaseOrders
    {
        public const string Default = GroupName + ".PurchaseOrders";
        public const string UpdateStatus = Default + ".UpdateStatus";
    }
}
