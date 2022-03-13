using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class Paquete
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required, DataType(DataType.DateTime), Column(TypeName = "DateTime")]
        public DateTime FechaSalida { get; set; }
        [Required, DataType(DataType.DateTime), Column(TypeName = "DateTime")]
        public DateTime FechaArribo { get; set; }
        [Required, DataType(DataType.DateTime), Column(TypeName = "DateTime")]
        public DateTime FechaPartida { get; set; }
        [Required, DataType(DataType.DateTime), Column(TypeName = "DateTime")]
        public DateTime FechaLlegada { get; set; }
        [Required]
        public int TotalNoches { get; set; }
        [Required]
        public int Precio { get; set; }
        [Required]
        public int Descuento { get; set; }
        [Required]
        public int EmpleadoId { get; set; }
        [Required]
        public int Prioridad { get; set; }
        [Required]
        public int DestinoId { get; set; }
        [Required]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<ComentarioPaqueteHistorico> ComentarioPaquetesHistorico{get; set; }
        public ICollection<PaqueteViaje> PaqueteViajes { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public string IdentificadorUnicoDePaquete { get; set; }

    }
}
