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

    }
}        