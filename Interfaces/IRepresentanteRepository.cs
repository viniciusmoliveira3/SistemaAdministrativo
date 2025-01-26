using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IRepresentanteRepository : IRepositoryBase<Representante>
    {
        List<RepresentanteViewModels> PaginacaoRepresentante(string representante, int paginaAtual, out int totalRepresentante);
    }
}
