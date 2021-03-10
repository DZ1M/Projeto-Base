using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Base.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.UseIdentityByDefaultColumns();

            //  Isto aqui faz com que todos os foreing keys sejam restritos e não cascade.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
