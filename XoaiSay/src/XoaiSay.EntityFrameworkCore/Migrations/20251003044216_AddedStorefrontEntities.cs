using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XoaiSay.Migrations
{
    /// <inheritdoc />
    public partial class AddedStorefrontEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDriedMangoProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    WeightGrams = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDriedMangoProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CustomerEmail = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ShippingAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Notes = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrders_ProductId",
                table: "AppPurchaseOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrders_Status",
                table: "AppPurchaseOrders",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDriedMangoProducts");

            migrationBuilder.DropTable(
                name: "AppPurchaseOrders");
        }
    }
}
