using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarambeeCommerce.Persistence.Migrations
{
    public partial class RemovePriceDiscountBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Basket");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Basket",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Basket",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Basket",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Basket",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
