using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.DTO
{
    public class PasajeroByEmailDto
    {
        public int pasajeroId { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int UserId { get; set; }
        public UserDtoSinPasswordSinRoll User { get; set; }
        public RollByEmailDto Roll { get; set; }
    }
}
