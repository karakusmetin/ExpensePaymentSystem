using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPS.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseRequest_Expense_ExpenseId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseRequest_ExpenseId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "EvaluationComment",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "dbo",
                table: "ExpenseRequest",
                newName: "ExpenseCategoryId");

            migrationBuilder.RenameColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseRequest",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsApproved",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "IsApproved",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRequest_EmployeeId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRequest_ExpenseCategoryId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseRequest_Employee_EmployeeId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "EmployeeId",
                principalSchema: "dbo",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseRequest_ExpenseCategory_ExpenseCategoryId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "ExpenseCategoryId",
                principalSchema: "dbo",
                principalTable: "ExpenseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseRequest_Employee_EmployeeId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseRequest_ExpenseCategory_ExpenseCategoryId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseRequest_EmployeeId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseRequest_ExpenseCategoryId",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "ExpenseCategoryId",
                schema: "dbo",
                table: "ExpenseRequest",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                schema: "dbo",
                table: "ExpenseRequest",
                newName: "InsertUserId");

            migrationBuilder.AddColumn<string>(
                name: "EvaluationComment",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IsApproved",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRequest_ExpenseId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseRequest_Expense_ExpenseId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "ExpenseId",
                principalSchema: "dbo",
                principalTable: "Expense",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
