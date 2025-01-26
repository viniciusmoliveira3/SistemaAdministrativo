using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
   
    public class OrcamentoController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IMapper _mapper;
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IOrcamentoProdutoRepository _orcamentoProdutoRepository;
        public OrcamentoController(IClienteRepository clienteRepository, IMateriaPrimaRepository materiaPrimaRepository, IMapper mapper, IOrcamentoRepository orcamentoRepository, IOrcamentoProdutoRepository orcamentoProdutoRepository)
        {
            _clienteRepository = clienteRepository;
            _materiaPrimaRepository = materiaPrimaRepository;
            _mapper = mapper;
            _orcamentoRepository = orcamentoRepository;
            _orcamentoProdutoRepository = orcamentoProdutoRepository;
            
        }
        public IActionResult Index()
        {

            int totalOrcamento = 0;
            var listOrcamento = _orcamentoRepository.PaginacaoOrcamento(null, 0, 1, out totalOrcamento);
            int totalPagina = (int)Math.Ceiling((double)totalOrcamento / (double)15);
            totalOrcamento = totalOrcamento == 0 ? 1 : totalOrcamento;

            //List<RelatorioItens> relatorioItens = _relatorioItensRepository.GetAllComplete();

            //List<RelatorioItensViewModels> relatorioItensViewModels = _mapper.Map<List<RelatorioItensViewModels>>(relatorioItens);

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Orcamento = listOrcamento.OrderBy(o => o.IdOrcamento);
            //ViewBag.RelatorioItens = relatorioItensViewModels.OrderByDescending(e => e.Os.DataAbertura).ToList();

            return View();

        }
        public IActionResult Cadastrar()
        {
            List<Cliente> clientes = _clienteRepository.GetAll().Where(c => c.Ativo == true).ToList();
            List<MateriaPrima> materia = _materiaPrimaRepository.GetAll().Where(m => m.Ativo == true).ToList();

            ViewBag.Materia = _mapper.Map<List<MateriaPrimaViewModels>>(materia);
            
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(OrcamentoViewModels viewModel)
        {
            try
            {
                OrcamentoViewModels orcamento = _orcamentoRepository.Adicionar(viewModel);
                
                TempData["Success-Orcamento"] = "Cadastrado com sucesso!";

                var url = "/Orcamento/OrcamentoPdf?idOrcamento=" + orcamento.IdOrcamento;

                GC.Collect();
                GC.SuppressFinalize(orcamento);

                return Redirect(url);

            }
            catch (Exception ex)
            {
                TempData["Error-Orcamento"] = "Erro ao cadastrar!";
                return Redirect("/Orcamento/Cadastrar");
            }
        
        }
        public IActionResult Editar(int IdOrcamento)
        {
            OrcamentoViewModels viewModel = _mapper.Map<OrcamentoViewModels>(_orcamentoRepository.GetById(IdOrcamento));
            List<OrcamentoProdutoViewModels> orcamentoProdutoViewModels = _orcamentoProdutoRepository.GetByIdOrcamento(IdOrcamento);
            List<MateriaPrima> materia = _materiaPrimaRepository.GetAll().Where(m => m.Ativo == true).ToList();

            ViewBag.OrcamentoProduto = orcamentoProdutoViewModels;
            ViewBag.Materia = _mapper.Map<List<MateriaPrimaViewModels>>(materia);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar (OrcamentoViewModels viewModels)
        {
            try
            {
                _orcamentoRepository.Alterar(viewModels);

                var url = "/Orcamento/OrcamentoPdf?idOrcamento=" + viewModels.IdOrcamento;

                GC.Collect();
                GC.SuppressFinalize(viewModels);

                return Redirect(url);

            }
            catch (Exception ex)
            {
                TempData["Error-Orcamento"] = "Erro ao cadastrar!";
                return Redirect("/Orcamento/Editar?idOrcamento=" + viewModels.IdOrcamento );
            }

        }
        public IActionResult OrcamentoPdf(int idOrcamento)
        {
            Orcamento viewModel = _orcamentoRepository.GetAll().Where(o => o.IdOrcamento == idOrcamento).First();
            ViewBag.OrcamentoProdutos = _orcamentoProdutoRepository.GetAllComplete().Where(e => e.IdOrcamento == idOrcamento).ToList();

            return View(_mapper.Map<OrcamentoViewModels>(viewModel)); 
        }
        [HttpPost]
        public IActionResult DeletarOrcamentoProduto(int idOrcamentoProduto) 
        {
            try
            {
                OrcamentoProduto orcamentoProduto = _orcamentoProdutoRepository.GetById(idOrcamentoProduto);
                orcamentoProduto.MateriaPrima = null;
                orcamentoProduto.Orcamento = null;
                _orcamentoProdutoRepository.Delete(orcamentoProduto);

                return Ok(new
                {
                    sucesso = true
                });

            }
            catch
            {
                return Ok(new
                {
                    sucesso = false
                });
            }
          
        }
        public IActionResult PesquisarOrcamentoIndex(string nome, long numeroOrcamento, int paginaAtual)
        {
            int totalOs = 0;
            var listOrcamento = _orcamentoRepository.PaginacaoOrcamento(nome, numeroOrcamento, paginaAtual, out totalOs);
            int totalPagina = (int)Math.Ceiling((double)totalOs / (double)15);
            totalOs = totalOs == 0 ? 1 : totalOs;

            var jsonList = new List<JsonObject>();

            if (listOrcamento != null)
            {
                foreach (var orcamento in listOrcamento)
                {
                    var json = new JsonObject();

                    json["IdOrcamento"] = orcamento.IdOrcamento;
                    json["Cliente"] = orcamento.Cliente;
                    json["Telefone"] = orcamento.Telefone;
                    json["ValorFinal"] = orcamento.ValorFinal;
                    json["TotalPagina"] = totalPagina;

                    jsonList.Add(json);
                }
                return Ok(jsonList);
            }
            else
            {
                TempData["Error-OS"] = "Não possível localizar a ordem de serviço, cadastre uma nova!";
                return NotFound(new OsViewModels());
            }

        }
    }
}
