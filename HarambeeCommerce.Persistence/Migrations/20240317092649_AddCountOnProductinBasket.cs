using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarambeeCommerce.Persistence.Migrations
{
    public partial class AddCountOnProductinBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ProductBasket",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ProductBasket");
        }
    }
}
