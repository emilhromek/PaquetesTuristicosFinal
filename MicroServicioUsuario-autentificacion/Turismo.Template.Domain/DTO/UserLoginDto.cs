using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginRollDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int roll { get; set; }
    }
}
