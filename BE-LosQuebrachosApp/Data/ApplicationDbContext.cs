using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Transporte> Transportes { get; set; }
        public DbSet<Chofer> Choferes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<OrdenDeCarga> OrdenesDeCargas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            MapVehiculo(modelBuilder);
            MapChofer(modelBuilder);
        }
        private static void MapVehiculo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehiculo>().HasKey(e => e.Id);
            modelBuilder.Entity<Vehiculo>().Property(u => u.Chasis);
            modelBuilder.Entity<Vehiculo>().Property(u => u.Acoplado);
            modelBuilder.Entity<Vehiculo>().Property(u => u.Tipo);
            modelBuilder.Entity<Vehiculo>().Property(u => u.CapacidadTN);
            modelBuilder.Entity<Vehiculo>().HasOne(u => u.Transporte);
        }
        private static void MapChofer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chofer>().HasKey(e => e.Id);
            modelBuilder.Entity<Chofer>().Property(u => u.Nombre);
            modelBuilder.Entity<Chofer>().Property(u => u.Apellido);
            modelBuilder.Entity<Chofer>().Property(u => u.Cuit);
            modelBuilder.Entity<Chofer>().HasOne(u => u.Transporte);
        }
    }
}
