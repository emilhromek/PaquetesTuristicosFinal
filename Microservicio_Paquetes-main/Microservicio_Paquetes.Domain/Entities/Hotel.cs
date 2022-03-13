using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class Hotel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Sucursal { get; set; }
        [Required]
        public int Estrellas { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int DestinoId { get; set; }
        public Destino Destino { get; set; }
        [Required]
        public int Capacidad { get; set; }
        [Required]
        public int Costo { get; set; }
        public ICollection<Paquete> Paquetes { get; set; }
    }
}
