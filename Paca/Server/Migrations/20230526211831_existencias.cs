using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paca.Server.Migrations
{
    /// <inheritdoc />
    public partial class existencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Existencias",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Existencias",
                table: "Productos");
        }
    }
}
