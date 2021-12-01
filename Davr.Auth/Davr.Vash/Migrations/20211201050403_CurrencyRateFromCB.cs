using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Davr.Vash.Migrations
{
    public partial class CurrencyRateFromCB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactions_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransactions");

            migrationBuilder.DropTable(
                name: "CurrencyRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeTransactions_CurrencyRateId",
                table: "ExchangeTransactions");

            migrationBuilder.DropColumn(
                name: "CurrencyRateId",
                table: "ExchangeTransactions");

            migrationBuilder.AddColumn<string>(
                name: "Ccy",
                table: "Currencies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ccy",
                table: "Currencies");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyRateId",
                table: "ExchangeTransactions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransactions_CurrencyRateId",
                table: "ExchangeTransactions",
                column: "CurrencyRateId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_CurrencyId",
                table: "CurrencyRates",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactions_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransactions",
                column: "CurrencyRateId",
                principalTable: "CurrencyRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
