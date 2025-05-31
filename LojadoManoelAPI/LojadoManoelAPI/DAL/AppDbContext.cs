using Microsoft.EntityFrameworkCore;
using LojadoManoelAPI.Models;

namespace LojadoManoelAPI.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pedido> PedidosDb { get; set; }
        public DbSet<Produto> ProdutosDb { get; set; }

        //Configurações do modelo e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configurando Dimensoes como propriedade complexa dentro de produto
            modelBuilder.Entity<Produto>().OwnsOne(p => p.Dimensoes);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.PedidoId);
        }
    }
}
