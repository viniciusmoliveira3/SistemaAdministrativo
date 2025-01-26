using System.Globalization;
using System.Numerics;

namespace Colex.Models
{
    public class Orcamento
    {
        public int IdOrcamento { get; set; }
        public string Cliente { get; set; }
        public string Cep { get; set; }
        public string? Endereco { get; set; }
        public string?  Cidade { get; set; }
        public string?  Bairro { get; set; }
        public string? Cnpj { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Estado { get; set; }
        public string? Comprador { get; set; }
        public string? Vendedor { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public string? Nota {  get; set; }
        public string? Observacao { get; set; }
        public decimal? ValorFinal {  get; set; }


    }
}
