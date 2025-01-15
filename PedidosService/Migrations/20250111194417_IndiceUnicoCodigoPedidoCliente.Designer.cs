﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PedidosService.Data;

#nullable disable

namespace PedidosService.Migrations
{
    [DbContext(typeof(PedidosDbContext))]
    [Migration("20250111194417_IndiceUnicoCodigoPedidoCliente")]
    partial class IndiceUnicoCodigoPedidoCliente
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PedidosService.Model.ItemPedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .HasColumnType("integer");

                    b.Property<string>("Produto")
                        .HasColumnType("text")
                        .HasColumnName("produto");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("preco");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer")
                        .HasColumnName("quantidade");

                    b.HasKey("PedidoId", "Produto");

                    b.ToTable("itenspedido", (string)null);
                });

            modelBuilder.Entity("PedidosService.Model.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CodigoCliente")
                        .HasColumnType("integer")
                        .HasColumnName("codigo_cliente");

                    b.Property<int>("CodigoPedido")
                        .HasColumnType("integer")
                        .HasColumnName("codigo_pedido");

                    b.HasKey("Id");

                    b.HasIndex("CodigoPedido", "CodigoCliente")
                        .IsUnique();

                    b.ToTable("pedidos", (string)null);
                });

            modelBuilder.Entity("PedidosService.Model.ItemPedido", b =>
                {
                    b.HasOne("PedidosService.Model.Pedido", "Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("PedidosService.Model.Pedido", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
