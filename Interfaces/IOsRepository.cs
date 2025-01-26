
using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IOsRepository : IRepositoryBase<Os>
    {
        List<Os> GetAllComplete();
        OsViewModels Adicionar(OsViewModels viewModels);
        OsViewModels GetRelatorioById(int id);
        List<OsViewModels> PaginacaoOs(string nome, string numeroOs, int paginaAtual, out int totalOs);
        OsViewModels Editar(OsViewModels viewModels);
    }
}
