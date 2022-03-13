using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(70)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RollId {get;set;}
        public Roll Roll { get; set; }

    }
}
