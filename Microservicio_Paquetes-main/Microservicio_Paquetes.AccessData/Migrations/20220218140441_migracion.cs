using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microservicio_Paquetes.AccessData.Migrations
{
    public partial class migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atractivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Historia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorasDeViaje = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destino", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteExcursion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    ExcursionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteExcursion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioDestino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    PasajeroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioDestino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentarioDestino_Destino_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Destino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Excursion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excursion_Destino_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Destino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sucursal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estrellas = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotel_Destino_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Destino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paquete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaArribo = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaPartida = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaLlegada = table.Column<DateTime>(type: "DateTime", nullable: false),
                    TotalNoches = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    IdentificadorUnicoDePaquete = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paquete_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioPaqueteHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaqueteHistoricoId = table.Column<int>(type: "int", nullable: false),
                    PasajeroId = table.Column<int>(type: "int", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioPaqueteHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentarioPaqueteHistorico_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteViaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteViaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaqueteViaje_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Pasajeros = table.Column<int>(type: "int", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
                    PasajeroId = table.Column<int>(type: "int", nullable: false),
                    FormaPagoId = table.Column<int>(type: "int", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_FormaPago_FormaPagoId",
                        column: x => x.FormaPagoId,
                        principalTable: "FormaPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaExcursion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    ExcursionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaExcursion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaExcursion_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaPasajero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    PasajeroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaPasajero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaPasajero_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Destino",
                columns: new[] { "Id", "Atractivo", "Descripcion", "Historia", "HorasDeViaje", "Imagen", "Lugar" },
                values: new object[,]
                {
                    { 1, "Atractivo de Quilmes", "Descripción de Quilmes", "Historia de Quilmes", 5, "C:/Users/emil2/OneDrive/Escritorio/Proyecto software - tp integrador/Imagenes tp ps/QuilmesCityMontage.jpg", "Quilmes" },
                    { 2, "Atractivo de Berazategui", "Descripción de Berazategui", "Historia de Berazategui", 6, "C:/Users/emil2/OneDrive/Escritorio/Proyecto software - tp integrador/Imagenes tp ps/1200px-Plaza_San_Martín_(Berazategui).jpg", "Berazategui" },
                    { 3, "Atractivo de Florencio Varela", "Descripción de Florencio Varela", "Historia de Varela", 7, "C:/Users/emil2/OneDrive/Escritorio/Proyecto software - tp integrador/Imagenes tp ps/Collage_Varela_ciudad.png", "Florencio Varela" }
                });

            migrationBuilder.InsertData(
                table: "FormaPago",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Efectivo" },
                    { 2, "Tarjeta de crédito" },
                    { 3, "Tarjeta de débito" },
                    { 4, "Mercado Pago" },
                    { 5, "Pago Fácil" },
                    { 6, "Bitcoin" },
                    { 7, "Otra (contactarse)" }
                });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Capacidad", "Costo", "DestinoId", "Direccion", "Estrellas", "Marca", "Sucursal" },
                values: new object[,]
                {
                    { 1, 100, 1000, 1, "Calle Falsa 123", 5, "Hilton", "Quilmes" },
                    { 2, 100, 1000, 1, "Calle Falsa 123", 5, "Sheraton", "Quilmes" },
                    { 3, 100, 1000, 1, "Calle Falsa 123", 5, "Silken", "Quilmes" },
                    { 4, 100, 1000, 2, "Calle Falsa 123", 5, "Hilton", "Berazategui" },
                    { 5, 100, 1000, 2, "Calle Falsa 123", 5, "Sheraton", "Berazategui" },
                    { 6, 100, 1000, 2, "Calle Falsa 123", 5, "Silken", "Berazategui" },
                    { 7, 100, 1000, 3, "Calle Falsa 123", 5, "Hilton", "Varela" },
                    { 8, 100, 1000, 3, "Calle Falsa 123", 5, "Sheraton", "Varela" },
                    { 9, 100, 1000, 3, "Calle Falsa 123", 5, "Silken", "Varela" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioDestino_DestinoId",
                table: "ComentarioDestino",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioPaqueteHistorico_PaqueteId",
                table: "ComentarioPaqueteHistorico",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursion_DestinoId",
                table: "Excursion",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_DestinoId",
                table: "Hotel",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paquete_HotelId",
                table: "Paquete",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteViaje_PaqueteId",
                table: "PaqueteViaje",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_FormaPagoId",
                table: "Reserva",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_PaqueteId",
                table: "Reserva",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaExcursion_ReservaId",
                table: "ReservaExcursion",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaPasajero_ReservaId",
                table: "ReservaPasajero",
                column: "ReservaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentarioDestino");

            migrationBuilder.DropTable(
                name: "ComentarioPaqueteHistorico");

            migrationBuilder.DropTable(
                name: "Excursion");

            migrationBuilder.DropTable(
                name: "PaqueteExcursion");

            migrationBuilder.DropTable(
                name: "PaqueteViaje");

            migrationBuilder.DropTable(
                name: "ReservaExcursion");

            migrationBuilder.DropTable(
                name: "ReservaPasajero");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "FormaPago");

            migrationBuilder.DropTable(
                name: "Paquete");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Destino");
        }
    }
}
