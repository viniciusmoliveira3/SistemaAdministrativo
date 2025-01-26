using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IFornecedorRepository : IRepositoryBase<Fornecedor>
    {

        List<FornecedorViewModels> PaginacaoFornecedor(string fornecedor, int paginaAtual, out int totalFornecedor);
    }
}
