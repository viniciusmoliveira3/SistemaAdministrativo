using AutoMapper;
using Colex.Interfaces;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Colex.Controllers
{
    

    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public LoginController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
                return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModels loginViewModels)
        {
            UsuarioViewModels usuario = null;

            usuario = _usuarioRepository.AutenticarLogin(loginViewModels.Login, loginViewModels.Senha);




            if (usuario != null)
            {
                if (usuario.Ativo)
                {
                    if (loginViewModels.Login == usuario.Login && loginViewModels.Senha == usuario.Senha)
                    {
                        HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                        HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                    }
                }
                return Redirect("/");
            }
            else
            {

                TempData["Error-Login"] = "Usuário inválido";
                return Redirect("/Login/Login");
            }
           
        }
      
    }

}
