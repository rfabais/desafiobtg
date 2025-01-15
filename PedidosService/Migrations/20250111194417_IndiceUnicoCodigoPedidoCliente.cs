using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidosService.Migrations
{
    /// <inheritdoc />
    public partial class IndiceUnicoCodigoPedidoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pedidos_codigo_pedido",
                table: "pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_codigo_pedido_codigo_cliente",
                table: "pedidos",
                columns: new[] { "codigo_pedido", "codigo_cliente" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pedidos_codigo_pedido_codigo_cliente",
                table: "pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_codigo_pedido",
                table: "pedidos",
                column: "codigo_pedido");
        }
    }
}
