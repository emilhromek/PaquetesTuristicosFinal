using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class Destino
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Lugar { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Atractivo { get; set; }
        [Required]
        public string Historia { get; set; }
        [Required]
        public int HorasDeViaje { get; set; }
        public string Imagen { get; set; }
        public ICollection<Excursion> Excursiones { get; set; }
        public ICollection<ComentarioDestino> Comentarios { get; set; }
        public ICollection<Hotel> Hoteles { get; set; }
    }
}
