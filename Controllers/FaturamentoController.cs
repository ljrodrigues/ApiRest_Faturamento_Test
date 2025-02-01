using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using API_Faturamento.Model;
using System.Data.Common;
using API_Faturamento.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Client;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;


namespace API_Faturamento.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class FaturamentoController : ControllerBase
    {
        private readonly DataContext _context = null;

        public FaturamentoController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("IncluirNotaFiscal")]
        public IActionResult IncluirNotaFiscal(String NfSaida, String SerieNf, String nomeCliente, String detDescricao, float totValor)
        {
            tabNotaFiscal newNF = new tabNotaFiscal
            {
                nfSaida = NfSaida,
                serieNf = SerieNf,
                Cliente = nomeCliente,
                Descricao = detDescricao
            };
            try
            {
                _context.tabNotaFiscal.Add(newNF);
                _context.SaveChanges();

                return Ok("Nota Fiscal Inserida com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao inserir Nota Fiscal. " + ex.Message + " - " + ex.InnerException);
            }

        }

        [HttpPost("ConsultaNotaFiscal")]
        public IActionResult ConsultaNotaFiscal(String NfSaida)
        {
            var nf = _context.tabNotaFiscal.Where(n => n.nfSaida.Equals(NfSaida)).ToList();
            if (nf.Count > 0 )
            {
                string retorno = "";
                foreach ( var n in nf )
                {
                    retorno += String.Format("Nota Fiscal: {0} \nSerie: {1} \nCLiente: {2} \nDescricao: {3}\n\n", n.nfSaida, n.serieNf, n.Cliente, n.Descricao);
                }
                
                return Ok(retorno);
            }
            else
            {
                return NotFound("Nota Fiscal não Existe na base de Dados");
            }


        }

    }
}
