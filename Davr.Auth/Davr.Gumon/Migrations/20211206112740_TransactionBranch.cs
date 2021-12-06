using Microsoft.EntityFrameworkCore.Migrations;

namespace Davr.Gumon.Migrations
{
    public partial class TransactionBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspicious_Currencies_CurrencyId",
                table: "TransactionSuspicious");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspicious_OperationCriterias_OperationCriteriaId",
                table: "TransactionSuspicious");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspicious_TransactionStatuses_TransactionStatus~",
                table: "TransactionSuspicious");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspicious_Users_UserId",
                table: "TransactionSuspicious");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionSuspicious",
                table: "TransactionSuspicious");

            migrationBuilder.RenameTable(
                name: "TransactionSuspicious",
                newName: "TransactionSuspiciouses");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspicious_UserId",
                table: "TransactionSuspiciouses",
                newName: "IX_TransactionSuspiciouses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspicious_TransactionStatusId",
                table: "TransactionSuspiciouses",
                newName: "IX_TransactionSuspiciouses_TransactionStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspicious_OperationCriteriaId",
                table: "TransactionSuspiciouses",
                newName: "IX_TransactionSuspiciouses_OperationCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspicious_CurrencyId",
                table: "TransactionSuspiciouses",
                newName: "IX_TransactionSuspiciouses_CurrencyId");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "TransactionSuspiciouses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionSuspiciouses",
                table: "TransactionSuspiciouses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSuspiciouses_BranchId",
                table: "TransactionSuspiciouses",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspiciouses_Branches_BranchId",
                table: "TransactionSuspiciouses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspiciouses_Currencies_CurrencyId",
                table: "TransactionSuspiciouses",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspiciouses_OperationCriterias_OperationCriteri~",
                table: "TransactionSuspiciouses",
                column: "OperationCriteriaId",
                principalTable: "OperationCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspiciouses_TransactionStatuses_TransactionStat~",
                table: "TransactionSuspiciouses",
                column: "TransactionStatusId",
                principalTable: "TransactionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspiciouses_Users_UserId",
                table: "TransactionSuspiciouses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspiciouses_Branches_BranchId",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspiciouses_Currencies_CurrencyId",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspiciouses_OperationCriterias_OperationCriteri~",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspiciouses_TransactionStatuses_TransactionStat~",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSuspiciouses_Users_UserId",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionSuspiciouses",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropIndex(
                name: "IX_TransactionSuspiciouses_BranchId",
                table: "TransactionSuspiciouses");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "TransactionSuspiciouses");

            migrationBuilder.RenameTable(
                name: "TransactionSuspiciouses",
                newName: "TransactionSuspicious");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspiciouses_UserId",
                table: "TransactionSuspicious",
                newName: "IX_TransactionSuspicious_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspiciouses_TransactionStatusId",
                table: "TransactionSuspicious",
                newName: "IX_TransactionSuspicious_TransactionStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspiciouses_OperationCriteriaId",
                table: "TransactionSuspicious",
                newName: "IX_TransactionSuspicious_OperationCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionSuspiciouses_CurrencyId",
                table: "TransactionSuspicious",
                newName: "IX_TransactionSuspicious_CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionSuspicious",
                table: "TransactionSuspicious",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspicious_Currencies_CurrencyId",
                table: "TransactionSuspicious",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspicious_OperationCriterias_OperationCriteriaId",
                table: "TransactionSuspicious",
                column: "OperationCriteriaId",
                principalTable: "OperationCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspicious_TransactionStatuses_TransactionStatus~",
                table: "TransactionSuspicious",
                column: "TransactionStatusId",
                principalTable: "TransactionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSuspicious_Users_UserId",
                table: "TransactionSuspicious",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
