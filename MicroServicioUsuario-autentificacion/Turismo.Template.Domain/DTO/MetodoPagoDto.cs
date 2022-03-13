using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.DTO
{
    public class MetodoPagoDto
    {
        public string Nombre { get; set; }

    }
    public class MetodoPagoDtoById
    {
        public int MetodoPagoId { get; set; }
        public string Nombre { get; set; }

    }
}
