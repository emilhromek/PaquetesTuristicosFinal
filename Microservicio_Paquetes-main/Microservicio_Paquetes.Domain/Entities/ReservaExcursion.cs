using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class ReservaExcursion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ReservaId { get; set; }
        public int ExcursionId { get; set; }
    }
}
