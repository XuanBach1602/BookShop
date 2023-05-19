using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OderHeader",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "OderHeader",
                table: "OrderDetail",
                newName: "OderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OderHeader",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OderId",
                table: "OrderDetail",
                column: "OderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OderId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "OderId",
                table: "OrderDetail",
                newName: "OderHeader");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OderId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OderHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeaders_OderHeader",
                table: "OrderDetail",
                column: "OderHeader",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
