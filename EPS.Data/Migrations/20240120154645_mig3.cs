using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPS.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "dbo",
                table: "ExpenditureDemand",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "ExpenditureDemand",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "dbo",
                table: "ExpenditureDemand");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "ExpenditureDemand");
        }
    }
}
