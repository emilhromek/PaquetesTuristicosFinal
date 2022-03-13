using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class Roll
    {
        public int RollId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<User> Usuarios { get; set; }
    }
}
