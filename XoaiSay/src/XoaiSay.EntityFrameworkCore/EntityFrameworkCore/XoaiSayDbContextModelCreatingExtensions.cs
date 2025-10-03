using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using XoaiSay.Orders;
using XoaiSay.Products;

namespace XoaiSay.EntityFrameworkCore;

public static class XoaiSayDbContextModelCreatingExtensions
{
    public static void ConfigureXoaiSay(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<DriedMangoProduct>(b =>
        {
            b.ToTable(XoaiSayConsts.DbTablePrefix + "DriedMangoProducts", XoaiSayConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(DriedMangoProductConsts.MaxNameLength);
            b.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(DriedMangoProductConsts.MaxDescriptionLength);
            b.Property(x => x.ImageUrl)
                .HasMaxLength(DriedMangoProductConsts.MaxImageUrlLength);
            b.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        });

        builder.Entity<PurchaseOrder>(b =>
        {
            b.ToTable(XoaiSayConsts.DbTablePrefix + "PurchaseOrders", XoaiSayConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.CustomerName)
                .IsRequired()
                .HasMaxLength(PurchaseOrderConsts.MaxCustomerNameLength);
            b.Property(x => x.CustomerEmail)
                .IsRequired()
                .HasMaxLength(PurchaseOrderConsts.MaxEmailLength);
            b.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(PurchaseOrderConsts.MaxPhoneNumberLength);
            b.Property(x => x.ShippingAddress)
                .IsRequired()
                .HasMaxLength(PurchaseOrderConsts.MaxAddressLength);
            b.Property(x => x.Notes)
                .HasMaxLength(PurchaseOrderConsts.MaxNotesLength);
            b.Property(x => x.TotalPrice)
                .HasColumnType("decimal(18,2)");
            b.HasIndex(x => x.ProductId);
            b.HasIndex(x => x.Status);
        });
    }
}
