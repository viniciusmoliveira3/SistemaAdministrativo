using Colex.Models;
using Colex.ViewModel;

namespace Colex.Interfaces
{
    public interface IExtintorRepository : IRepositoryBase<Extintor>
    {
        List<Extintor> GetAllComplete();
        List<ExtintorViewModels> PesquisarExtintor(string codigoExtintor);
        ExtintorViewModels PesquisarExtintor(int idExintor, string numeroCilindro);
        ExtintorViewModels Alterar(ExtintorViewModels viewModels);
        ExtintorViewModels GetByIdExtintor(int idExintor);
        List<ExtintorViewModels> PaginacaoExtintor(string numeroExtintor, int paginaAtual, out int totalExtintor);
        //List<ExtintorViewModels> GetExtintiorByLote(int carga, string lote);
    }
}
