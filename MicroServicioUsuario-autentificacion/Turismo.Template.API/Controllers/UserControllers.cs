using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.API.Controllers
{
    [EnableCors]

    [Authorize]
    [Route("api/User")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IUserService _service;
        public UserControllers (IUserService service)
        {
            _service = service;
        }

        // GET: Usuarios
        [HttpGet]
        [ProducesResponseType(typeof(List<UserByEmailDto>), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] string email)
        {
            try
            {
                return new JsonResult(_service.GetUserByEmail(email)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST: Cargar usuario
        // nombre, apelido, email, password
        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            try
            {
                return new JsonResult(_service.CreateUser(user)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {

            try
            {
                return new JsonResult(_service.getUserId(id)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteId(int id)
        {
            try
            {
                var user = _service.getUserId(id);
                if (user != null)
                {
                    _service.deleteUserId(id);
                    return new JsonResult(user) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(user) { StatusCode = 404 };
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UserDto user)
        {
            try
            {
                return new JsonResult(_service.Update(id, user)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto model)
        {

            try
            {
                model.Roll = 3;
                // create user
                var user =_service.CreateUser(model);
                return Ok(new {
                    Id = user.UserId,
                    Username = user.Email,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                });
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLoginDto login)
        {
            var user = _service.Authenticate(login.Email, login.Password);

            if (user == null)
                return BadRequest(new { message = "Usuario o Password son incorrectos " });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("xecretKeywqejane");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.UserId,
                Username = user.Email,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Token = tokenString
            });
        }

    }
}
