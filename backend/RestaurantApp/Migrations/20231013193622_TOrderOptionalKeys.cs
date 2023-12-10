using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class TOrderOptionalKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOrder_TState_TStateId",
                table: "TOrder");

            migrationBuilder.AlterColumn<int>(
                name: "TStateId",
                table: "TOrder",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_TOrder_TState_TStateId",
                table: "TOrder",
                column: "TStateId",
                principalTable: "TState",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TOrder_TState_TStateId",
                table: "TOrder");

            migrationBuilder.AlterColumn<int>(
                name: "TStateId",
                table: "TOrder",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TOrder_TState_TStateId",
                table: "TOrder",
                column: "TStateId",
                principalTable: "TState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
