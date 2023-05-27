using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paca.Server.Migrations
{
    /// <inheritdoc />
    public partial class funcionespedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadAnterior",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadAnterior",
                table: "Pedidos");
        }
    }
}
