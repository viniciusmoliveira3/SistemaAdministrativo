using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Colex.Controllers
{
    public class SeloController : Controller
    {
        private readonly ISeloRepository _seloRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public SeloController(ISeloRepository seloRepository, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _seloRepository = seloRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var selo = _seloRepository.GetAllComplete();
            List<SeloViewModels> viewModels = _mapper.Map<List<SeloViewModels>>(selo);

            return View(viewModels);
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
        public IActionResult Cadastrar(SeloViewModels viewModel)
        {
            try
            {
                Selo selo = _mapper.Map<Selo>(viewModel);
                selo.Ativo = true;
                _seloRepository.Add(selo);
                TempData["Success-Selo"] = "Cadastrado com sucesso!";
                return Redirect("/Selo/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-Selo"] = "Erro ao cadastrar!";
                return Redirect("/Selo/Cadastrar");
            }
        

        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var selo = _seloRepository.GetById(id);

            SeloViewModels viewModel = _mapper.Map<SeloViewModels>(selo);

            List<Fornecedor> fornecedor = _fornecedorRepository.GetAll();
            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);

            ViewBag.Fornecedor = fornecedorViewModel;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(SeloViewModels viewModel)
        {
            try
            {
                Selo selo = _mapper.Map<Selo>(viewModel);
                _seloRepository.Update(selo);
                TempData["Success-Selo"] = "Editado com sucesso!";
                return Redirect($"/Selo/Editar/{viewModel.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error-Selo"] = "Erro ao editar!";
                return Redirect($"/Selo/Editar/{viewModel.Id}");
            }


        }

        [HttpGet]
        public IActionResult Detalhe (int id)
        {
            var selo = _seloRepository.GetById(id);

            SeloViewModels viewModel = _mapper.Map<SeloViewModels>(selo);

            List<Fornecedor> fornecedor = _fornecedorRepository.GetAll();
            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);

            ViewBag.Fornecedor = fornecedorViewModel;

            return View(viewModel);

        }
    }
}
