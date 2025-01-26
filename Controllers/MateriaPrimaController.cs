using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class MateriaPrimaController : Controller
    {

        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public MateriaPrimaController(IMateriaPrimaRepository materiaPrimaRepository, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _materiaPrimaRepository = materiaPrimaRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            int totalMateria = 0;
            var listMateria = _materiaPrimaRepository.PaginacaoMateriaPrima(null, 1, out totalMateria);
            int totalPagina = (int)Math.Ceiling((double)totalMateria / (double)15);
            totalMateria = totalMateria == 0 ? 1 : totalMateria;

            ViewBag.TotalPagina = totalPagina;
            ViewBag.MateriaPrima = listMateria;

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
        public IActionResult Cadastrar(MateriaPrimaViewModels viewModel)
        {
            try
            {
                MateriaPrima materia = _mapper.Map<MateriaPrima>(viewModel);
                materia.IdFornecedor = materia.IdFornecedor == 0 ? null : materia.IdFornecedor;
                materia.Nome = materia.Nome.ToUpper();
                materia.Certificado = materia.Certificado != null ? materia.Certificado.ToUpper() : null;
                materia.Batelada = materia.Batelada != null ? materia.Batelada.ToUpper() : null;
                _materiaPrimaRepository.Add(materia);
                TempData["Success-MateriaPrima"] = "Cadastrado com sucesso!";
                return Redirect("/MateriaPrima/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-MateriaPrima"] = "Erro ao cadastrar!";
                return Redirect("/MateriaPrima/Cadastrar");
            }
           
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var materia = _materiaPrimaRepository.GetById(id);
            MateriaPrimaViewModels viewModel = _mapper.Map<MateriaPrimaViewModels>(materia);

            List<Fornecedor> fornecedor = _fornecedorRepository.GetAll();
            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);

            ViewBag.Fornecedor = fornecedorViewModel;

            return View(viewModel);  
        }

        [HttpPost]
        public IActionResult Editar(MateriaPrimaViewModels viewModel)
        {
            try
            {
                MateriaPrima materia = _mapper.Map<MateriaPrima>(viewModel);
                materia.Nome = materia.Nome.ToUpper();
                materia.Certificado = materia.Certificado != null ? materia.Certificado.ToUpper() : null;
                materia.Batelada = materia.Batelada != null ? materia.Batelada.ToUpper() : null;
                _materiaPrimaRepository.Update(materia);
                TempData["Success-MateriaPrima"] = "Editado com sucesso!";
                return Redirect($"/MateriaPrima/Editar/{viewModel.Id}");
            }   
            catch (Exception ex)
            {
                TempData["Error-MateriaPrima"] = "Erro ao editar!";
                return Redirect($"/MateriaPrima/Editar/{viewModel.Id}");
            }
           
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var materia = _materiaPrimaRepository.GetById(id);

            MateriaPrimaViewModels viewModel = _mapper.Map<MateriaPrimaViewModels>(materia);
            
            List<Fornecedor> fornecedor= _fornecedorRepository.GetAll();
            List<FornecedorViewModels> fornecedorViewModel = _mapper.Map<List<FornecedorViewModels>>(fornecedor);

            ViewBag.Fornecedor = fornecedorViewModel;

            return View(viewModel);
        }
        public IActionResult PesquisarMateriaPrimaIndex(string materia, int paginaAtual)
        {
            int totalMateria = 0;
            var listMateria = _materiaPrimaRepository.PaginacaoMateriaPrima(materia, paginaAtual, out totalMateria);
            int totalPagina = (int)Math.Ceiling((double)totalMateria / (double)15);
            totalMateria = totalMateria == 0 ? 1 : totalMateria;

          
            if (listMateria != null)
            {
              return Ok(listMateria);
            }
            else
            {
                TempData["Error-OS"] = "Não possível localizar a ordem de serviço, cadastre uma nova!";
                return NotFound(new MateriaPrimaViewModels());
            }

        }
        public IActionResult VerficarLoteAtivoExistente(string materiaPrima)
        {
            var materiaPrimaAtivo = _materiaPrimaRepository.GetAll().Where(c => c.Nome.Replace("ã", "a").Replace("ô", "o").Replace("ç", "c").Replace("á", "a").Replace("ó", "o").ToLower()
            == materiaPrima.Replace("ã", "a").Replace("ç", "c").Replace("á", "a").Replace("ô", "o").Replace("ó", "o").ToLower() && c.Ativo == true);
            if (materiaPrimaAtivo == null || materiaPrimaAtivo.IsNullOrEmpty())
            {
                return NotFound();
            }
            else
            {
                return Ok(materiaPrimaAtivo.Select(c => new
                {
                    nome = c.Nome,
                    lote = c.LoteInterno,
                    data = c.Data.Value.ToString("dd/MM/yyyy"),
                    nf = c.NF,
                    ativo = true
                })); ;

            }
        }
    }
}
