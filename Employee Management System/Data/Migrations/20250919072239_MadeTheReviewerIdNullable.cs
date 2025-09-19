using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class MadeTheReviewerIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Drop foreign keys first
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Employees_EmployeeId",
                table: "PerformanceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Employees_ReviewerId",
                table: "PerformanceReviews");

            // Alter ReviewerId to be nullable BEFORE re-adding FK
            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                table: "PerformanceReviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Re-add foreign keys with correct delete behavior
            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Employees_EmployeeId",
                table: "PerformanceReviews",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Employees_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            // Drop updated foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Employees_EmployeeId",
                table: "PerformanceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Employees_ReviewerId",
                table: "PerformanceReviews");

            // Alter ReviewerId back to non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                table: "PerformanceReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Re-add original foreign keys (assuming they were Restrict or NoAction)
            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Employees_EmployeeId",
                table: "PerformanceReviews",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // or NoAction

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Employees_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // or NoAction



        }
    }
}
