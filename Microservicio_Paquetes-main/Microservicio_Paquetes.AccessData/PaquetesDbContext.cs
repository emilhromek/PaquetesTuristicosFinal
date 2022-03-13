using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microservicio_Paquetes.Domain.Entities;

namespace Microservicio_Paquetes.AccessData
{
    public class PaquetesDbContext: DbContext 
    {

        public PaquetesDbContext(DbContextOptions<PaquetesDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=mspaquetes;Trusted_Connection=True;");
        }
        public DbSet<ComentarioDestino> ComentarioDestino { get; set; }
        public DbSet<Destino> Destino { get; set; }
        public DbSet<Excursion> Excursion { get; set; }
        public DbSet<FormaPago> FormaPago { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Paquete> Paquete { get; set; }
        public DbSet<PaqueteViaje> PaqueteViaje { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<PaqueteExcursion> ReservaExcursion { get; set; }
        public DbSet<PaqueteExcursion> PaqueteExcursion { get; set; }
        public DbSet<ReservaPasajero> ReservaPasajero { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<FormaPago>().HasData(new FormaPago[] {
                new FormaPago{Id = 1, Descripcion = "Efectivo"},
                new FormaPago{Id = 2, Descripcion = "Tarjeta de crédito"},
                new FormaPago{Id = 3, Descripcion = "Tarjeta de débito"},
                new FormaPago{Id = 4, Descripcion = "Mercado Pago"},
                new FormaPago{Id = 5, Descripcion = "Pago Fácil"},
                new FormaPago{Id = 6, Descripcion = "Bitcoin"},
                new FormaPago{Id = 7, Descripcion = "Otra (contactarse)"},
            });

            modelBuilder.Entity<Destino>().HasData(new Destino[] {
                new Destino{Id = 1, Lugar = "Quilmes", Descripcion = "Descripción de Quilmes", HorasDeViaje = 5,
                Atractivo = "Atractivo de Quilmes", Historia = "Historia de Quilmes",
                Imagen = "C:/Users/emil2/OneDrive/Escritorio/Proyecto software - tp integrador/Imagenes tp ps/QuilmesCityMontage.jpg"},
                new Destino{Id = 2, Lugar = "Berazategui", Descripcion = "Descripción de Berazategui", HorasDeViaje = 6,
                Atractivo = "Atractivo de Berazategui", Historia = "Historia de Berazategui",
                Imagen = "C:/Users/emil2/OneDrive/Escritorio/Proyecto software - tp integrador/Imagenes tp ps/1200px-Plaza_San_Martín_(Berazategui).jpg"},
                new Destino{Id = 3, Lugar = "Florencio Varela", Descripcion = "Descripción de Florencio Varela", HorasDeViaje = 7,
                Atractivo = "Atractivo de Florencio Varela", Historia = "Historia de Varela",
                Imagen = "C:/Users/emil2/OneDrive/Escritorio/Proyecto software - tp integrador/Imagenes tp ps/Collage_Varela_ciudad.png"},
            });

            modelBuilder.Entity<Hotel>().HasData(new Hotel[] {
                new Hotel{Id = 1, Marca = "Hilton", Sucursal = "Quilmes",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 1, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 2, Marca = "Sheraton", Sucursal = "Quilmes",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 1, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 3, Marca = "Silken", Sucursal = "Quilmes",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 1, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 4, Marca = "Hilton", Sucursal = "Berazategui",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 2, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 5, Marca = "Sheraton", Sucursal = "Berazategui",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 2, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 6, Marca = "Silken", Sucursal = "Berazategui",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 2, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 7, Marca = "Hilton", Sucursal = "Varela",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 3, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 8, Marca = "Sheraton", Sucursal = "Varela",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 3, Capacidad = 100, Costo = 1000 },
                new Hotel{Id = 9, Marca = "Silken", Sucursal = "Varela",
                Estrellas = 5, Direccion = "Calle Falsa 123", DestinoId = 3, Capacidad = 100, Costo = 1000 },
            });

        }

    }
}
