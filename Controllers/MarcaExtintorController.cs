using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class MarcaExtintorController : Controller
    {
        private readonly IMarcaExintorRepository _marcaExintorRepository;
        private readonly IMapper _mapper;

        public MarcaExtintorController(IMarcaExintorRepository marcaExintorRepository, IMapper mapper)
        {
            _marcaExintorRepository = marcaExintorRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            int totalMarca = 0;
            var listMarca = _marcaExintorRepository.PaginacaoMarcaExtintor(null, 1, out totalMarca);
            int totalPagina = (int)Math.Ceiling((double)totalMarca / (double)15);
            totalMarca = totalMarca == 0 ? 1 : totalMarca;


            ViewBag.TotalPagina = totalPagina;
            ViewBag.MarcaExtintor = listMarca;

            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(MarcaExtintorViewModels viewModel)
        {
            try
            {
                MarcaExtintor marca = _mapper.Map<MarcaExtintor>(viewModel);
                marca.Nome = marca.Nome.ToUpper();
                _marcaExintorRepository.Add(marca);
                TempData["Success-MarcaExtintor"] = "Cadastrado com sucesso!";
                return Redirect("/MarcaExtintor/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-MarcaExtintor"] = "Erro ao cadastrar!";
                return Redirect("/MarcaExtintor/Cadastrar");
            }
         
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var marca = _marcaExintorRepository.GetById(id);
            MarcaExtintorViewModels viewModel = _mapper.Map<MarcaExtintorViewModels>(marca);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(MarcaExtintorViewModels viewModel)
        {
            try
            {
                MarcaExtintor marca = _mapper.Map<MarcaExtintor>(viewModel);
                marca.Nome = marca.Nome.ToUpper();
                _marcaExintorRepository.Update(marca);
                TempData["Success-MarcaExtintor"] = "Editado com sucesso!";
                return Redirect($"/MarcaExtintor/Editar/{viewModel.Id}");
            }
            catch(Exception ex)
            {
                TempData["Error-MarcaExtintor"] = "Erro ao editar!";
                return Redirect($"/MarcaExtintor/Editar/{viewModel.Id}");
            }
            
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            MarcaExtintor marca = _marcaExintorRepository.GetById(id);
            MarcaExtintorViewModels viewModel = _mapper.Map<MarcaExtintorViewModels>(marca);

            return View(viewModel);
        }
        public IActionResult PesquisarMarcaExtintorIndex(string marca, int paginaAtual)
        {
            int totalMarca = 0;
            var listMarca = _marcaExintorRepository.PaginacaoMarcaExtintor(marca, paginaAtual, out totalMarca);
            int totalPagina = (int)Math.Ceiling((double)totalMarca / (double)15);
            totalMarca = totalMarca == 0 ? 1 : totalMarca;

            var jsonList = new List<JsonObject>();

            if (listMarca != null)
            {
                foreach (var item in listMarca)
                {
                    var json = new JsonObject();

                    json["Id"] = item.Id;
                    json["Nome"] = item.Nome;
                    json["TotalPagina"] = totalPagina;

                    jsonList.Add(json);
                }
                return Ok(jsonList);
            }
            else
            {
                TempData["Error-OS"] = "Não possível localizar a ordem de serviço, cadastre uma nova!";
                return NotFound(new MarcaExtintorViewModels());
            }

        }
    }
}
