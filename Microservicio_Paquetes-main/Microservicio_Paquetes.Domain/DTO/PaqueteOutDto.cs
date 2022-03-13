using System;
using System.Collections.Generic;
using System.Text;


namespace Microservicio_Paquetes.Domain.DTO
{
    public class PaqueteOutDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FechaSalida { get; set; }
        public string FechaSalidaSinFormato { get; set; }
        public string FechaArribo { get; set; }
        public string FechaArriboSinFormato { get; set; }
        public string FechaPartida { get; set; }
        public string FechaPartidaSinFormato { get; set; }
        public string FechaLlegada { get; set; }
        public string FechaLlegadaSinFormato { get; set; }
        public int TotalNoches { get; set; }
        public int Precio { get; set; }
        public int Descuento { get; set; }
        public int Prioridad { get; set; }
        public int DestinoId { get; set; }
        public DestinoOutDto Destino {get; set;} 
        public int HotelId { get; set; }
        public HotelOutDto Hotel {get; set;}
        public List<ExcursionOutDto> ExcursionOutDtos { get; set; }
        public string IdentificadorUnicoDePaquete { get; set; }
    }
}
