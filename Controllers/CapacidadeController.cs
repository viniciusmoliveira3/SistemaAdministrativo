using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class CapacidadeController : Controller
    {
        private readonly ICapacidadeRepository _capacidadeRepository;
        private readonly IMapper _mapper;

        public CapacidadeController(ICapacidadeRepository capacidadeRepository, IMapper mapper)
        {
            _capacidadeRepository = capacidadeRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            int totalCapacidade = 0;
            var listCapacidade = _capacidadeRepository.PaginacaoCapacidade(null, 1, out totalCapacidade);
            int totalPagina = (int)Math.Ceiling((double)totalCapacidade / (double)15);
            totalCapacidade = totalCapacidade == 0 ? 1 : totalCapacidade;

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Capacidade = listCapacidade;          

            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(CapacidadeViewModels viewModel)
        {
            try
            {
                Capacidade capacidade = _mapper.Map<Capacidade>(viewModel);

                _capacidadeRepository.Add(capacidade);
                TempData["Success-Capacidade"] = "Cadastrado com sucesso";
                return Redirect("/Capacidade/Cadastrar");
            }
            catch(Exception ex)
            {
                TempData["Error-Capacidade"] = "Erro ao cadastrar";
                return Redirect("/Capacidade/Cadastrar");
            }
          
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var capacidade = _capacidadeRepository.GetById(id);

            CapacidadeViewModels viewModel = _mapper.Map<CapacidadeViewModels>(capacidade);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(CapacidadeViewModels viewModel)
        {
            try
            {
                Capacidade capacidade = _mapper.Map<Capacidade>(viewModel);
                _capacidadeRepository.Update(capacidade);
                TempData["Success-Capacidade"] = "Editado com sucesso";
                return Redirect($"/Capacidade/Editar/{viewModel.Id}");
            }
            catch(Exception ex)
            {
                TempData["Error-Capacidade"] = "Erro ao editar";
                return Redirect($"/Capacidade/Editar/{viewModel.Id}");
            }
            
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var capacidade = _capacidadeRepository.GetById(id);
            CapacidadeViewModels viewModel = _mapper.Map<CapacidadeViewModels>(capacidade);

            return View(viewModel);
        }

        public IActionResult PesquisarCapacidadeIndex(string capacidade, int paginaAtual)
        {
            int totalCapacidade = 0;
            var listCapacidade = _capacidadeRepository.PaginacaoCapacidade(capacidade, paginaAtual, out totalCapacidade);
            int totalPagina = (int)Math.Ceiling((double)totalCapacidade / (double)15);
            totalCapacidade = totalCapacidade == 0 ? 1 : totalCapacidade;

            var jsonList = new List<JsonObject>();

            if (listCapacidade != null)
            {
                foreach (var capa in listCapacidade)
                {
                    var json = new JsonObject();

                    json["IdCapacidade"] = capa.Id;
                    json["Capacidade"] = capa.CapacidadeCarga;
                    json["TotalPagina"] = totalPagina;

                    jsonList.Add(json);
                }
                return Ok(jsonList);
            }
            else
            {
                return NotFound(new CapacidadeViewModels());
            }

        }
    }
}
