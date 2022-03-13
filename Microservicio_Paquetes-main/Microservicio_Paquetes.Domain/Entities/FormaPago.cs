using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class FormaPago // Debe estar fija
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}
