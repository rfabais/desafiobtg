using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidosService.Migrations
{
    /// <inheritdoc />
    public partial class UnicidadeCodigoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_pedidos_codigo_pedido",
                table: "pedidos",
                column: "codigo_pedido",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pedidos_codigo_pedido",
                table: "pedidos");
        }
    }
}
