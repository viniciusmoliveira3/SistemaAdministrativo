using Colex.Models;
using Colex.ViewModel;
using Microsoft.CodeAnalysis.Operations;

namespace Colex.Models
{
    public class Os
    {

        public int IdOServico { get; set; }
        public DateTime DataAbertura { get; set; }
        public int? IdRepresentante { get; set; }
        public int IdCliente { get; set; }
        public string NumeroOrdemServico { get; set; }
        public bool Ativo { get; set; }
        //public string? LoteIndPressao { get; set; }
        //public string? LoteMangueirapo { get; set; }
        //public string? LoteMangueiraco2 { get; set; }
        //public string? LoteValvulaBaixa { get; set; }
        //public string? LoteValvulaAlta { get; set; }
        //public string? LoteConjSegurança { get; set; }
        //public string? LoteAtualBc { get; set; }
        //public string? LoteAtualAbc { get; set; }
        





        public virtual Cliente Cliente { get; set; }
        public virtual Representante Representante { get; set; }
        public virtual List<RelatorioItens> RelatorioItens { get; set; }

    }
}
