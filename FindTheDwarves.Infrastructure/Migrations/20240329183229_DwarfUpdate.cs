using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindTheDwarves.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DwarfUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "Dwarves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "Dwarves");
        }
    }
}
