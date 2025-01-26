using Colex.Models;
using Colex.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Colex.Interfaces
{
    public interface IRelatorioItensRepository : IRepositoryBase<RelatorioItens>
    {
        List<RelatorioItensViewModels> Adicionar(List<RelatorioItensViewModels> viewModels);
        List<RelatorioItensViewModels> GetRelatorioByOsid(int idOs);
        List<RelatorioItens> GetAllComplete();
        List<RelatorioItens> GetAllCompleteCliente();
        List<RelatorioItens> GetExtintorByCliente(int idCliente, int data);
        RelatorioItens GetExtintorBySelo(long selo);
        List<RelatorioItensViewModels> GetExtintiorByLote(int carga, string lote);
        List<RelatorioItensViewModels> GetRelaotrioItensByNumeroOs(string numeroOs, int data);
        RelatorioItensViewModels GetEtiquetasByNumeroExtintior(string numeroExtintor, int data);
        List<RelatorioItensViewModels> GetRelatorioExtintor(string numeroExtintor);
        RelatorioItensViewModels Adicionar(RelatorioItensViewModels viewModels);
    }
}
