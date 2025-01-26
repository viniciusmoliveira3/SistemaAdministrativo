namespace Colex.Models
{
    public class OrcamentoProduto
    {
        public int IdOrcamentoProduto { get; set; }
        public string? Nome { get; set; }
        public int IdOrcamento {  get; set; }
        public int? IdMateriaPrima { get; set; }
        public int? Quantidade {  get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? ValorTotal { get; set; }


        public virtual MateriaPrima MateriaPrima { get; set; }
        public virtual Orcamento Orcamento { get; set; }
    }
}
