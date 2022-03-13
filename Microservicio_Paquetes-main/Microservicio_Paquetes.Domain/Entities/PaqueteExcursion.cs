using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class PaqueteExcursion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PaqueteId { get; set; }
        public int ExcursionId { get; set; }
    }
}
