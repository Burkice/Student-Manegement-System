using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudenProject.Migrations
{
    /// <inheritdoc />
    public partial class dfdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_Installment_InstallmentId",
                table: "PaymentHistory");

            migrationBuilder.AlterColumn<int>(
                name: "InstallmentId",
                table: "PaymentHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_Installment_InstallmentId",
                table: "PaymentHistory",
                column: "InstallmentId",
                principalTable: "Installment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_Installment_InstallmentId",
                table: "PaymentHistory");

            migrationBuilder.AlterColumn<int>(
                name: "InstallmentId",
                table: "PaymentHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_Installment_InstallmentId",
                table: "PaymentHistory",
                column: "InstallmentId",
                principalTable: "Installment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
