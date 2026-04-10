using Microsoft.EntityFrameworkCore;
using NikeStoreApi.Models;

namespace NikeStoreApi.src
{
    public class NikeContext : DbContext
    {
        public NikeContext(DbContextOptions<NikeContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}