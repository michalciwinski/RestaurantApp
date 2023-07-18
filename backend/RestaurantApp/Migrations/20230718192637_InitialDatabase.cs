using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TDishType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDishType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    TDishTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMenu_TDishType_TDishTypeId",
                        column: x => x.TDishTypeId,
                        principalTable: "TDishType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    QR = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    UserTypeId = table.Column<int>(type: "integer", nullable: false),
                    TUserTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TUser_TUserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "TUserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfOrder = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Bill = table.Column<int>(type: "integer", nullable: false),
                    AdditionalComment = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    TUserId = table.Column<int>(type: "integer", nullable: false),
                    TStateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOrder_TState_TStateId",
                        column: x => x.TStateId,
                        principalTable: "TState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOrder_TUser_TUserId",
                        column: x => x.TUserId,
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TOrderPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TMenuId = table.Column<int>(type: "integer", nullable: false),
                    TOrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOrderPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOrderPosition_TMenu_TMenuId",
                        column: x => x.TMenuId,
                        principalTable: "TMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOrderPosition_TOrder_TOrderId",
                        column: x => x.TOrderId,
                        principalTable: "TOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TMenu_TDishTypeId",
                table: "TMenu",
                column: "TDishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrder_TStateId",
                table: "TOrder",
                column: "TStateId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrder_TUserId",
                table: "TOrder",
                column: "TUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrderPosition_TMenuId",
                table: "TOrderPosition",
                column: "TMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrderPosition_TOrderId",
                table: "TOrderPosition",
                column: "TOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TUser_UserTypeId",
                table: "TUser",
                column: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TOrderPosition");

            migrationBuilder.DropTable(
                name: "TMenu");

            migrationBuilder.DropTable(
                name: "TOrder");

            migrationBuilder.DropTable(
                name: "TDishType");

            migrationBuilder.DropTable(
                name: "TState");

            migrationBuilder.DropTable(
                name: "TUser");

            migrationBuilder.DropTable(
                name: "TUserType");
        }
    }
}
