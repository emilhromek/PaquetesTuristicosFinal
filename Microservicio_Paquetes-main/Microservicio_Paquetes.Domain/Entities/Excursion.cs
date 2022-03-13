using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class Excursion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Precio { get; set; }
        [Required]
        public int Duracion { get; set; } // horas
        [Required]
        public int DestinoId { get; set; }
        public Destino Destino { get; set; }

    }
}
