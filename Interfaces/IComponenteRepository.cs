using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IComponenteRepository : IRepositoryBase<Componente>
    {
        List<Componente> GetAllComplete();
        List<ComponenteViewModels> PaginacaoComponente(string componente, int paginaAtual, out int totalComponente);
        void AlterarEstoqueComponente(int idComponente);
    }
}
