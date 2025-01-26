using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class ExtintorController : Controller
    {
        private readonly IExtintorRepository _extintorRepository;
        private readonly IMarcaExintorRepository _marcaExintorRepository;
        private readonly ICapacidadeRepository _capacidadeRepository;
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IMapper _mapper;
        
        public ExtintorController(IExtintorRepository extintorRepository, IMarcaExintorRepository marcaExintorRepository, ICapacidadeRepository capacidadeRepository, IMapper mapper, IMateriaPrimaRepository materiaPrimaRepository)
        {
            _extintorRepository = extintorRepository;
            _marcaExintorRepository = marcaExintorRepository;
            _capacidadeRepository = capacidadeRepository;
            _mapper = mapper;
            _materiaPrimaRepository = materiaPrimaRepository;
        }
        public IActionResult Index()
        {
            int totalOs = 0;
            var listExtintores = _extintorRepository.PaginacaoExtintor(null, 1, out totalOs);
            int totalPagina = (int)Math.Ceiling((double)totalOs / (double)15);
            totalOs = totalOs == 0 ? 1 : totalOs;


            ViewBag.TotalPagina = totalPagina;
            ViewBag.ListaExtintores = listExtintores.OrderBy(e => e.Id);

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ExtintorViewModels viewModel)
        {
            try
            {
                Extintor extintor = _mapper.Map<Extintor>(viewModel);
                extintor.Ativo = true;
                extintor.NumeroCilindro = extintor.NumeroCilindro.ToUpper();
                extintor.Projeto = extintor.Projeto != null ? extintor.Projeto.ToUpper() : null;
                extintor.CapacExtintora = extintor.CapacExtintora != null ? extintor.CapacExtintora.ToUpper() : null;
                _extintorRepository.Add(extintor);
                TempData["Success-Extintor"] = "Cadastrado com sucesso!";
                return Redirect("/Extintor/Cadastrar");
            }
            catch (Exception ex)
            {
                TempData["Error-Extintor"] = "Erro ao cadastrar!";
                return Redirect("/Extintor/Cadastrar");
            }
           

        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            List<MarcaExtintor> marcaExtintor = _marcaExintorRepository.GetAll();
            List<MateriaPrima> materiaPrimas = _materiaPrimaRepository.GetAll().Where(e => e.Ativo == true).ToList();
            List<Capacidade> capacidade = _capacidadeRepository.GetAll();

            List<MarcaExtintorViewModels> marcaExtintorViewModels = _mapper.Map<List<MarcaExtintorViewModels>>(marcaExtintor);
            List<MateriaPrimaViewModels> materiaPrimaViewModels = _mapper.Map<List<MateriaPrimaViewModels>>(materiaPrimas);
            List<CapacidadeViewModels> capacidadeViewModels = _mapper.Map<List<CapacidadeViewModels>>(capacidade);


            ViewBag.MarcaExtintor = marcaExtintorViewModels;
            ViewBag.MateriaPrima = materiaPrimaViewModels;
            ViewBag.Capacidade = capacidadeViewModels;


            return View();
        }

        [HttpPost]
        public IActionResult Editar(ExtintorViewModels viewModels)
        {
            try
            {
                Extintor extintor = _mapper.Map<Extintor>(viewModels);
                extintor.NumeroCilindro = extintor.NumeroCilindro.ToUpper();
                extintor.Projeto = extintor.Projeto != null ? extintor.Projeto.ToUpper() : null;
                extintor.CapacExtintora = extintor.CapacExtintora != null ? extintor.CapacExtintora.ToUpper() : null;
                _extintorRepository.Update(extintor);
                TempData["Success-Extintor"] = "Editado com sucesso!";
                return Redirect($"/Extintor/Editar/{viewModels.Id}");
            }
            catch (Exception ex) 
            {
                TempData["Error-Extintor"] = "Erro ao editar!";
                return Redirect($"/Extintor/Editar/{viewModels.Id}");
            }

        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var extintor = _extintorRepository.GetById(id);

            ExtintorViewModels viewModel = _mapper.Map<ExtintorViewModels>(extintor);

            List<MateriaPrima> materiaPrimas = _materiaPrimaRepository.GetAll();
            List<MarcaExtintor> marcaExtintor = _marcaExintorRepository.GetAll();
            List<Capacidade> capacidade = _capacidadeRepository.GetAll();

            List<MateriaPrimaViewModels> materiaPrimaViewModels = _mapper.Map<List<MateriaPrimaViewModels>>(materiaPrimas);
            List<MarcaExtintorViewModels> marcaExtintorViewModel = _mapper.Map<List<MarcaExtintorViewModels>>(marcaExtintor);
            List<CapacidadeViewModels>  capacidadeViewModel = _mapper.Map<List<CapacidadeViewModels>>(capacidade);

            ViewBag.MateriaPrima = materiaPrimaViewModels;
            ViewBag.MarcaExtintor = marcaExtintorViewModel;
            ViewBag.Capacidade = capacidadeViewModel;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Detalhe (int id)
        {
            var extintor = _extintorRepository.GetById (id);
            ExtintorViewModels viewModel = _mapper.Map<ExtintorViewModels>(extintor);

            List<MateriaPrima> materiaPrimas = _materiaPrimaRepository.GetAll();
            List<MarcaExtintor> marcaExtintor = _marcaExintorRepository.GetAll();
            List<Capacidade> capacidade = _capacidadeRepository.GetAll();

            List<MateriaPrimaViewModels> materiaPrimaViewModels = _mapper.Map<List<MateriaPrimaViewModels>>(materiaPrimas);
            List<MarcaExtintorViewModels> marcaExtintorViewModel = _mapper.Map<List<MarcaExtintorViewModels>> (marcaExtintor);
            List<CapacidadeViewModels> capacidadeViewModel = _mapper.Map<List<CapacidadeViewModels>> (capacidade);

            ViewBag.MateriaPrima = materiaPrimaViewModels;
            ViewBag.MarcaExtintor = marcaExtintorViewModel;
            ViewBag.Capacidade = capacidadeViewModel;

            return View(viewModel);

        }

        public IActionResult PesquisarExtintorIndex(string numeroExtintor, int paginaAtual)
        {
            int totalOs = 0;
            var listExtintores = _extintorRepository.PaginacaoExtintor(numeroExtintor, paginaAtual, out totalOs);
            int totalPagina = (int)Math.Ceiling((double)totalOs / (double)15);
            totalOs = totalOs == 0 ? 1 : totalOs;

            var jsonList = new List<JsonObject>();

            if (listExtintores != null)
            {
                foreach (var extintor in listExtintores)
                {
                    var json = new JsonObject();

                    json["NumeroCilindro"] = extintor.NumeroCilindro;
                    json["MarcaExtintor"] = extintor.MarcaExtintor.Nome;
                    json["MateriaPrima"] = extintor.MateriaPrima.Nome;
                    json["Capacidade"] = extintor.Capacidade.CapacidadeCarga;
                    json["ProximoEnsaioHisdrostatico"] = extintor.ProximoEnsaioHisdrostatico;
                    json["idExtintor"] = extintor.Id;
                    json["TotalPagina"] = totalPagina;

                    jsonList.Add(json);
                }
                return Ok(jsonList);
            }
            else
            {
                TempData["Error-OS"] = "Não possível localizar a ordem de serviço, cadastre uma nova!";
                return NotFound(new ExtintorViewModels());
            }

        }
    }
}
