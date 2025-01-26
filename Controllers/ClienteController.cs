using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.Repository;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.Json.Nodes;

namespace Colex.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IRepresentanteRepository _representanteRepository;


        public ClienteController(IClienteRepository clienteRepository, IRepresentanteRepository representanteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _representanteRepository = representanteRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            int totalCliente = 0;
            var listCliente = _clienteRepository.PaginacaoCliente(null, 1, out totalCliente);
            int totalPagina = (int)Math.Ceiling((double)totalCliente / (double)15);
            totalCliente = totalCliente == 0 ? 1 : totalCliente;

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Cliente = listCliente.OrderByDescending(e => e.Id);

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ClienteViewModels viewModels)
        {
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(viewModels);
                cliente.Ativo = true;
                cliente.NomeFantasia = cliente.NomeFantasia.ToUpper();
                cliente.Email = cliente.Email != null ? cliente.Email.ToUpper() : null;
                cliente.EnderecoSocial = cliente.EnderecoSocial != null? cliente.EnderecoSocial.ToUpper() : null;
                cliente.RazaoSocial =cliente.RazaoSocial != null ? cliente.RazaoSocial.ToUpper() : null;
                cliente.Bairro = cliente.Bairro != null ? cliente.Bairro.ToUpper() : null;
                cliente.Cidade = cliente.Cidade != null ? cliente.Cidade.ToUpper() : null;
                _clienteRepository.Add(cliente);
                TempData["Success-Cliente"] = "Cadastrado com sucesso";
                return Redirect("/Cliente/Cadastrar");
            }
            catch(Exception ex)
            {
                TempData["Error-Cliente"] = "Erro ao cadastrar";
                return Redirect("/Cliente/Cadastrar");
            }
                

        }
        [HttpGet]
        public IActionResult Cadastrar()
        {

            List<Representante> representantes = _representanteRepository.GetAll();

            List<RepresentanteViewModels> representanteViewModels = _mapper.Map<List<RepresentanteViewModels>>(representantes);
            ViewBag.Representante = representanteViewModels;

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cliente = _clienteRepository.GetById(id);
            ClienteViewModels viewModel = _mapper.Map<ClienteViewModels>(cliente);

            List<Representante> representante= _representanteRepository.GetAll();
            List<RepresentanteViewModels> representanteViewModels = _mapper.Map<List<RepresentanteViewModels>>(representante);

            ViewBag.Representante = representanteViewModels;

            return View(viewModel);


        }
        [HttpPost]
        public IActionResult Editar(ClienteViewModels viewModel)
        {

            try
            {
                Cliente cliente = _mapper.Map<Cliente>(viewModel);
                cliente.Ativo = true;
                cliente.NomeFantasia = cliente.NomeFantasia.ToUpper();
                cliente.Email = cliente.Email != null ? cliente.Email.ToUpper() : null;
                cliente.EnderecoSocial = cliente.EnderecoSocial != null ? cliente.EnderecoSocial.ToUpper() : null;
                cliente.RazaoSocial = cliente.RazaoSocial != null ? cliente.RazaoSocial.ToUpper() : null;
                cliente.Bairro = cliente.Bairro != null ? cliente.Bairro.ToUpper() : null;
                cliente.Cidade = cliente.Cidade != null ? cliente.Cidade.ToUpper() : null;
                _clienteRepository.Update(cliente);
                TempData["Success-Cliente"] = "Alterado com sucesso";
                return Redirect($"/Cliente/Editar/{viewModel.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error-Cliente"] = "Erro ao alterar";
                return Redirect($"/Cliente/Editar/{viewModel.Id}");

            }
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var cliente = _clienteRepository.GetById(id);
            ClienteViewModels viewModel = _mapper.Map<ClienteViewModels>(cliente);

            List<Representante> representante = _representanteRepository.GetAll();
            List<RepresentanteViewModels> representanteViewModels = _mapper.Map<List<RepresentanteViewModels>>(representante);

            ViewBag.Representante = representanteViewModels;

            return View(viewModel);
        }

        public IActionResult PesquisarClienteIndex(string cliente, int paginaAtual)
        {
            int totalcliente = 0;
            var listCliente = _clienteRepository.PaginacaoCliente(cliente, paginaAtual, out totalcliente);
            int totalPagina = (int)Math.Ceiling((double)totalcliente / (double)15);
            totalcliente = totalcliente == 0 ? 1 : totalcliente;

            var jsonList = new List<JsonObject>();
            listCliente = listCliente.OrderByDescending(e => e.Id).ToList();
            if (listCliente != null)
            {
                foreach (var item in listCliente)
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
                return NotFound(new ClienteViewModels());
            }

        }
    }
}
