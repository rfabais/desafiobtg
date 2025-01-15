using Microsoft.EntityFrameworkCore;
using PedidosService.Model;

namespace PedidosService.Data
{
    public class PedidosDbContext : DbContext
    {
        public PedidosDbContext(DbContextOptions<PedidosDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pedido>()
                .HasKey(p => p.CodigoPedido);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.CodigoPedido)
                .ValueGeneratedOnAdd()
                .HasColumnName("codigo_pedido");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.CodigoCliente)
                .HasColumnName("codigo_cliente");

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>().ToTable("pedidos");

            modelBuilder.Entity<ItemPedido>()
                .HasKey(i => new { i.PedidoId, i.Produto });

            modelBuilder.Entity<ItemPedido>()
                .Property(i => i.PedidoId)
                .HasColumnName("pedido_id");

            modelBuilder.Entity<ItemPedido>()
                .Property(i => i.Produto)
                .HasColumnName("produto");

            modelBuilder.Entity<ItemPedido>()
                .Property(i => i.Quantidade)
                .HasColumnName("quantidade");

            modelBuilder.Entity<ItemPedido>()
                .Property(i => i.Preco)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("preco");

            modelBuilder.Entity<ItemPedido>().ToTable("itenspedido");
        }
    }
}
