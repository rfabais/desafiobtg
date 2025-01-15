using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PedidosService.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarPedidosDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itenspedido_pedidos_PedidoId",
                table: "itenspedido");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "itenspedido",
                newName: "pedidoid");

            migrationBuilder.AlterColumn<int>(
                name: "codigo_pedido",
                table: "pedidos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_itenspedido_pedidos_pedidoid",
                table: "itenspedido",
                column: "pedidoid",
                principalTable: "pedidos",
                principalColumn: "codigo_pedido",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itenspedido_pedidos_pedidoid",
                table: "itenspedido");

            migrationBuilder.RenameColumn(
                name: "pedidoid",
                table: "itenspedido",
                newName: "PedidoId");

            migrationBuilder.AlterColumn<int>(
                name: "codigo_pedido",
                table: "pedidos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_itenspedido_pedidos_PedidoId",
                table: "itenspedido",
                column: "PedidoId",
                principalTable: "pedidos",
                principalColumn: "codigo_pedido",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
