using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
        public DbSet<OrdenDeGasoil> OrdenesDeGasoil { get; set; } 
        public DbSet<DestinoDeCarga> DestinosDeCarga { get; set; }
        public DbSet<DestinoDeDescarga> DestinosDeDescarga { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            MapVehiculo(modelBuilder);
            MapChofer(modelBuilder);
            MapOrdenDeGasoil(modelBuilder);
            MapDestinoDeCarga(modelBuilder);
            MapOrdenDeCarga(modelBuilder);
        }
     
        private static void MapOrdenDeCarga(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdenDeCarga>().HasKey(e => e.Id);
            modelBuilder.Entity<OrdenDeCarga>().Property(e => e.NumeroOrden);
            modelBuilder.Entity<OrdenDeCarga>().Property(e => e.DiaHoraCarga);
            modelBuilder.Entity<OrdenDeCarga>().Property(e => e.TipoMercaderia);
            modelBuilder.Entity<OrdenDeCarga>().Property(e => e.DistanciaViaje);
            modelBuilder.Entity<OrdenDeCarga>().HasOne(u => u.Cliente)
                                               .WithMany()
                                               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrdenDeCarga>().HasOne(u => u.DestinoDeCarga)
                                              .WithMany()
                                              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrdenDeCarga>().HasOne(u => u.DestinoDeDescarga)
                                              .WithMany()
                                              .OnDelete(DeleteBehavior.NoAction);
        }

        private static void MapDestinoDeCarga(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DestinoDeCarga>().HasKey(e => e.Id);
            modelBuilder.Entity<DestinoDeCarga>().Property(u => u.Latitud);
            modelBuilder.Entity<DestinoDeCarga>().Property(u => u.Longitud);
            modelBuilder.Entity<DestinoDeCarga>().Property(u => u.NombreEstablecimiento);
            modelBuilder.Entity<DestinoDeCarga>().HasOne(u => u.Cliente);
        }
        private static void MapOrdenDeGasoil(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdenDeGasoil>().HasKey(e => e.Id);
            modelBuilder.Entity<OrdenDeGasoil>().Property(u => u.NumeroOrden);
            modelBuilder.Entity<OrdenDeGasoil>().Property(u => u.Litros);
            modelBuilder.Entity<OrdenDeGasoil>().Property(u => u.Fecha);
            modelBuilder.Entity<OrdenDeGasoil>().Property(u => u.Estacion);
            modelBuilder.Entity<OrdenDeGasoil>().HasOne(u =>  u.Transporte)
                                                .WithMany()
                                                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrdenDeGasoil>().HasOne(u => u.Chofer)
                                                .WithMany()
                                                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrdenDeGasoil>().HasOne(u => u.Vehiculo)
                                                .WithMany()
                                                .OnDelete(DeleteBehavior.NoAction);
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
