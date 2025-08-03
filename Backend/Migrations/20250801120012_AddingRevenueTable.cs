using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class AddingRevenueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginRegistre");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAccountId",
                table: "TransactionsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BankRevenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    RelatedTransactionId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankRevenues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginRegister_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_EmployeeAccountId",
                table: "TransactionsHistory",
                column: "EmployeeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginRegister_EmployeeId",
                table: "LoginRegister",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_EmployeeAccount_EmployeeAccountId",
                table: "TransactionsHistory",
                column: "EmployeeAccountId",
                principalTable: "EmployeeAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_EmployeeAccount_EmployeeAccountId",
                table: "TransactionsHistory");

            migrationBuilder.DropTable(
                name: "BankRevenues");

            migrationBuilder.DropTable(
                name: "LoginRegister");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_EmployeeAccountId",
                table: "TransactionsHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeAccountId",
                table: "TransactionsHistory");

            migrationBuilder.CreateTable(
                name: "LoginRegistre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRegistre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginRegistre_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginRegistre_EmployeeId",
                table: "LoginRegistre",
                column: "EmployeeId");
        }
    }
}
