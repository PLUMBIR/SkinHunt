using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinHunt.Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSkinEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Skins",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Skins");
        }
    }
}
