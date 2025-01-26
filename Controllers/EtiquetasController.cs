using AutoMapper;
using Colex.Interfaces;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.AspNetCore.Mvc;
using ZXing;

namespace Colex.Controllers
{
   

    public class EtiquetasController : Controller
    {
        private readonly IOsRepository _osRepository;
        private readonly IMapper _mapper;
        private readonly IExtintorRepository _extintorRepository;
        private readonly IRelatorioItensRepository _relatorioItensRepository;

        public EtiquetasController(IOsRepository osRepository, IMapper mapper, IExtintorRepository extintorRepository, IRelatorioItensRepository relatorioItensRepository)
        {
            _osRepository = osRepository;
            _mapper = mapper;
            _extintorRepository = extintorRepository;
            _relatorioItensRepository = relatorioItensRepository;
        }

        public IActionResult GerarEtiquetas( string numeroOs, int data)
        {
            try
            {
                List<RelatorioItensViewModels> itensEtiquetas = _relatorioItensRepository.GetRelaotrioItensByNumeroOs(numeroOs, data);

                return View(itensEtiquetas);
            }
            catch(Exception ex)
            {
                TempData["Error-Etiquetas"] = "Não possível localizar as etiquetas!";
                return Redirect("/Home");
            }
        }
           

        public IActionResult GerarEtiquetasPorExtintor(string numeroExtintor, int data)
        {
            try
            {
                RelatorioItensViewModels itensEtiquetas = _relatorioItensRepository.GetEtiquetasByNumeroExtintior(numeroExtintor, data);

                return View(itensEtiquetas);
            }
            catch (Exception ex)
            {
                TempData["Error-Etiqueta"] = "Não possível localizar as etiqueta!";
                return Redirect("/Home");
            }
        }
        public IActionResult GerarEtiquetasTemporaria(string NomeMateria, string mes, string anoProxima, string ano, string manutencao, int qtd)
        {
            EtiquetaTemporariaViewModel viewModel  = new EtiquetaTemporariaViewModel();
            viewModel.NomeMateria = NomeMateria;
            viewModel.Mes = mes;
            viewModel.Ano = ano;    
            viewModel.Manutencao = manutencao;
            viewModel.Qtd = qtd;
            viewModel.AnoProximaManu = anoProxima;


            return View(viewModel);
        }
    }
}
