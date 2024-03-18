using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarambeeCommerce.Persistence.Migrations
{
    public partial class AddDateCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProductBaskets",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Basket",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProductBaskets");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Basket");
        }
    }
}
