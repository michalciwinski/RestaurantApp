using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class TOrderAndModelTablesFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOrderPosition_TOrder_TOrderId",
                table: "TOrderPosition");

            migrationBuilder.AddForeignKey(
                name: "FK_TOrderPosition_TOrder_TOrderId",
                table: "TOrderPosition",
                column: "TOrderId",
                principalTable: "TOrder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOrderPosition_TOrder_TOrderId",
                table: "TOrderPosition");

            migrationBuilder.AddForeignKey(
                name: "FK_TOrderPosition_TOrder_TOrderId",
                table: "TOrderPosition",
                column: "TOrderId",
                principalTable: "TOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
