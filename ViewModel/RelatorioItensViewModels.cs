using Colex.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Colex.ViewModel
{
    public class RelatorioItensViewModels 
    {

        public int IdRelatorioItens { get; set; }
        public int IdExtintor { get; set; }
        public int IdOServico { get; set; }
        public int? IdComponentes1 { get; set; }
        public int? IdComponentes2 { get; set; }
        public int? IdComponentes3 { get; set; }
        public int? IdComponentes4 { get; set; }
        public DateTime DataProximaRecarga { get; set; }
        public int NivelManutencao { get; set; }
        public bool EnsaioIndPre { get; set; }
        public bool EnsaioVazVal { get; set; }
        public bool InspRosca { get; set; }
        public bool VisualIntacto { get; set; }
        public bool Pintura { get; set; }
        public float PesoCilindroVazio { get; set; }
        public float PesoComAgua { get; set; }
        public float VolumeLitros { get; set; }
        public float CapacidadeMaxima { get; set; }
        public float PressaoTrabalho { get; set; }
        public float PressaoTesteCilindro { get; set; }
        public float PressaoTesteValvula { get; set; }
        public float PressaoTesteMangueira { get; set; }
        public float PressaoTesteManometro { get; set; }
        public float DefInstantanea { get; set; }
        public float DefPermanente { get; set; }
        public float PorcEpEt { get; set; }
        public float TaraGravada { get; set; }
        public float TaraReal { get; set; }
        public float PerdaMassa { get; set; }
        public int MotivoRepro { get; set; }
        public string? LaudoAR { get; set; }
        public long NumSelo { get; set; }
        public bool Reaproveitado { get; set; }



        public virtual ExtintorViewModels Extintor{ get; set; }
        public virtual OsViewModels Os {  get; set; }
        public virtual ComponenteViewModels? Componente1 { get; set; }
        public virtual ComponenteViewModels? Componente2 { get; set; }
        public virtual ComponenteViewModels? Componente3 { get; set; }
        public virtual ComponenteViewModels? Componente4 { get; set; }
    }
}
