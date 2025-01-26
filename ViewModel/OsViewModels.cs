using Colex.Models;

namespace Colex.ViewModel
{
    public class OsViewModels
    {
        public int IdOServico { get; set; }
        public DateTime DataAbertura { get; set; }
        public int IdRepresentante { get; set; }
        public int IdCliente { get; set; }
        public string NumeroOrdemServico { get; set; }
        public bool SeguirRelatorio { get; set; }
        public string? LoteIndPressao { get; set; }
        public string? LoteMangueirapo { get; set; }
        public string? LoteMangueiraco2 { get; set; }
        public string? LoteValvulaBaixa { get; set; }
        public string? LoteValvulaAlta { get; set; }
        public string? LoteConjSegurança { get; set; }
        public string? LoteAtualBc { get; set; }
        public string? LoteAtualAbc { get; set; }
        public string? LoteAtualCo2 { get; set; }
        public string? LoteAtualEm { get; set; }
        public string ListRelItensExclusao { get; set; }





        public virtual ClienteViewModels Cliente { get; set; }
        public virtual RepresentanteViewModels Representante { get; set; }
        public virtual List<RelatorioItensViewModels> RelatorioItens { get; set; }
        
    }
}
