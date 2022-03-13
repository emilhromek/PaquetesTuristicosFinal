using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Turismo.Template.API.Migrations
{
    public partial class migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaBuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaBuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaChoferes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoferId = table.Column<int>(type: "int", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaChoferes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaCoordinadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoordinadorId = table.Column<int>(type: "int", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaCoordinadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Choferes",
                columns: table => new
                {
                    ChoferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Licencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choferes", x => x.ChoferId);
                });

            migrationBuilder.CreateTable(
                name: "Coordinadores",
                columns: table => new
                {
                    CoordinadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinadores", x => x.CoordinadorId);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPasajeros = table.Column<int>(type: "int", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.GrupoId);
                });

            migrationBuilder.CreateTable(
                name: "Terminales",
                columns: table => new
                {
                    TerminalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminales", x => x.TerminalId);
                });

            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVuelta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminalOrigen1Id = table.Column<int>(type: "int", nullable: false),
                    TerminalOrigen2Id = table.Column<int>(type: "int", nullable: false),
                    TerminalOrigen3Id = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    Chofer1Id = table.Column<int>(type: "int", nullable: false),
                    Chofer2Id = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    CoordinadorId = table.Column<int>(type: "int", nullable: false),
                    IdentificadorUnicoDeViaje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.ViajeId);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_Buses_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Choferes",
                columns: new[] { "ChoferId", "Agenda", "Apellido", "Contacto", "Email", "Licencia", "Nombre", "Vencimiento" },
                values: new object[,]
                {
                    { 1, "./agenda/chofer/1", "Gonzalez", "cel 011-4242-6789", "ninguno", "CNT234568DE", "José", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "./agenda/chofer/2", "Perez", "cel 0221-432-1234", "juan999@gmail.com", "AP25533467", "Juan", new DateTime(2021, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "./agenda/chofer/3", "Gutierrez", "cel 0221-422-4567", "elpedrogut1980@hotmail.com", "CNT3412567P", "Pedro", new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "./agenda/chofer/3", "seoane", "cel 011-456-6587", "ninguno", "P4563526341", "Oscar", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Coordinadores",
                columns: new[] { "CoordinadorId", "Agenda", "Apellido", "Contacto", "Email", "Nombre" },
                values: new object[,]
                {
                    { 1, "./agenda/coordinador/1", "Zarza", "WS +5491144176068", "sergiozarza0@gmail.com", "Sergio" },
                    { 2, "./agenda/coordinador/2", "Conde", "WS +5491134563456", "juanconde01@gmail.com", "Juan" },
                    { 3, "./agenda/coordinador/3", "Hromek", "cel 011-4345-4545", "hromekemil@gmail.com", "Emil" },
                    { 4, "./agenda/coordinador/4", "Jimenez", "WS +5491141236068", "estebanjimenez23@gmail.com", "Esteban" },
                    { 5, "./agenda/coordinador/5", "Rojas", "WS +54922156342726", "gonzar1234@hotmail.com", "Gonzalo" }
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "EmpresaId", "Contacto", "Email", "Nombre" },
                values: new object[,]
                {
                    { 4, "", "", "Otras empresas" },
                    { 3, "WS +549115430123", "central@chevalier.com.ar", "Chevallier" },
                    { 1, "", "", "Ninguna (bus propio)" },
                    { 2, "Tel 011-444-1234", "contacto@elcondor.com.ar", "El Condor" }
                });

            migrationBuilder.InsertData(
                table: "Terminales",
                columns: new[] { "TerminalId", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 6, "Terminal de omnibus de Retiro", "Retiro" },
                    { 1, "Terminal del cruce de Varela", "Cruce Varela" },
                    { 2, "Pasamos por el centro de Quilmes", "Centro de Quilmes" },
                    { 3, "Terminal de omnibus de Avellaneda", "Avellaneda" },
                    { 4, "Terminal de omnibus de Adrogue", "Adrogue" },
                    { 5, "Terminal de omnibus de Lomas de Zamora", "Lomas de Zamora" },
                    { 7, "Terminal de omnibus de Liniers", "Liniers" }
                });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "BusId", "Capacidad", "EmpresaId", "Numero", "Observacion", "Patente" },
                values: new object[,]
                {
                    { 1, 44, 1, "0234", "", "ABC345" },
                    { 2, 52, 1, "1893", "", "VDG567" },
                    { 3, 52, 1, "1894", "", "ABUY819" },
                    { 4, 39, 1, "2350", "", "ACKN233" },
                    { 5, 50, 2, "5984", "Genérico", "PSX823" },
                    { 6, 50, 3, "7742", "Genérico", "MDJ830" },
                    { 7, 50, 4, "4896", "No definido", "MRIU98" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buses_EmpresaId",
                table: "Buses",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaBuses");

            migrationBuilder.DropTable(
                name: "AgendaChoferes");

            migrationBuilder.DropTable(
                name: "AgendaCoordinadores");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Choferes");

            migrationBuilder.DropTable(
                name: "Coordinadores");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Terminales");

            migrationBuilder.DropTable(
                name: "Viajes");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
