using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class updatingDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferFundHistory_Accounts_RecieverAccountId",
                table: "TransferFundHistory");

            migrationBuilder.DropTable(
                name: "TransactionRegistre");

            migrationBuilder.DropTable(
                name: "TransactionsRegistres");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TransferFundHistory");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TransactionsHistory");

            migrationBuilder.RenameColumn(
                name: "RecieverAccountId",
                table: "TransferFundHistory",
                newName: "RecipientAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferFundHistory_RecieverAccountId",
                table: "TransferFundHistory",
                newName: "IX_TransferFundHistory_RecipientAccountId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TransferFundHistory",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TransactionsHistory",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LoginRegistre",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferFundHistory_Accounts_RecipientAccountId",
                table: "TransferFundHistory",
                column: "RecipientAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferFundHistory_Accounts_RecipientAccountId",
                table: "TransferFundHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TransferFundHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TransactionsHistory");

            migrationBuilder.RenameColumn(
                name: "RecipientAccountId",
                table: "TransferFundHistory",
                newName: "RecieverAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferFundHistory_RecipientAccountId",
                table: "TransferFundHistory",
                newName: "IX_TransferFundHistory_RecieverAccountId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "TransferFundHistory",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "TransactionsHistory",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "LoginRegistre",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "TransactionRegistre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverClientId = table.Column<int>(type: "int", nullable: false),
                    SenderClientId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRegistre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRegistre_Clients_ReceiverClientId",
                        column: x => x.ReceiverClientId,
                        principalTable: "Clients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionRegistre_Clients_SenderClientId",
                        column: x => x.SenderClientId,
                        principalTable: "Clients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionsRegistres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientAccountId = table.Column<int>(type: "int", nullable: false),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsRegistres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionsRegistres_Accounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionsRegistres_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRegistre_ReceiverClientId",
                table: "TransactionRegistre",
                column: "ReceiverClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRegistre_SenderClientId",
                table: "TransactionRegistre",
                column: "SenderClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsRegistres_ClientAccountId",
                table: "TransactionsRegistres",
                column: "ClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsRegistres_EmployeeAccountId",
                table: "TransactionsRegistres",
                column: "EmployeeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferFundHistory_Accounts_RecieverAccountId",
                table: "TransferFundHistory",
                column: "RecieverAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
