using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO
{
    public class UserDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Roll { get; set; }
    }
    public class UserDtoSinPassword
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Roll { get; set; }
    }
    public class UserDtoSinPasswordSinRoll
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
    }
}
