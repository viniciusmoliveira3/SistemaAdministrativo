using AutoMapper;
using Colex.Interfaces;
using Colex.Repository;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Text;

namespace Colex.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IExtintorRepository _extintorRepository;
        private readonly IRelatorioItensRepository _relatorioItensRepository;
        private readonly IOsRepository _osRepository;
        public readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;

        public RelatorioController ( IExtintorRepository extintorRepository, IMapper mapper, IRelatorioItensRepository relatorioItensRepository, IOsRepository osRepository,IClienteRepository clienteRepository, IMateriaPrimaRepository materiaPrimaRepository)
        {
            _extintorRepository = extintorRepository;
            _mapper = mapper;
            _relatorioItensRepository = relatorioItensRepository;
            _osRepository = osRepository;
            _clienteRepository = clienteRepository;
            _materiaPrimaRepository = materiaPrimaRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RelatorioExtintorCliente(int idCliente, int data)
        {
            try
            {
                var cliente = _clienteRepository.GetById(idCliente);
                var clienteViewModel = _mapper.Map<ClienteViewModels>(cliente);

                var extintores = _relatorioItensRepository.GetExtintorByCliente(idCliente, data);

                var os = extintores.FirstOrDefault();
                var osViewModel = _mapper.Map<OsViewModels>(os.Os);

                TempData["NumeroOs"] = osViewModel.NumeroOrdemServico;
                ViewBag.ExtintorCliente = _mapper.Map<List<RelatorioItensViewModels>>(extintores).OrderBy(e => e.NumSelo);


                return View(clienteViewModel);
            }
            catch(Exception ex)
            {
                TempData["Error-RelatorioCliente"] = "Não foi possivel localizar esses relatório"!;

                return Redirect("/Home");
            }
           
        }
        public IActionResult BuscarExtintiorClienteAutoComplete(int idCliente, string nome, bool autocomplete = true)
        {

            var listCliente = !autocomplete ? _clienteRepository.PesquisarCliente(idCliente, nome) :
            new List<ClienteViewModels>();

            var listClienteAutoComplete = autocomplete ? _clienteRepository.PesquisarClienteAutoComplete(nome) :
            new List<ClienteAutoCompleteViewModels>();

            if (!autocomplete)
            {
                GC.Collect();
                GC.SuppressFinalize(this);
                return Ok(listCliente.Select(c => new
                {
                    IdCliente = c.Id,
                    Nome = new string(c.NomeFantasia.Normalize(NormalizationForm.FormD).Where(ch => char.GetUnicodeCategory(ch)
                    != UnicodeCategory.NonSpacingMark).ToArray())
                }));
            }
            else
            {
                GC.Collect();
                GC.SuppressFinalize(this);
                return Ok(listClienteAutoComplete.Select(c => new
                {
                    id = c.IdCliente,
                    label = c.Nome
                }));
            }

        }

        public IActionResult RelatorioExtintorSelo(long selo)
        {
            try
            {
                var extintor = _relatorioItensRepository.GetExtintorBySelo(selo);
                if(extintor != null)
                {
                    return View(_mapper.Map<RelatorioItensViewModels>(extintor));

                }
                else
                {
                    TempData["Error-RelatorioSelo"] = "Não foi possivel localizar esses relatório"!;
                    return Redirect("/Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error-RelatorioSelo"] = "Não foi possivel localizar esses relatório"!;
                return Redirect("/Home");
            }
            
        }
        public IActionResult RelatorioExtintorLote(int carga, string lote)
        {
            try
            {
                List<RelatorioItensViewModels> extintoresLote = _relatorioItensRepository.GetExtintiorByLote(carga, lote);
                var materiaPrima = _materiaPrimaRepository.GetById(carga);
                TempData["MateriaPrima"] = materiaPrima.Nome;
                TempData["Lote"] = lote;

                ViewBag.ExtintiorLote = extintoresLote.OrderBy(e => e.NumSelo);

                return View();
            }
            catch(Exception ex)
            {
                TempData["Error-RelatorioLote"] = "Não foi possivel localizar esses relatório"!;
                return Redirect("/Home");
            }
            
        }

        public IActionResult RelatorioExtintor (string numeroExtintor)
        {
            try
            {
                List<RelatorioItensViewModels> list = _relatorioItensRepository.GetRelatorioExtintor(numeroExtintor);

                if (list.Count > 0)
                {
                    return View(list);

                }
                else
                {
                    TempData["Error-RelatorioExtintor"] = "Não foi possivel localizar esse extintor"!;
                    return Redirect("/Home");
                }

            }
            catch (Exception ex)
            {

                TempData["Error-RelatorioExtintor"] = "Não foi possivel localizar esses relatório"!;
                return Redirect("/Home");
            }
        }
       
    }
}
