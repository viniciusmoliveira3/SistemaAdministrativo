using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.Models.Auxiliares;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.EntityFrameworkCore;

namespace Colex.Repository
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        private readonly IMapper _mapper;
        public ClienteRepository(IMapper mapper ,BaseContext context) : base(context)
        {
            _mapper = mapper;

        }


        public List<Cliente> GetAllComplete()
        {
            List<Cliente> clientes = Db.Set<Cliente>()
                .Include(c => c.Representante)
                .ToList();

            return clientes;
        }

        public List<ClienteAutoCompleteViewModels> PesquisarClienteAutoComplete(string nome)
        {
            var list = this.GetAllComplete();
            List<Cliente> lista = list;

            if (nome != string.Empty)
            {
                lista = lista.Where(p => p.NomeFantasia.ToLower().StartsWith(nome.ToLower())).ToList();
            }
           
                List<ClienteAutoCompleteViewModels> viewModel = lista.Take(20).Select(c => new ClienteAutoCompleteViewModels
                {
                    IdCliente = c.Id,
                    Nome = c.NomeFantasia,
                    Endereco = c.EnderecoSocial,
                    Bairro = c.Bairro,
                    Cidade = c.Cidade
                }).ToList();

                return viewModel;
            
        }

        public List<ClienteViewModels> PesquisarCliente(int idCliente, string nome)
        {
            var list = this.GetAllComplete();

            if(idCliente > 0)
            {
                list = list.Where(c => c.Id == idCliente).ToList();
            }

            if (nome != null)
            {
                list = list.Where(c => c.NomeFantasia.ToLower().Replace("á", "a").Replace("é", "e").Replace("í", "i")
                .Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u").StartsWith(nome.ToLower())).ToList();
            }

            if(idCliente == 0 && nome == null)
            {
                return new List<ClienteViewModels>();
            }

            var listCliente = list.OrderBy(c => c.NomeFantasia).ToList();

            return _mapper.Map<List<ClienteViewModels>>(listCliente);
        }

        public List<ClienteViewModels> PaginacaoCliente(string cliente, int paginaAtual, out int totalCliente)
        {
            int limite = 15;

            List<Cliente> listCliente = this.GetAll().OrderByDescending(e => e.Id).ToList();

            if (!string.IsNullOrEmpty(cliente))
            {
                var clienteArray = cliente.ToLower().Split(" ");
                foreach (var item in clienteArray)
                {
                    listCliente = (List<Cliente>)listCliente.Where(r => r.NomeFantasia.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalCliente = listCliente.Count();
            List<Cliente> paginacao = listCliente.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<ClienteViewModels>>(paginacao);
        }

        private string RemoverCaracterEspecial(string palavra)
        {
            string comAcento = "áàãâäéèêëíìîïóòõôöúùûüç";
            string semAcento = "aaaaaeeeeiiiiooooouuuuc";

            palavra = palavra.ToLower();

            for (int i = 0; i < comAcento.Count(); i++)
                palavra = palavra.Replace(comAcento[i], semAcento[i]);
            return palavra;
        }
    }

}

