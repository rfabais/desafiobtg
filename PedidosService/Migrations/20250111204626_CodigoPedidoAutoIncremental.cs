using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidosService.Migrations
{
    /// <inheritdoc />
    public partial class CodigoPedidoAutoIncremental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itenspedido_pedidos_pedidoid",
                table: "itenspedido");

            migrationBuilder.RenameColumn(
                name: "pedidoid",
                table: "itenspedido",
                newName: "pedido_id");

            migrationBuilder.AddForeignKey(
                name: "FK_itenspedido_pedidos_pedido_id",
                table: "itenspedido",
                column: "pedido_id",
                principalTable: "pedidos",
                principalColumn: "codigo_pedido",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itenspedido_pedidos_pedido_id",
                table: "itenspedido");

            migrationBuilder.RenameColumn(
                name: "pedido_id",
                table: "itenspedido",
                newName: "pedidoid");

            migrationBuilder.AddForeignKey(
                name: "FK_itenspedido_pedidos_pedidoid",
                table: "itenspedido",
                column: "pedidoid",
                principalTable: "pedidos",
                principalColumn: "codigo_pedido",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
