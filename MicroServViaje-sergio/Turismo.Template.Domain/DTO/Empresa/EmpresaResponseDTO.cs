﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.Empresa
{
    public class EmpresaResponseDTO
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
    }
}
