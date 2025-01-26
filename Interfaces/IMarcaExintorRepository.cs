using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IMarcaExintorRepository : IRepositoryBase<MarcaExtintor>
    {
        List<MarcaExtintorViewModels> PaginacaoMarcaExtintor(string marca, int paginaAtual, out int totalMarca);
    }
}
