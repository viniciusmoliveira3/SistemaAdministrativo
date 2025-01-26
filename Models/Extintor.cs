namespace Colex.Models
{
    public class Extintor
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
        public string CapacExtintora {  get; set; }
        public int IdMateriaPrima { get; set; }
        public int IdCapacidade { get; set; }
        public bool Ativo { get; set; }
        public string Lote { get; set; }
        public long SeloAnterior { get; set; }



        public virtual MarcaExtintor MarcaExtintor { get; set;}
        public virtual Capacidade Capacidade { get; set; }
        public virtual MateriaPrima MateriaPrima { get; set; }



    }
}
