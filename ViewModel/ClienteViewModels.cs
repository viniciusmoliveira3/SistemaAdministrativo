using Colex.Models;

namespace Colex.ViewModel
{


    public class ClienteViewModels
    {
        public int Id { get; set; }
        public int CEP { get; set; }
        public int IdRepresentante { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string EnderecoSocial { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public long CNPJ { get; set; }
        public long InscricaoEstadual { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
        public bool Ativo { get; set; }



        public virtual RepresentanteViewModels Representante { get; set; }
    }
}

