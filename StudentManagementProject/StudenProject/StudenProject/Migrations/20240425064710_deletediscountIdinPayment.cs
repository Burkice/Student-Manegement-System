using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudenProject.Migrations
{
    /// <inheritdoc />
    public partial class deletediscountIdinPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Discount_DiscountId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DiscountId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DiscountId",
                table: "Payments",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Discount_DiscountId",
                table: "Payments",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id");
        }
    }
}
