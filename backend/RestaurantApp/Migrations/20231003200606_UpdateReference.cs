using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TUser_TUserType_UserTypeId",
                table: "TUser");

            migrationBuilder.DropIndex(
                name: "IX_TUser_UserTypeId",
                table: "TUser");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "TUser");

            migrationBuilder.CreateIndex(
                name: "IX_TUser_TUserTypeId",
                table: "TUser",
                column: "TUserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TUser_TUserType_TUserTypeId",
                table: "TUser",
                column: "TUserTypeId",
                principalTable: "TUserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TUser_TUserType_TUserTypeId",
                table: "TUser");

            migrationBuilder.DropIndex(
                name: "IX_TUser_TUserTypeId",
                table: "TUser");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "TUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TUser_UserTypeId",
                table: "TUser",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TUser_TUserType_UserTypeId",
                table: "TUser",
                column: "UserTypeId",
                principalTable: "TUserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
