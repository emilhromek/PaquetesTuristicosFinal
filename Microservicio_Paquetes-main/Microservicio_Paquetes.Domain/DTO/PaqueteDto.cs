using System;
using System.Collections.Generic;
using System.Text;


namespace Microservicio_Paquetes.Domain.DTO
{
    public class PaqueteDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaArribo { get; set; }
        public DateTime FechaPartida { get; set; }
        public int Precio { get; set; }
        public int Descuento { get; set; }
        public int Prioridad { get; set; }
        public int EmpleadoId { get; set; }
        public int HotelId { get; set; }
        public int DestinoId { get; set; }
        public List<int> Excursiones { get; set; }
    }
}
