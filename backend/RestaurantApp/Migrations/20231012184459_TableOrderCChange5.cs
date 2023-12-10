using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class TableOrderCChange5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOrder_TUser_TUserId",
                table: "TOrder");

            migrationBuilder.AlterColumn<int>(
                name: "TUserId",
                table: "TOrder",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TOrder_TUser_TUserId",
                table: "TOrder",
                column: "TUserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOrder_TUser_TUserId",
                table: "TOrder");

            migrationBuilder.AlterColumn<int>(
                name: "TUserId",
                table: "TOrder",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_TOrder_TUser_TUserId",
                table: "TOrder",
                column: "TUserId",
                principalTable: "TUser",
                principalColumn: "Id");
        }
    }
}
