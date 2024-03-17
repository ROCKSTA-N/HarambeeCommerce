using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarambeeCommerce.Persistence.Migrations
{
    public partial class RemoveIdProductinBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBasket_Basket_BasketId",
                table: "ProductBasket");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBasket_Product_ProductId",
                table: "ProductBasket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBasket",
                table: "ProductBasket");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductBasket");

            migrationBuilder.RenameTable(
                name: "ProductBasket",
                newName: "ProductBaskets");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBasket_ProductId",
                table: "ProductBaskets",
                newName: "IX_ProductBaskets_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBaskets",
                table: "ProductBaskets",
                columns: new[] { "BasketId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBaskets_Basket_BasketId",
                table: "ProductBaskets",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBaskets_Product_ProductId",
                table: "ProductBaskets",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBaskets_Basket_BasketId",
                table: "ProductBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBaskets_Product_ProductId",
                table: "ProductBaskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBaskets",
                table: "ProductBaskets");

            migrationBuilder.RenameTable(
                name: "ProductBaskets",
                newName: "ProductBasket");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBaskets_ProductId",
                table: "ProductBasket",
                newName: "IX_ProductBasket_ProductId");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ProductBasket",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBasket",
                table: "ProductBasket",
                columns: new[] { "BasketId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBasket_Basket_BasketId",
                table: "ProductBasket",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBasket_Product_ProductId",
                table: "ProductBasket",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
