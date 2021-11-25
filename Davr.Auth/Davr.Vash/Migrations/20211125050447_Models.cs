using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Davr.Vash.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weather");

            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SurName = table.Column<string>(type: "text", nullable: true),
                    MiddleSurName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BirthPlace = table.Column<string>(type: "text", nullable: true),
                    DocumentSeries = table.Column<string>(type: "text", nullable: true),
                    DocumentNumber = table.Column<string>(type: "text", nullable: true),
                    DocumentAuthority = table.Column<string>(type: "text", nullable: true),
                    DocumentIssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Registration = table.Column<string>(type: "text", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    CitizenId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCash = table.Column<bool>(type: "boolean", nullable: false),
                    CardNumber = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsBuying = table.Column<bool>(type: "boolean", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    SourceOfOrigin = table.Column<string>(type: "text", nullable: true),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyRateId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeTransaction_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeTransaction_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeTransaction_CurrencyRates_CurrencyRateId",
                        column: x => x.CurrencyRateId,
                        principalTable: "CurrencyRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CitizenId",
                table: "Clients",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DocumentTypeId",
                table: "Clients",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_CurrencyId",
                table: "CurrencyRates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransaction_ClientId",
                table: "ExchangeTransaction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransaction_CurrencyId",
                table: "ExchangeTransaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransaction_CurrencyRateId",
                table: "ExchangeTransaction",
                column: "CurrencyRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeTransaction");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "CurrencyRates");

            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.CreateTable(
                name: "weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    TemperatureC = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather", x => x.Id);
                });
        }
    }
}
