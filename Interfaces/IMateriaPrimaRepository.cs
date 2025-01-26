using Colex.Mapping;
using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IMateriaPrimaRepository : IRepositoryBase<MateriaPrima>
    {
        List<MateriaPrima> GetAllComplete();
        List<MateriaPrimaViewModels> PaginacaoMateriaPrima(string materia, int paginaAtual, out int totalMateria);
        void AlterarEstoqueMateriaPrima(int idMateriaPrima, int idCapacidade);
    }
}
