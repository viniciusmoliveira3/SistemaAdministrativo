using Microsoft.AspNetCore.Mvc;

namespace Colex.Models.Auxiliares
{
    public class ClienteAutoComplete
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }

    }


    
}
