using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface ICapacidadeRepository : IRepositoryBase<Capacidade>
    {
        List<CapacidadeViewModels> PaginacaoCapacidade(string capacidade, int paginaAtual, out int totalCapacidade);
    }
}
