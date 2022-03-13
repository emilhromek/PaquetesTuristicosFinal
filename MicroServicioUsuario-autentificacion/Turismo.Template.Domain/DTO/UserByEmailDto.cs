using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO
{
    public class UserByEmailDto
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int RollId { get; set; }
        public RollByEmailDto Roll { get; set; }

    }

    public class RollByEmailDto
    {
        public int RollId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    } 
}
