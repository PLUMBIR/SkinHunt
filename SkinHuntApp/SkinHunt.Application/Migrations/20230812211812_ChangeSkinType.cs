using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinHunt.Application.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSkinType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Skins");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Skins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SkinTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Subcategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skins_TypeId",
                table: "Skins",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skins_SkinTypes_TypeId",
                table: "Skins",
                column: "TypeId",
                principalTable: "SkinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skins_SkinTypes_TypeId",
                table: "Skins");

            migrationBuilder.DropTable(
                name: "SkinTypes");

            migrationBuilder.DropIndex(
                name: "IX_Skins_TypeId",
                table: "Skins");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Skins");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Skins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
