namespace Colex.Models
{
    public class Componente
    {
        public int Id { get; set; }
        public string? Lote { get; set; }
        public DateTime? Data { get; set; }
        public string Nome { get; set; }
        public float? Quantidade { get; set; }
        public float? QuantidadeAtual { get; set; }
        public int? IdFornecedor { get; set; }
        public string? Certificado { get; set; }
        public long? NF { get; set; }
        public bool Ativo { get; set; }



        public virtual Fornecedor Fornecedor { get; set; }

    }
}
