using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinHunt.Application.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Skins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Skins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
