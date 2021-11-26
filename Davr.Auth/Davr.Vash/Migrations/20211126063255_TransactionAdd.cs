using Microsoft.EntityFrameworkCore.Migrations;

namespace Davr.Vash.Migrations
{
    public partial class TransactionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransaction");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyRateId",
                table: "ExchangeTransaction",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "ExchangeTransaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CurrencyRate",
                table: "ExchangeTransaction",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransaction_BranchId",
                table: "ExchangeTransaction",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransaction_UserId",
                table: "ExchangeTransaction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_Branches_BranchId",
                table: "ExchangeTransaction",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransaction",
                column: "CurrencyRateId",
                principalTable: "CurrencyRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_Users_UserId",
                table: "ExchangeTransaction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_Branches_BranchId",
                table: "ExchangeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_Users_UserId",
                table: "ExchangeTransaction");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeTransaction_BranchId",
                table: "ExchangeTransaction");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeTransaction_UserId",
                table: "ExchangeTransaction");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ExchangeTransaction");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "ExchangeTransaction");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyRateId",
                table: "ExchangeTransaction",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransaction",
                column: "CurrencyRateId",
                principalTable: "CurrencyRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
