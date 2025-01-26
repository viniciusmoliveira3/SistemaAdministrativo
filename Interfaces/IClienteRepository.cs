using Colex.Models;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;

namespace Colex.Interfaces
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        List<Cliente> GetAllComplete();
        List<ClienteAutoCompleteViewModels> PesquisarClienteAutoComplete(string nome);
        List<ClienteViewModels> PesquisarCliente(int idCliente, string nome);
        List<ClienteViewModels> PaginacaoCliente(string cliente, int paginaAtual, out int totalCliente);
    }
}
