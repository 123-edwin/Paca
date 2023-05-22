using Microsoft.EntityFrameworkCore;
using Paca.Shared.Modelos;

namespace Paca.Server.Data
{
    public class ClienteContexto : DbContext
    {
        public ClienteContexto(DbContextOptions<ClienteContexto> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Producto)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(p => p.ProductoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<Pedido>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    var pedido = entry.Entity;
                    var producto = Set<Producto>().Find(pedido.ProductoId);
                    var cliente = Set<Cliente>().Find(pedido.ClienteId);

                    if (producto != null && pedido.CantidadSolicitada > producto.CantidadExistencias)
                    {
                        throw new InvalidOperationException("La cantidad solicitada excede las existencias disponibles del producto.");
                    }

                    pedido.Producto = producto;
                    pedido.Cliente = cliente;
                    producto.CantidadExistencias -= pedido.CantidadSolicitada;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    var pedido = entry.Entity;
                    var producto = Set<Producto>().Find(pedido.ProductoId);

                    producto.CantidadExistencias += pedido.CantidadSolicitada;
                }
            }

            return base.SaveChanges();
        }
    }
}
