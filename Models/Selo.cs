namespace Colex.Models
{
    public class Selo
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdFornecedor { get; set; }
        public long NF { get; set; }
        public long NumeroInicial { get; set; }
        public long NumeroFinal { get; set; }
        public long Quantidade { get; set; }
        public bool Ativo { get; set; }


        public virtual Fornecedor Fornecedor { get; set; }
    }
}
