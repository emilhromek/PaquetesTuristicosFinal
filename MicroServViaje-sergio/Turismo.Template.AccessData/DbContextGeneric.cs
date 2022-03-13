using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.AccessData
{
    public class DbContextGeneric : DbContext
    {
        public virtual DbSet<Viaje> Viajes { get; set; }
        public virtual DbSet<Coordinador> Coordinadores { get; set; }
        public virtual DbSet<Terminal> Terminales { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<Chofer> Choferes { get; set; }
        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<AgendaBus> AgendaBuses { get; set; }
        public virtual DbSet<AgendaChofer> AgendaChoferes { get; set; }
        public virtual DbSet<AgendaCoordinador> AgendaCoordinadores { get; set; }

        public DbContextGeneric() { }
        public DbContextGeneric(DbContextOptions<DbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=msviajes;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(b => b.MigrationsAssembly("Turismo.Template.API"));


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Terminal>().HasData(
                new Terminal { TerminalId = 1, Nombre = "Cruce Varela", Descripcion = "Terminal del cruce de Varela" },
                new Terminal { TerminalId = 2, Nombre = "Centro de Quilmes", Descripcion = "Pasamos por el centro de Quilmes" },
                new Terminal { TerminalId = 3, Nombre = "Avellaneda", Descripcion = "Terminal de omnibus de Avellaneda" },
                new Terminal { TerminalId = 4, Nombre = "Adrogue", Descripcion = "Terminal de omnibus de Adrogue" },
                new Terminal { TerminalId = 5, Nombre = "Lomas de Zamora", Descripcion = "Terminal de omnibus de Lomas de Zamora" },
                new Terminal { TerminalId = 6, Nombre = "Retiro", Descripcion = "Terminal de omnibus de Retiro" },
                new Terminal { TerminalId = 7, Nombre = "Liniers", Descripcion = "Terminal de omnibus de Liniers" });

            modelBuilder.Entity<Empresa>().HasData(
                new Empresa { EmpresaId = 1, Nombre = "Ninguna (bus propio)", Contacto = "", Email = "" },
                new Empresa { EmpresaId = 2, Nombre = "El Condor", Contacto = "Tel 011-444-1234", Email = "contacto@elcondor.com.ar" },
                new Empresa { EmpresaId = 3, Nombre = "Chevallier", Contacto = "WS +549115430123", Email = "central@chevalier.com.ar" },
                new Empresa { EmpresaId = 4, Nombre = "Otras empresas", Contacto = "", Email = "" });

            modelBuilder.Entity<Chofer>().HasData(
                new Chofer { ChoferId = 1, Nombre = "José", Apellido = "Gonzalez", Contacto = "cel 011-4242-6789", Email = "ninguno", Licencia = "CNT234568DE", Vencimiento = new DateTime(2022, 2, 10), Agenda = "./agenda/chofer/1" },
                new Chofer { ChoferId = 2, Nombre = "Juan", Apellido = "Perez", Contacto = "cel 0221-432-1234", Email = "juan999@gmail.com", Licencia = "AP25533467", Vencimiento = new DateTime(2021, 11, 20), Agenda = "./agenda/chofer/2" },
                new Chofer { ChoferId = 3, Nombre = "Pedro", Apellido = "Gutierrez", Contacto = "cel 0221-422-4567", Email = "elpedrogut1980@hotmail.com", Licencia = "CNT3412567P", Vencimiento = new DateTime(2023, 4, 25), Agenda = "./agenda/chofer/3" },
                new Chofer { ChoferId = 4, Nombre = "Oscar", Apellido = "seoane", Contacto = "cel 011-456-6587", Email = "ninguno", Licencia = "P4563526341", Vencimiento = new DateTime(2021, 7, 15), Agenda = "./agenda/chofer/3" });

            modelBuilder.Entity<Coordinador>().HasData(
                new Coordinador { CoordinadorId = 1, Nombre = "Sergio", Apellido = "Zarza", Contacto = "WS +5491144176068", Email = "sergiozarza0@gmail.com", Agenda = "./agenda/coordinador/1" },
                new Coordinador { CoordinadorId = 2, Nombre = "Juan", Apellido = "Conde", Contacto = "WS +5491134563456", Email = "juanconde01@gmail.com", Agenda = "./agenda/coordinador/2" },
                new Coordinador { CoordinadorId = 3, Nombre = "Emil", Apellido = "Hromek", Contacto = "cel 011-4345-4545", Email = "hromekemil@gmail.com", Agenda = "./agenda/coordinador/3" },
                new Coordinador { CoordinadorId = 4, Nombre = "Esteban", Apellido = "Jimenez", Contacto = "WS +5491141236068", Email = "estebanjimenez23@gmail.com", Agenda = "./agenda/coordinador/4" },
                new Coordinador { CoordinadorId = 5, Nombre = "Gonzalo", Apellido = "Rojas", Contacto = "WS +54922156342726", Email = "gonzar1234@hotmail.com", Agenda = "./agenda/coordinador/5" });

            modelBuilder.Entity<Bus>().HasData(
                new Bus { BusId = 1, Numero = "0234", Capacidad = 44, Patente = "ABC345", EmpresaId = 1, Observacion = "" },
                new Bus { BusId = 2, Numero = "1893", Capacidad = 52, Patente = "VDG567", EmpresaId = 1, Observacion = "" },
                new Bus { BusId = 3, Numero = "1894", Capacidad = 52, Patente = "ABUY819", EmpresaId = 1, Observacion = "" },
                new Bus { BusId = 4, Numero = "2350", Capacidad = 39, Patente = "ACKN233", EmpresaId = 1, Observacion = "" },
                new Bus { BusId = 5, Numero = "5984", Capacidad = 50, Patente = "PSX823", EmpresaId = 2, Observacion = "Genérico" },
                new Bus { BusId = 6, Numero = "7742", Capacidad = 50, Patente = "MDJ830", EmpresaId = 3, Observacion = "Genérico" },
                new Bus { BusId = 7, Numero = "4896", Capacidad = 50, Patente = "MRIU98", EmpresaId = 4, Observacion = "No definido" });

                    //modelBuilder.Entity<Viaje>(entity =>
                    //{
                    //    entity.HasKey(q => q.ViajeId);

                    //    entity.Property(q => q.FechaIda).IsRequired();
                    //    entity.Property(q => q.FechaVuelta).IsRequired();
                    //    entity.Property(q => q.Hora).IsRequired();
                    //    entity.Property(q => q.Excursion).HasMaxLength(255).IsRequired();


                    //    entity.HasOne(x => x.TipoNavigator)
                    //    .WithMany(x => x.ListViaje)
                    //    .HasForeignKey(x => x.TipoId);

                    //    entity.HasOne(x => x.TipoCordinador)
                    //    .WithMany(i => i.ViajesNavigator)
                    //    .HasForeignKey(x => x.CoordinadorId);

                    //    entity.HasOne(x => x.TerminalNavigator)
                    //    .WithMany(i => i.ViajesNavigator)
                    //    .HasForeignKey(x => x.TerminalId);



                    //});

                    //modelBuilder.Entity<Coordinador> (entity =>
                    //{
                    //    entity.HasKey(q => q.CoordinadorId);

                    //    entity.Property(q => q.Nombre).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Apellido).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Agenda).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Contacto).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Email).HasMaxLength(255).IsRequired();
                    //});

                    //modelBuilder.Entity<Chofer>(entity =>
                    //{
                    //    entity.HasKey(q => q.ChoferId);

                    //    entity.Property(q => q.Nombre).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Apellido).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Agenda).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Contacto).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Email).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Licencia).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Vencimiento).HasMaxLength(255).IsRequired();

                    //    entity.HasOne(x => x.Viaje)
                    //    .WithMany(i => i.Choferes)
                    //    .HasForeignKey(x => x.ViajeId);

                    //});

                    //modelBuilder.Entity<Bus>(entity =>
                    //{
                    //    entity.HasKey(q => q.BusId);

                    //    entity.Property(q => q.Numero).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Patente).HasMaxLength(55).IsRequired();
                    //    entity.Property(q => q.Capacidad).HasMaxLength(255).IsRequired();

                    //    entity.HasOne(x => x.Viaje)
                    //    .WithMany(i => i.Buses)
                    //    .HasForeignKey(x => x.ViajeId);

                    //    entity.HasOne(x => x.Empresa)
                    //    .WithMany(i => i.Buses)
                    //    .HasForeignKey(x => x.EmpresaId);
                    //});

                    //modelBuilder.Entity<Empresa>(entity =>
                    //{
                    //    entity.HasKey(q => q.EmpresaId);

                    //    entity.Property(q => q.Nombre).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Email).HasMaxLength(255).IsRequired();
                    //    entity.Property(q => q.Contacto).HasMaxLength(255).IsRequired();

                    //});

                    //modelBuilder.Entity<Grupo>(entity =>
                    //{
                    //    entity.HasKey(q => q.GrupoId);

                    //    entity.Property(q => q.Cantidad).HasMaxLength(255).IsRequired();

                    //    entity.HasOne(x => x.CoordinadorNavigator)
                    //    .WithMany(i => i.GruposNavigator)
                    //    .HasForeignKey(x => x.CoordinadorId);
                    //});

                    //modelBuilder.Entity<Terminal>(entity =>
                    //{
                    //    entity.HasKey(q => q.TerminalId);

                    //    entity.Property(q => q.Nombre).HasMaxLength(255).IsRequired();
                    //});
                    //modelBuilder.Entity<TipoViaje>(entity =>
                    //{
                    //    entity.HasKey(q => q.TipoId);

                    //    entity.Property(q => q.Descripcion).HasMaxLength(255).IsRequired();

                    //});
                }
    }
}
