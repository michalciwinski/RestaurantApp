using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderVarTypePrize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Bill",
                table: "TOrder",
                type: "double precision",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 25);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Bill",
                table: "TOrder",
                type: "integer",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldMaxLength: 25);
        }
    }
}
