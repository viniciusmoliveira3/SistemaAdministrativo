using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IOrcamentoRepository : IRepositoryBase<Orcamento>
    {
        OrcamentoViewModels Adicionar(OrcamentoViewModels model);
        List<OrcamentoViewModels> GetAllComplete();
        OrcamentoViewModels Alterar(OrcamentoViewModels model);
        List<OrcamentoViewModels> PaginacaoOrcamento(string nome, long numeroOrcamento, int paginaAtual, out int totalOrcamento);
    }
}
