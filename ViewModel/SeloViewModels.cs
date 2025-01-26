using Colex.Models;

namespace Colex.ViewModel
{
    public class SeloViewModels
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string IdFornecedor { get; set; }
        public long NF { get; set; }
        public int NumeroInicial { get; set; }
        public int NumeroFinal { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }


        public virtual FornecedorViewModels Fornecedor { get; set; }
    }
}
