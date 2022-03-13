using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class MetodoPago
    {
        public int MetodoPagoId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Pasajero> Pasajeros { get; set; }
    }
}
