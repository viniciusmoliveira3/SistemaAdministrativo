using Colex.Models;

namespace Colex.ViewModel
{
    public class MateriaPrimaViewModels
    {
        public int Id { get; set; }
        public string LoteInterno { get; set; }
        public string Nome { get; set; }
        public float Quantidade { get; set; }
        public float QuantidadeAtual { get; set; }
        public int IdFornecedor { get; set; }
        public DateTime Data { get; set; }
        public string NF { get; set; }
        public string Certificado { get; set; }
        public string Batelada { get; set; }
        public bool Ativo { get; set; }


        public virtual FornecedorViewModels Fornecedor { get; set; }
    }
}
