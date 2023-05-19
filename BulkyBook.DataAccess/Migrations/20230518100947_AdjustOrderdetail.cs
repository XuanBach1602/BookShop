using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdjustOrderdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OderId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OderId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "OderId",
                table: "OrderDetail",
                newName: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "OrderDetail",
                newName: "OderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OderId",
                table: "OrderDetail",
                column: "OderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OderId",
                table: "OrderDetail",
                column: "OderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
