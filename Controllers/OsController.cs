using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using System.Text;
using System.Globalization;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Colex.Controllers
{
    public class OsController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IOsRepository _osRepository;
        private readonly IRelatorioItensRepository _relatorioItensRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IExtintorRepository _extintorRepository;
        private readonly IMarcaExintorRepository _marcaExintorRepository;
        private readonly ICapacidadeRepository _capacidadeRepository;
        private readonly IComponenteRepository _componenteRepository;
        private readonly ISeloRepository _seloRepository;
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IRepresentanteRepository _representanteRepository;
        public OsController(IMapper mapper, IOsRepository osRepository, IClienteRepository clienteRepository, IRelatorioItensRepository relatorioItensRepository,
            IExtintorRepository extintorRepository, IMarcaExintorRepository marcaExintorRepository, ICapacidadeRepository capacidadeRepository,
            IComponenteRepository componenteRepository, ISeloRepository seloRepository, IMateriaPrimaRepository materiaPrimaRepository, IRepresentanteRepository representanteRepository)
        {

            _mapper = mapper;
            _osRepository = osRepository;
            _clienteRepository = clienteRepository;
            _relatorioItensRepository = relatorioItensRepository;
            _extintorRepository = extintorRepository;
            _marcaExintorRepository = marcaExintorRepository;
            _capacidadeRepository = capacidadeRepository;
            _componenteRepository = componenteRepository;
            _seloRepository = seloRepository;
            _materiaPrimaRepository = materiaPrimaRepository;
            _representanteRepository = representanteRepository;
        }


        public IActionResult Index()
        {

            int totalOs = 0;
            var listOs = _osRepository.PaginacaoOs(null, null, 1, out totalOs);
            int totalPagina = (int)Math.Ceiling((double)totalOs / (double)15);
            totalOs = totalOs == 0 ? 1 : totalOs;

            List<RelatorioItens> relatorioItens = _relatorioItensRepository.GetAllComplete();

            List<RelatorioItensViewModels> relatorioItensViewModels = _mapper.Map<List<RelatorioItensViewModels>>(relatorioItens);

            ViewBag.TotalPagina = totalPagina;
            ViewBag.Os = listOs.OrderByDescending(o => o.IdOServico);
            ViewBag.RelatorioItens = relatorioItensViewModels.OrderByDescending(e => e.Os.IdOServico).ToList();

            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()

        {

            List<Extintor> extintores = _extintorRepository.GetAll();
            List<Cliente> clientes = _clienteRepository.GetAll();
            List<RelatorioItens> relatorioItens = _relatorioItensRepository.GetAll();
            List<MarcaExtintor> marcaExtintores = _marcaExintorRepository.GetAll();
            List<Capacidade> capacidades = _capacidadeRepository.GetAll();
            List<Componente> componentes = _componenteRepository.GetAll().Where(c => c.Ativo == true).ToList();
            List<MateriaPrima> materiaPrimas = _materiaPrimaRepository.GetAll().Where(m => m.Ativo == true).ToList();
            List<Representante> representante = _representanteRepository.GetAll();

            List<ExtintorViewModels> extintorViewModels = _mapper.Map<List<ExtintorViewModels>>(extintores);
            List<ClienteViewModels> clienteViewModels = _mapper.Map<List<ClienteViewModels>>(clientes);
            List<RelatorioItensViewModels> relatorioItensViewModels = _mapper.Map<List<RelatorioItensViewModels>>(relatorioItens);
            List<MarcaExtintorViewModels> marcaExtintorViewModels = _mapper.Map<List<MarcaExtintorViewModels>>(marcaExtintores);
            List<CapacidadeViewModels> capacidadeViewModels = _mapper.Map<List<CapacidadeViewModels>>(capacidades);
            List<ComponenteViewModels> componenteViewModels = _mapper.Map<List<ComponenteViewModels>>(componentes);
            List<MateriaPrimaViewModels> materiaPrimaViewModels = _mapper.Map<List<MateriaPrimaViewModels>>(materiaPrimas);
            List<RepresentanteViewModels> representanteViewModels = _mapper.Map<List<RepresentanteViewModels>>(representante);

            ViewBag.Extintor = extintorViewModels;
            ViewBag.Cliente = clienteViewModels;
            ViewBag.RelatorioItens = relatorioItensViewModels.OrderBy(o => o.IdRelatorioItens);
            ViewBag.MarcaExtintor = marcaExtintorViewModels.OrderBy(m => m.Nome);
            ViewBag.Capacidade = capacidadeViewModels;
            ViewBag.Componente1 = componenteViewModels;
            ViewBag.Componente2 = componenteViewModels;
            ViewBag.Componente3 = componenteViewModels;
            ViewBag.Componente4 = componenteViewModels;
            ViewBag.MateriaPrima = materiaPrimaViewModels.OrderBy(m => m.Nome);
            ViewBag.Representante = representanteViewModels.OrderBy(m => m.NomeFantasia);
            var dataAtual = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            var dataProximaano = new DateTime(dataAtual.Year + 1, dataAtual.Month, dataAtual.Day);
            TempData["ProximaDataRecarga"] = dataProximaano.ToString("dd/MM/yyyy");
            foreach (var materia in materiaPrimas)
            {
                if (materia.Nome.ToLower() != "ap" || materia.Nome.ToLower() != "água")
                {
                    if (materia.Nome.ToLower() == "pó bc" || materia.Nome.ToLower() == "bc")
                    {
                        if (materia.QuantidadeAtual <= 50)
                        {
                            TempData["EstoquePoBc"] = "O estoque de bó BC está abaixo de 50 kg";
                        }
                    }
                    if (materia.Nome.ToLower() == "pó abc" || materia.Nome.ToLower() == "abc")
                    {
                        if (materia.QuantidadeAtual <= 50)
                        {
                            TempData["EstoquePoAbc"] = "O estoque de bó abc está abaixo de 50 kg";
                        }
                    }
                    if (materia.Nome.ToLower() == "co2")
                    {
                        if (materia.QuantidadeAtual <= 25)
                        {
                            TempData["EstoqueCo2"] = "O estoque de Co2 está abaixo de 25 kg";
                        }
                    }
                    if (materia.Nome.ToLower() == "Esp.Mec" || materia.Nome.ToLower() == "E.Mecânica")
                    {
                        if (materia.QuantidadeAtual <= 2)
                        {
                            TempData["EstoqueEspMec"] = "O estoque de espuma mec. está abaixo de 2 lt";
                        }
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(OsViewModels viewModels)
        {
            string urlRelatorio = string.Empty;
            bool prosseguirRelatorio;
            try
            {
                viewModels = _osRepository.Adicionar(viewModels);
                TempData["Success-os"] = "Cadastrado com sucesso!";

                prosseguirRelatorio = true;

                TempData["IdOs"] = viewModels.IdOServico;
                urlRelatorio = prosseguirRelatorio == true ? "/Os/Relatorio?idOs=" + viewModels.IdOServico : "/Os/Cadastrar?idOs=" + viewModels.IdOServico;

                GC.Collect();
                GC.SuppressFinalize(viewModels);


                return Redirect(urlRelatorio);
            }
            catch (Exception ex)
            {
                TempData["Error-os"] = "Erro ao cadastrar";
                return Redirect("/Os/Cadastrar");
            }

        }
        public IActionResult Editar(int idOs)
        {
            OsViewModels osViewModel = _osRepository.GetRelatorioById(idOs);
            List<RelatorioItensViewModels> relatorioItensViewModels = _relatorioItensRepository.GetRelatorioByOsid(idOs);


            List<Extintor> extintores = _extintorRepository.GetAll();
            List<Cliente> clientes = _clienteRepository.GetAll();
            List<RelatorioItens> relatorioItens = _relatorioItensRepository.GetAll();
            List<MarcaExtintor> marcaExtintores = _marcaExintorRepository.GetAll();
            List<Capacidade> capacidades = _capacidadeRepository.GetAll();
            List<Componente> componentes = _componenteRepository.GetAll().Where(c => c.Ativo == true).ToList();
            List<MateriaPrima> materiaPrimas = _materiaPrimaRepository.GetAll().Where(m => m.Ativo == true).ToList();
            List<Representante> representante = _representanteRepository.GetAll();

            List<ExtintorViewModels> extintorViewModels = _mapper.Map<List<ExtintorViewModels>>(extintores);
            List<ClienteViewModels> clienteViewModels = _mapper.Map<List<ClienteViewModels>>(clientes);
            List<MarcaExtintorViewModels> marcaExtintorViewModels = _mapper.Map<List<MarcaExtintorViewModels>>(marcaExtintores);
            List<CapacidadeViewModels> capacidadeViewModels = _mapper.Map<List<CapacidadeViewModels>>(capacidades);
            List<ComponenteViewModels> componenteViewModels = _mapper.Map<List<ComponenteViewModels>>(componentes);
            List<MateriaPrimaViewModels> materiaPrimaViewModels = _mapper.Map<List<MateriaPrimaViewModels>>(materiaPrimas);
            List<RepresentanteViewModels> representanteViewModels = _mapper.Map<List<RepresentanteViewModels>>(representante);

            ViewBag.Extintor = extintorViewModels;
            ViewBag.Cliente = clienteViewModels;
            ViewBag.RelatorioItens = relatorioItensViewModels.OrderBy(o => o.IdRelatorioItens);
            ViewBag.MarcaExtintor = marcaExtintorViewModels.OrderBy(m => m.Nome);
            ViewBag.Capacidade = capacidadeViewModels;
            ViewBag.Componente1 = componenteViewModels;
            ViewBag.Componente2 = componenteViewModels;
            ViewBag.Componente3 = componenteViewModels;
            ViewBag.Componente4 = componenteViewModels;
            ViewBag.MateriaPrima = materiaPrimaViewModels.OrderBy(m => m.Nome);
            ViewBag.Representante = representanteViewModels.OrderBy(m => m.NomeFantasia);
            var dataAtual = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            var dataProximaano = new DateTime(dataAtual.Year + 1, dataAtual.Month, dataAtual.Day);
            TempData["ProximaDataRecarga"] = dataProximaano.ToString("dd/MM/yyyy");
            foreach (var materia in materiaPrimas)
            {
                if (materia.Nome.ToLower() != "ap" || materia.Nome.ToLower() != "água")
                {
                    if (materia.Nome.ToLower() == "pó bc" || materia.Nome.ToLower() == "bc")
                    {
                        if (materia.QuantidadeAtual <= 50)
                        {
                            TempData["EstoquePoBc"] = "O estoque de bó BC está abaixo de 50 kg";
                        }
                    }
                    if (materia.Nome.ToLower() == "pó abc" || materia.Nome.ToLower() == "abc")
                    {
                        if (materia.QuantidadeAtual <= 50)
                        {
                            TempData["EstoquePoAbc"] = "O estoque de bó abc está abaixo de 50 kg";
                        }
                    }
                    if (materia.Nome.ToLower() == "co2")
                    {
                        if (materia.QuantidadeAtual <= 25)
                        {
                            TempData["EstoqueCo2"] = "O estoque de Co2 está abaixo de 25 kg";
                        }
                    }
                    if (materia.Nome.ToLower() == "Esp.Mec" || materia.Nome.ToLower() == "E.Mecânica")
                    {
                        if (materia.QuantidadeAtual <= 2)
                        {
                            TempData["EstoqueEspMec"] = "O estoque de espuma mec. está abaixo de 2 lt";
                        }
                    }
                }
            }
            return View(osViewModel);
        }
        [HttpPost]
        public IActionResult Editar(OsViewModels viewModels)
        {

            string urlRelatorio = string.Empty;
            bool prosseguirRelatorio;
            try
            {
                OsViewModels os = _osRepository.Editar(viewModels);

                prosseguirRelatorio = true;

                TempData["IdOs"] = os.IdOServico;
                urlRelatorio = prosseguirRelatorio == true ? "/Os/Relatorio?idOs=" + os.IdOServico : "/Os/Cadastrar?idOs=" + os.IdOServico;

                GC.Collect();
                GC.SuppressFinalize(os);


                return Redirect(urlRelatorio);

            }
            catch (Exception ex)
            {
                TempData["Error-os"] = "Erro ao cadastrar";
                return Redirect("/Os/Cadastrar");
            }

        }
        public IActionResult BuscarCliente(int idCliente, string nome, bool autocomplete = true)
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
                    != UnicodeCategory.NonSpacingMark).ToArray()),
                    Endereco = c.EnderecoSocial,
                    Bairro = c.Bairro != null ? c.Bairro : "Não informado",
                    Cidade = c.Cidade != null ? c.Cidade : "Não informado",
                }));
            }
            else
            {
                GC.Collect();
                GC.SuppressFinalize(this);
                return Ok(listClienteAutoComplete.Select(c => new
                {
                    id = c.IdCliente,
                    label = c.Nome,

                }));
            }

        }

        public IActionResult PesquisarExtintorCilindro(int idExtintor, string numeroCilindro)
        {

            var extintor = _extintorRepository.PesquisarExtintor(idExtintor, numeroCilindro);

            if (extintor != null)
            {
                var json = new JsonObject();

                json["NumeroCilindro"] = extintor.NumeroCilindro;
                json["AnoFabricacao"] = extintor.AnoFabricacao;
                json["UltimoEnsaio"] = extintor.EnsaioHidrostatico;
                json["Fabricante"] = extintor.IdMarcaExtintor;
                json["MateriaPrima"] = extintor.IdMateriaPrima;
                json["Capacidade"] = extintor.IdCapacidade;
                json["NBR"] = extintor.NBR;
                json["ProximoEnsaio"] = extintor.ProximoEnsaioHisdrostatico;
                json["IdExtintor"] = extintor.Id;
                json["CapacidadeExtintora"] = extintor.CapacExtintora;
                json["NumPatrimonio"] = extintor.NumPatrimonio;
                json["Projeto"] = extintor.Projeto;
                json["Lote"] = extintor.Lote;
                json["SeloAnterior"] = extintor.SeloAnterior;
                return Ok(json);

            }
            else
            {
                return NotFound(new ExtintorViewModels());
            }

        }

        [HttpGet]
        public IActionResult Relatorio(int idOs)
        {
            var os = _osRepository.GetRelatorioById(idOs);
            OsViewModels viewModels = _mapper.Map<OsViewModels>(os);
            List<RelatorioItensViewModels> relatorioItensViewModels = _relatorioItensRepository.GetRelatorioByOsid(idOs);
            var materiasPrimas = _materiaPrimaRepository.GetAll().Where(m => m.Ativo == true);
            var componentes = _componenteRepository.GetAll();

            foreach (var materia in materiasPrimas)
            {
                var materiaNome = "";
                materiaNome = materia.Nome.Trim().ToLower();
                if (materiaNome == "po bc" || materiaNome == "pó bc")
                {
                    viewModels.LoteAtualBc = materia.LoteInterno;
                }
                if (materiaNome == "po abc" || materiaNome == "pó abc")
                {
                    viewModels.LoteAtualAbc = materia.LoteInterno;
                }
                if (materiaNome == "em" || materiaNome == "espuma mecanica")
                {
                    viewModels.LoteAtualEm = materia.LoteInterno;
                }
                if(materiaNome == "co2" || materiaNome == "co²" )
                {
                    viewModels.LoteAtualCo2 = materia.LoteInterno;
                }

            }
            foreach (var componente in componentes)
            {
                var componenteNome = "";
                componenteNome = componente.Nome.Trim().ToLower();
                if ((componenteNome == "indicador de pressão" || componenteNome == "indicador de pressao") && componente.Ativo == true)
                {
                    viewModels.LoteIndPressao = componente.Lote;
                }
                else if ((componenteNome == "mangueira de pó" || componenteNome == "mangueira de po") && componente.Ativo == true)
                {
                    viewModels.LoteMangueirapo = componente.Lote;
                }
                else if (componenteNome == "mangueira de co2" && componente.Ativo == true)
                {
                    viewModels.LoteMangueiraco2 = componente.Lote;
                }
                else if ((componenteNome == "válvula alta" || componenteNome == "valvula alta") && componente.Ativo == true)
                {
                    viewModels.LoteValvulaAlta = componente.Lote;
                }
                else if ((componenteNome == "válvula baixa" || componenteNome == "valvula baixa") && componente.Ativo == true)
                {
                    viewModels.LoteValvulaBaixa = componente.Lote;
                }
                else if ((componenteNome == "conjunto de segurança" || componenteNome == "conjunto de seguranca") && componente.Ativo == true)
                {
                    viewModels.LoteConjSegurança = componente.Lote;
                }
            }

            ViewBag.Os = os;
            ViewBag.RelatorioItens = relatorioItensViewModels.OrderBy(o => o.NumSelo);


            return View(os);
        }

        public IActionResult AlterarExtintor(ExtintorViewModels viewModel)
        {
            try
            {
                _extintorRepository.Alterar(viewModel);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        public IActionResult RecuperarExtintorAlterar(int idExtintor)
        {
            var extintor = _extintorRepository.GetByIdExtintor(idExtintor);

            if (extintor != null)
            {
                var json = new JsonObject();

                json["NumeroCilindro"] = extintor.NumeroCilindro;
                json["AnoFabricacao"] = extintor.AnoFabricacao;
                json["UltimoEnsaio"] = extintor.EnsaioHidrostatico;
                json["Fabricante"] = extintor.IdMarcaExtintor;
                json["MateriaPrima"] = extintor.IdMateriaPrima;
                json["Capacidade"] = extintor.IdCapacidade;
                json["NBR"] = extintor.NBR;
                json["ProximoEnsaio"] = extintor.ProximoEnsaioHisdrostatico;
                json["IdExtintor"] = extintor.Id;
                json["CapacidadeExtintora"] = extintor.CapacExtintora == null ? "" : extintor.CapacExtintora;
                json["NumPatrimonio"] = extintor.NumPatrimonio == null ? "" : extintor.NumPatrimonio;
                json["Projeto"] = extintor.Projeto == null ? "" : extintor.Projeto;
                json["Lote"] = extintor.Lote;
                json["SeloAnterior"] = extintor.SeloAnterior;

                return Ok(json);

            }
            else
            {
                TempData["Error-OS"] = "Não possível localizar o cilindro, cadastre um novo!";
                return NotFound(new ExtintorViewModels());
            }

        }


        public IActionResult PesquisarOsIndex(string nome, string numeroOs, int paginaAtual)
        {
            int totalOs = 0;
            var listOs = _osRepository.PaginacaoOs(nome, numeroOs, paginaAtual, out totalOs);
            int totalPagina = (int)Math.Ceiling((double)totalOs / (double)15);
            totalOs = totalOs == 0 ? 1 : totalOs;

            listOs = listOs.OrderByDescending(o => o.IdOServico).ToList();
            var jsonList = new List<JsonObject>();

            if (listOs != null)
            {
                foreach (var os in listOs)
                {
                    var json = new JsonObject();

                    json["NumeroOs"] = os.NumeroOrdemServico;
                    json["Cliente"] = os.Cliente.NomeFantasia;
                    json["DataAbertura"] = os.DataAbertura.ToString("dd/MM/yyyy");
                    json["idOs"] = os.IdOServico;
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

        public IActionResult CadastrarExtintor(ExtintorViewModels viewModel)
        {
            Extintor extintor = _mapper.Map<Extintor>(viewModel);
            extintor.Ativo = true;
            extintor.NumeroCilindro = extintor.NumeroCilindro.ToUpper();
            extintor.Projeto = extintor.Projeto != null ? extintor.Projeto.ToUpper() : null;
            extintor.CapacExtintora = extintor.CapacExtintora.ToUpper();
            _extintorRepository.Add(extintor);

            ExtintorViewModels view = _mapper.Map<ExtintorViewModels>(extintor);

            if (view != null)
                return Ok(view);
            else
                return NotFound(new ExtintorViewModels());


        }

        public IActionResult CadastrarCliente(ClienteViewModels viewModels)
        {
            Cliente cliente = _mapper.Map<Cliente>(viewModels);
            cliente.Ativo = true;
            cliente.NomeFantasia = cliente.NomeFantasia.ToUpper();
            cliente.Email = cliente.Email != null ? cliente.Email.ToUpper() : null;
            cliente.EnderecoSocial = cliente.EnderecoSocial != null ? cliente.EnderecoSocial.ToUpper() : null;
            cliente.RazaoSocial = cliente.RazaoSocial != null ? cliente.RazaoSocial.ToUpper() : null;
            cliente.Bairro = cliente.Bairro != null ? cliente.Bairro.ToUpper() : null;
            cliente.Cidade = cliente.Cidade != null ? cliente.Cidade.ToUpper() : null;
            _clienteRepository.Add(cliente);

            ClienteViewModels view = _mapper.Map<ClienteViewModels>(cliente);

            if (view != null)
                return Ok(view);
            else
                return NotFound(new ClienteViewModels());
        }
        public IActionResult RecuperarRelatorioItens(int idRelatorioItem)
        {
            try
            {
                var relatorioItem = _relatorioItensRepository.GetById(idRelatorioItem);
                return Ok(relatorioItem);
            }
            catch
            {
                return NotFound();

            }

        }
        public IActionResult AlterarRelatorioItens(RelatorioItensViewModels viewModel)
        {
            try
            {
                RelatorioItens relatorioItens = _mapper.Map<RelatorioItens>(viewModel);
                relatorioItens.IdComponentes1 = relatorioItens.IdComponentes1 == 0 ? null : relatorioItens.IdComponentes1;
                relatorioItens.IdComponentes2 = relatorioItens.IdComponentes2 == 0 ? null : relatorioItens.IdComponentes2;
                relatorioItens.IdComponentes3 = relatorioItens.IdComponentes3 == 0 ? null : relatorioItens.IdComponentes3;
                relatorioItens.IdComponentes4 = relatorioItens.IdComponentes4 == 0 ? null : relatorioItens.IdComponentes4;
                _relatorioItensRepository.Update(relatorioItens);
                return Ok(new
                {

                    Mensagem = "success"

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }



}
