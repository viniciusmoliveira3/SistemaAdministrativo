using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class RepresentanteController : Controller
    {
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IMapper _mapper;

        public RepresentanteController(IRepresentanteRepository representanteRepository, IMapper mapper)
        {
            _representanteRepository = representanteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {

            int totalRepresentante = 0;
            var listRepresentante = _representanteRepository.PaginacaoRepresentante(null, 1, out totalRepresentante);
            int totalPagina = (int)Math.Ceiling((double)totalRepresentante / (double)15);
            totalRepresentante = totalRepresentante == 0 ? 1 : totalRepresentante;

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Representante = listRepresentante;

            return View();


        }

        [HttpPost]
        public IActionResult Cadastrar(RepresentanteViewModels viewModel)
        {
            try
            {
                Representante representante = _mapper.Map<Representante>(viewModel);
                representante.Ativo = true;
                _representanteRepository.Add(representante);
                TempData["Success-Representante"] = "Cadastrado com sucesso!";
                return Redirect("/Representante/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-Representante"] = "Erro ao cadastrar!";
                return Redirect("/Representante/Cadastrar");
            }
         
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editar(RepresentanteViewModels viewmodel)
        {
            try
            {
                Representante representamnte = _mapper.Map<Representante>(viewmodel);
                _representanteRepository.Update(representamnte);
                TempData["Success-Representante"] = "Editado com sucesso!";
                return Redirect($"/Representante/Editar/{viewmodel.Id}");
            }
            catch(Exception ex)
            {
                TempData["Error-Representante"] = "Erro ao editar!";
                return Redirect($"/Representante/Editar/{viewmodel.Id}");
            }
            
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var representante = _representanteRepository.GetById(id);
            RepresentanteViewModels viewmodel = _mapper.Map<RepresentanteViewModels>(representante);
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var detalhe = _representanteRepository.GetById(id);
            RepresentanteViewModels viewmodel = _mapper.Map<RepresentanteViewModels>(detalhe);
            return View(viewmodel);
        }
        public IActionResult PesquisarRepresentanteIndex(string representante, long numeroOs, int paginaAtual)
        {
            int totalRepresentante = 0;
            var listRepresentante = _representanteRepository.PaginacaoRepresentante(representante, paginaAtual, out totalRepresentante);
            int totalPagina = (int)Math.Ceiling((double)totalRepresentante / (double)15);
            totalRepresentante = totalRepresentante == 0 ? 1 : totalRepresentante;

            var jsonList = new List<JsonObject>();

            if (listRepresentante != null)
            {
                foreach (var item in listRepresentante)
                {
                    var json = new JsonObject();

                    json["Id"] = item.Id;
                    json["NomeFantasia"] = item.NomeFantasia;
                    json["Telefone"] = item.Telefone;
                    json["Email"] = item.Email;
                    json["TotalPagina"] = totalPagina;

                    jsonList.Add(json);
                }
                return Ok(jsonList);
            }
            else
            {
                TempData["Error-OS"] = "Não possível localizar a ordem de serviço, cadastre uma nova!";
                return NotFound(new RepresentanteViewModels());
            }

        }
    }
}
