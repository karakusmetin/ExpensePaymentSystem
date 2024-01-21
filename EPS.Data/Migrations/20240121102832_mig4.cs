using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPS.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenRequestCount",
                schema: "dbo",
                table: "Employee",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "employee",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<int>(
                name: "ExpensRequestCount",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Admin",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivityDate",
                schema: "dbo",
                table: "Admin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PasswordRetryCount",
                schema: "dbo",
                table: "Admin",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpensRequestCount",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LastActivityDate",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "PasswordRetryCount",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "dbo",
                table: "Employee",
                newName: "ExpenRequestCount");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "employee");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Admin",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
