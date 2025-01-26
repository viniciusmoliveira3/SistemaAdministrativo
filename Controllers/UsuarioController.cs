using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Colex.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;


        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper) 
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var list = _usuarioRepository.GetAll();

            List<UsuarioViewModels> viewModel = _mapper.Map<List<UsuarioViewModels>>(list);

            return View(viewModel);
        }


        public IActionResult Cadastrar()
        {
            List<Usuario> list = _usuarioRepository.GetAll();
            List<UsuarioViewModels> usuariosViewModel = _mapper.Map<List<UsuarioViewModels>>(list);

            ViewBag.Usuario = usuariosViewModel;

            return View();

        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioViewModels viewModel)
        {
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(viewModel);
                usuario.Ativo = true;
                _usuarioRepository.Add(usuario);
                TempData["Usuario-Success"] = "Cadastrado com sucesso";
                return Redirect("/Usuario/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Usuario-Error"] = "Erro ao cadastrar";
                return Redirect("/Usuario/Cadastrar");
            }
        }

        public IActionResult Editar(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            UsuarioViewModels viewModel = _mapper.Map<UsuarioViewModels>(usuario);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioViewModels viewmodel)
        {
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(viewmodel);
                _usuarioRepository.Update(usuario);
                TempData["Usuario-Success"] = "Editado com sucesso";
                return Redirect("/Usuario/Editar");

            }
            catch(Exception ex)
            {
                TempData["Usuario-Error"] = "Erro ao alterar";
                return Redirect("/Usuario/Editar");
            }

        }
        public IActionResult Detalhe(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            UsuarioViewModels viewModel = _mapper.Map<UsuarioViewModels>(usuario);

            return View(viewModel);
        }
    }

}
