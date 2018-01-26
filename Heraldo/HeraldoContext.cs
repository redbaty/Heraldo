using Microsoft.EntityFrameworkCore;
using RaioNet.Entity.Sistema.pessoa;

namespace Heraldo
{
    internal class HeraldoContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=heraldo.db");
        }
    }
}