using Colex.Models;

namespace Colex.ViewModel
{
    public class OrcamentoProdutoViewModels
    {
        public int IdOrcamentoProduto { get; set; }
        public string Nome { get; set; }
        public int IdOrcamento { get; set; }
        public int? IdMateriaPrima { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }


        public virtual MateriaPrimaViewModels MateriaPrima { get; set; }
        public virtual OrcamentoViewModels Orcamento { get; set; }
    }
}
