using Microsoft.EntityFrameworkCore.Migrations;

namespace Davr.Vash.Migrations
{
    public partial class CurrencyRateФвв : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Currencies",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Currencies");
        }
    }
}
