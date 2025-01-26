using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Colex.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IMapper _mapper;
        
        public HomeController(IMateriaPrimaRepository materiaPrimaRepository, IMapper mapper)
        {
            _materiaPrimaRepository = materiaPrimaRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var materiaPrima = _materiaPrimaRepository.GetAll().Where(e => e.Ativo == true); ;
            List<MateriaPrimaViewModels> materiaPrimaViewModels = _mapper.Map<List<MateriaPrimaViewModels>>(materiaPrima);

            ViewBag.MateriaPrima = materiaPrimaViewModels;
            string[] Modes = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set","Out","Nov","Dez" };
            ViewBag.Meses = Modes;
            return View();

        }

    }
}