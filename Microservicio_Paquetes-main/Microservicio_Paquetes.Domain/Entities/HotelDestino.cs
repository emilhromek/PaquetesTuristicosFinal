using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class HotelDestino
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PaqueteId { get; set; }
        public Paquete Paquete { get; set; }
        [Required]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
