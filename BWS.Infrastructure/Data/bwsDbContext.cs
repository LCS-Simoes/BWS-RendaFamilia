using BWS.Domain.Entidades;
using BWS.Infrastructure.Data.Map;
using Microsoft.EntityFrameworkCore;


namespace BWS.Infrastructure.Data
{
    public class bwsDbContext : DbContext
    {
        public bwsDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientesMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
