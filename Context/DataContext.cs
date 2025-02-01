using API_Faturamento.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace API_Faturamento.Context
{
    public class DataContext : DbContext
    {


        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<tabUsuarios> tabUsuarios { get; set; }
        public DbSet<tabNotaFiscal> tabNotaFiscal { get; set; }


    }
}
