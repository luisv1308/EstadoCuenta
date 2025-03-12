using EstadoCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EstadoCuenta.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TarjetaCredito> TarjetasCredito { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones
            modelBuilder.Entity<Transaccion>()
                .HasOne(t => t.TarjetaCredito)
                .WithMany(tc => tc.Transacciones)
                .HasForeignKey(t => t.TarjetaCreditoId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
