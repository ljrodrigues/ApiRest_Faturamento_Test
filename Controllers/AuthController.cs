using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using API_Faturamento.Model;
using API_Faturamento.Context;
using Microsoft.EntityFrameworkCore;
using API_Faturamento.Controllers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context = null;

        public AuthController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        //private string GerarToken(tabUsuarios usuario)
        private string GerarToken(string emailUsuario, string senhaUsuario)
        {
            var claims = new[]
            {
                new Claim("senhaUsuario", senhaUsuario),
                new Claim("emailUsuario", emailUsuario),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secretkey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:issuer"],
                _configuration["Jwt:audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30), // Tempo de expiração do token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("login")]
        //public IActionResult Login([FromBody] tabUsuarios login)
        public IActionResult Login(string emailUsuario, string senhaUsuario)
        {

            var user = _context.tabUsuarios.Where(u => u.emailUsuario.Equals(emailUsuario) && u.senhaUsuario.Equals(getHashMD5(senhaUsuario).ToString())).ToList();

            if (user.Count <= 0)
            {
                return BadRequest("Usuário ou Senha inválidos");
            }

            //var token = GerarToken(login);
            var token = GerarToken(emailUsuario, senhaUsuario);

            return Ok(new { token });
        }

        [HttpGet]
        private string getHashMD5(string input)
        {

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Converter byte array para string hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

    }
}
