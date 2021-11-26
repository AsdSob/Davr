using Microsoft.EntityFrameworkCore.Migrations;

namespace Davr.Vash.Migrations
{
    public partial class TransactionAdd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_Branches_BranchId",
                table: "ExchangeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_Clients_ClientId",
                table: "ExchangeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_Currencies_CurrencyId",
                table: "ExchangeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransaction_Users_UserId",
                table: "ExchangeTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeTransaction",
                table: "ExchangeTransaction");

            migrationBuilder.RenameTable(
                name: "ExchangeTransaction",
                newName: "ExchangeTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransaction_UserId",
                table: "ExchangeTransactions",
                newName: "IX_ExchangeTransactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransaction_CurrencyRateId",
                table: "ExchangeTransactions",
                newName: "IX_ExchangeTransactions_CurrencyRateId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransaction_CurrencyId",
                table: "ExchangeTransactions",
                newName: "IX_ExchangeTransactions_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransaction_ClientId",
                table: "ExchangeTransactions",
                newName: "IX_ExchangeTransactions_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransaction_BranchId",
                table: "ExchangeTransactions",
                newName: "IX_ExchangeTransactions_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeTransactions",
                table: "ExchangeTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactions_Branches_BranchId",
                table: "ExchangeTransactions",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactions_Clients_ClientId",
                table: "ExchangeTransactions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactions_Currencies_CurrencyId",
                table: "ExchangeTransactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactions_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransactions",
                column: "CurrencyRateId",
                principalTable: "CurrencyRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactions_Users_UserId",
                table: "ExchangeTransactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactions_Branches_BranchId",
                table: "ExchangeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactions_Clients_ClientId",
                table: "ExchangeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactions_Currencies_CurrencyId",
                table: "ExchangeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactions_CurrencyRates_CurrencyRateId",
                table: "ExchangeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactions_Users_UserId",
                table: "ExchangeTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeTransactions",
                table: "ExchangeTransactions");

            migrationBuilder.RenameTable(
                name: "ExchangeTransactions",
                newName: "ExchangeTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactions_UserId",
                table: "ExchangeTransaction",
                newName: "IX_ExchangeTransaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactions_CurrencyRateId",
                table: "ExchangeTransaction",
                newName: "IX_ExchangeTransaction_CurrencyRateId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactions_CurrencyId",
                table: "ExchangeTransaction",
                newName: "IX_ExchangeTransaction_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactions_ClientId",
                table: "ExchangeTransaction",
                newName: "IX_ExchangeTransaction_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactions_BranchId",
                table: "ExchangeTransaction",
                newName: "IX_ExchangeTransaction_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeTransaction",
                table: "ExchangeTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_Branches_BranchId",
                table: "ExchangeTransaction",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_Clients_ClientId",
                table: "ExchangeTransaction",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransaction_Currencies_CurrencyId",
                table: "ExchangeTransaction",
                column: "CurrencyId",
                principalTable: "Currencies",
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
    }
}
