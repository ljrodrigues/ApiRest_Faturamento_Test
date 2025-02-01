using System.ComponentModel.DataAnnotations;

namespace API_Faturamento.Model
{
    public class tabNotaFiscal
    {
        [Key]
        public int id { get; set; }
        public string nfSaida { get; set; }
        public string serieNf { get; set; }
        public string Cliente { get; set; }
        public string Descricao { get; set; }
       // public decimal Valor { get; set; }

    }
}
