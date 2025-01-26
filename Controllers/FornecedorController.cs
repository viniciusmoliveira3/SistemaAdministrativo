using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class FornecedorController : Controller
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;


        public FornecedorController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int totalFornecedor = 0;
            var listFornecedor = _fornecedorRepository.PaginacaoFornecedor(null, 1, out totalFornecedor);
            int totalPagina = (int)Math.Ceiling((double)totalFornecedor / (double)15);
            totalFornecedor = totalFornecedor == 0 ? 1 : totalFornecedor;

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Fornecedor = listFornecedor;

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(FornecedorViewModels viewModels)
        {
            try
            {
                Fornecedor fornecedor = _mapper.Map<Fornecedor>(viewModels);
                fornecedor.Ativo = true;
                fornecedor.NomeFantasia = fornecedor.NomeFantasia.ToUpper();
                _fornecedorRepository.Add(fornecedor);
                TempData["Success-Fornecedor"] = "Cadastrado com sucesso!";
                return Redirect("/Fornecedor/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-Fornecedor"] = "Erro ao cadastrar!";
                return Redirect("/Fornecedor/Cadastrar");
            }
        }
            

        

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Editar(FornecedorViewModels viewModel)
        {
            try
            {
                Fornecedor fornecedor = _mapper.Map<Fornecedor>(viewModel);
                fornecedor.NomeFantasia = fornecedor.NomeFantasia.ToUpper();

                _fornecedorRepository.Update(fornecedor);
                TempData["Success-Fornecedor"] = "Editado com sucesso!";
                return Redirect($"/Fornecedor/Editar/{viewModel.Id}");
            }
            catch(Exception ex)
            {
                TempData["Error-Fornecedor"] = "Erro ao editar!";
                return Redirect($"/Fornecedor/Editar/{viewModel.Id}");
            }
           

        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var Fornecedor = _fornecedorRepository.GetById(id);
            FornecedorViewModels viewmodel = _mapper.Map<FornecedorViewModels>(Fornecedor);
            return View(viewmodel);

        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var detalhe = _fornecedorRepository.GetById(id);
            FornecedorViewModels viewModel = _mapper.Map<FornecedorViewModels>(detalhe);
            return View(viewModel);
        }
        public IActionResult PesquisarFornecedorIndex(string fornecedor, int paginaAtual)
        {
            int totalFornecedor = 0;
            var listFornecedor = _fornecedorRepository.PaginacaoFornecedor(fornecedor, paginaAtual, out totalFornecedor);
            int totalPagina = (int)Math.Ceiling((double)totalFornecedor / (double)15);
            totalFornecedor = totalFornecedor == 0 ? 1 : totalFornecedor;

            var jsonList = new List<JsonObject>();

            if (listFornecedor != null)
            {
                foreach (var item in listFornecedor)
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
                return NotFound(new FornecedorViewModels());
            }

        }

    }

}

