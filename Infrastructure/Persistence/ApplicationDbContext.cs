using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descricao).HasMaxLength(500);
                entity.Property(p => p.Preco).HasPrecision(18, 2);
                entity.HasIndex(p => p.Codigo).IsUnique();
            });
        }
    }
}