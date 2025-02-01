using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace API_Faturamento.Model
{
    public class tabUsuarios
    {
        [Key]
        public int idUsuario { get; set; }   
        public string? nomeUsuario { get; set;}
        public string? emailUsuario { get; set;}
        public string? senhaUsuario { get; set; }




    }
}
