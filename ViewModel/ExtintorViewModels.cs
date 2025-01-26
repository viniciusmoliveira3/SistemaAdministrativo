using Colex.Models;

namespace Colex.ViewModel
{
    public class ExtintorViewModels
    {
        public int Id { get; set; }
        public string NumeroCilindro { get; set; }
        public int AnoFabricacao { get; set; }
        public int IdMarcaExtintor { get; set; }
        public int EnsaioHidrostatico { get; set; }
        public int ProximoEnsaioHisdrostatico { get; set; }
        public string? NBR { get; set; }
        public string? Projeto { get; set; }
        public string NumPatrimonio { get; set; }
        public string CapacExtintora { get; set; }
        public int IdMateriaPrima { get; set; }
        public int IdCapacidade { get; set; }
        public bool Ativo { get; set; }
        public string Lote { get; set; }
        public long? SeloAnterior { get; set; }




        public virtual MarcaExtintorViewModels MarcaExtintor { get; set; }
        public virtual CapacidadeViewModels Capacidade { get; set; }
        public virtual MateriaPrimaViewModels MateriaPrima { get; set; }
    }
}
