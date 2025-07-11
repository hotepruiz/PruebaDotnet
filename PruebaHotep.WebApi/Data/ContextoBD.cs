using Microsoft.EntityFrameworkCore;
using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Data
{
    public class ContextoBD : DbContext
    {
        //siendo completametne sincero, no entiendo al 100% estas lineas
        //Se que son para configurar la conexion del orm, pero no sé exactamente que hacen
        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options) { 
            
        }

        //estas son las "tablas" (modelos)
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        //Entiendo que esto es para crear el modelo como tal
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .HasIndex(c => c.NumeroCuenta)
                .IsUnique();  // esto es para garantizar que el número de cuenta sea único

            base.OnModelCreating(modelBuilder);
        }
    }
}
