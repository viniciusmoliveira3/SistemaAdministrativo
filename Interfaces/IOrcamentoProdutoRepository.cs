using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IOrcamentoProdutoRepository : IRepositoryBase<OrcamentoProduto>
    {
        List<OrcamentoProdutoViewModels> Adicionar(List<OrcamentoProdutoViewModels> orcamentoProdutoViewModels);
        List<OrcamentoProdutoViewModels> Alterar(List<OrcamentoProdutoViewModels> viewModels);
        List<OrcamentoProdutoViewModels> GetAllComplete();
        List<OrcamentoProdutoViewModels> GetByIdOrcamento(int IdOrcamento);
    }
}
