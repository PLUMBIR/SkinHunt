using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinHunt.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddSoldSkinsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoldsSkins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SkinId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldsSkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldsSkins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoldsSkins_Skins_SkinId",
                        column: x => x.SkinId,
                        principalTable: "Skins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoldsSkins_SkinId",
                table: "SoldsSkins",
                column: "SkinId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldsSkins_UserId",
                table: "SoldsSkins",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldsSkins");
        }
    }
}
