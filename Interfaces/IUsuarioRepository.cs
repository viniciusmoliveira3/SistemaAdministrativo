using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Colex.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
         UsuarioViewModels AutenticarLogin(string login, string senha);
    }
}
