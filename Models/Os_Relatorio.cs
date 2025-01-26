using Microsoft.AspNetCore.Mvc;

namespace Colex.Models
{
    public class Os_Relatorio 
    {   
        public int IdOsRelatorio { get; set; }
        public int IdOServico { get; set; }
        public int IdRelatorioItens { get; set; }
        public string Relatorio { get; set; }



        public virtual Os Os { get; set; }
        public virtual RelatorioItens RelatorioItens { get;set; }
    }
}
