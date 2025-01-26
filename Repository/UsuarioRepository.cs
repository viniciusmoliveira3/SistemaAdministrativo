using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Colex.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly IMapper _mapper;



        public UsuarioRepository(BaseContext context, IMapper mapper) : base(context)
        {
          _mapper = mapper;
        }

        public UsuarioViewModels AutenticarLogin (string login, string senha)
        {
            var usuarios = this.GetAll();

            var usuario = usuarios.Where(u => u.Login == login && u.Senha == senha).FirstOrDefault();

            return _mapper.Map<UsuarioViewModels>(usuario);
        }
    }
}
