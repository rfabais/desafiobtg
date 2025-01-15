using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PedidosService.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_itenspedido",
                table: "itenspedido");

            migrationBuilder.DropIndex(
                name: "IX_itenspedido_PedidoId",
                table: "itenspedido");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "itenspedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itenspedido",
                table: "itenspedido",
                columns: new[] { "PedidoId", "produto" });

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_codigo_cliente",
                table: "pedidos",
                column: "codigo_cliente",
                unique: true);

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
                name: "IX_pedidos_codigo_cliente",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_codigo_pedido",
                table: "pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itenspedido",
                table: "itenspedido");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "itenspedido",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_itenspedido",
                table: "itenspedido",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_itenspedido_PedidoId",
                table: "itenspedido",
                column: "PedidoId");
        }
    }
}
