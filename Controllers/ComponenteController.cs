using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class ComponenteController : Controller
    {
        private readonly IComponenteRepository _componenteRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;


        public ComponenteController(IComponenteRepository componenteRepository, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _componenteRepository = componenteRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            int totalComponente = 0;
            var listComponente = _componenteRepository.PaginacaoComponente(null, 1, out totalComponente);
            int totalPagina = (int)Math.Ceiling((double)totalComponente / (double)15);
            totalComponente = totalComponente == 0 ? 1 : totalComponente;

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Compoenente = listComponente;

            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            List<Fornecedor> fornecedor = _fornecedorRepository.GetAll();

            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);
            ViewBag.Fornecedor = fornecedorViewModel;
            


            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ComponenteViewModels viewModel)
        {
            try
            {
                Componente componente = _mapper.Map<Componente>(viewModel);
                componente.Nome = componente.Nome.ToUpper();
                _componenteRepository.Add(componente);
                TempData["Success-Componente"] = "Cadastrado com sucesso!";
                return Redirect("/Componente/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-Componente"] = "Erro ao cadastrar";
                return Redirect("/Componente/Cadastrar");
            }
        }
           
        

        [HttpGet]
        public IActionResult Editar (int id)
        {
            var componente = _componenteRepository.GetById(id);

           ComponenteViewModels viewModel = _mapper.Map<ComponenteViewModels>(componente);

            List<Fornecedor> fornecedor= _fornecedorRepository.GetAll();
            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);

            ViewBag.Fornecedor = fornecedorViewModel;

            return View(viewModel);


        }
        [HttpPost]
        public IActionResult Editar (ComponenteViewModels viewModel)
        {
            try
            {
                Componente componente = _mapper.Map<Componente>(viewModel);
                componente.Nome = componente.Nome.ToUpper();
          
                _componenteRepository.Update(componente);
                TempData["Success-Componente"] = "Editado com sucesso!";
                return Redirect($"/Componente/Editar/{viewModel.Id}");
            }
            catch(Exception ex)
            {
                TempData["Error-Componente"] = "Erro ao editar!";
                return Redirect($"/Componente/Editar/{viewModel.Id}");
            }
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {

            var componente = _componenteRepository.GetById(id);

            ComponenteViewModels ViewModel = _mapper.Map<ComponenteViewModels>(componente);

            List<Fornecedor> fornecedor = _fornecedorRepository.GetAll();
            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);

            ViewBag.Fornecedor = fornecedorViewModel;

            return View(ViewModel);

        }
        public IActionResult PesquisarComponenteIndex(string componente, int paginaAtual)
        {
            int totalComponente = 0;
            var listComponente = _componenteRepository.PaginacaoComponente(componente, paginaAtual, out totalComponente);
            int totalPagina = (int)Math.Ceiling((double)totalComponente / (double)15);
            totalComponente = totalComponente == 0 ? 1 : totalComponente;

            var jsonList = new List<JsonObject>();

            if (listComponente != null)
            {
                foreach (var item in listComponente)
                {
                    var json = new JsonObject();

                    json["Id"] = item.Id;
                    json["Nome"] = item.Nome;
                    json["Quantidade"] = item.Quantidade;
                    json["Data"] = item.Data;
                    json["TotalPagina"] = totalPagina;

                    jsonList.Add(json);
                }
                return Ok(jsonList);
            }
            else
            {
                return NotFound(new ClienteViewModels());
            }

        }
        public IActionResult VerficarLoteAtivoExistente(string componente)
        {
            var componentesLotesAtivos = _componenteRepository.GetAll().Where(c => c.Nome.Replace("ã", "a").Replace("ô", "o").Replace("ç", "c").Replace("á", "a").Replace("ó", "o").ToLower() 
            == componente.Replace("ã", "a").Replace("ç", "c").Replace("á", "a").Replace("ô", "o").Replace("ó", "o").ToLower() && c.Ativo == true);
            if(componentesLotesAtivos == null || componentesLotesAtivos.IsNullOrEmpty())
            {
                return NotFound();
            }
            else
            {
                return Ok(componentesLotesAtivos.Select(c => new
                {
                    nome = c.Nome,
                    lote = c.Lote,
                    data = c.Data.Value.ToString("dd/MM/yyyy"),
                    nf = c.NF,
                    ativo = true
                })); ;

            }
        }
    }
}
