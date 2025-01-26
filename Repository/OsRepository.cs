using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Colex.Repository
{
    public class OsRepository : RepositoryBase<Os>, IOsRepository
    {
        private readonly IMapper _mapper;
        private readonly IRelatorioItensRepository _relatorioItensRepository;
        private readonly IClienteRepository _clienteRepository;


        public OsRepository(BaseContext context, IMapper mapper, IRelatorioItensRepository relatorioItensRepository,IClienteRepository clienteRepository ) : base(context)
        {
            _mapper = mapper;
            _relatorioItensRepository = relatorioItensRepository;
            _clienteRepository = clienteRepository;
            
        }

        public List<Os> GetAllComplete()
        {
            List<Os> os = Db.Set<Os>()
                .Include(o => o.Cliente)
                .Include(o => o.Representante)
                .ToList();
            return os;
        }


        public OsViewModels Adicionar(OsViewModels viewModels)
        {
            try
            {

                Os os = _mapper.Map<Os>(viewModels);
                
                List<RelatorioItens> relatorioItens = os.RelatorioItens;

                os.IdCliente = os.Cliente.Id;
                os.RelatorioItens = null;
                os.Cliente = null;
                os.Ativo = true;
                os.Representante = null;
               



                this.Add(os);           
                
                relatorioItens?.ForEach(r => r.IdOServico = os.IdOServico);
                
                if (relatorioItens != null && relatorioItens.Count > 0)
                    _relatorioItensRepository.Adicionar(_mapper.Map<List<RelatorioItensViewModels>>(relatorioItens));

                return _mapper.Map<OsViewModels>(os);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
 

        }
        public OsViewModels Editar (OsViewModels viewModels)
        {
            try
            {
                Os os = _mapper.Map<Os>(viewModels);
                List<RelatorioItensViewModels> relatorioItens = viewModels.RelatorioItens;
                foreach (var item in relatorioItens) { 
                    if(item.IdRelatorioItens == 0)
                    {
                        item.IdOServico = os.IdOServico;
                        _relatorioItensRepository.Adicionar(item);
                    }
                }
                os.RelatorioItens = null;
                os.Cliente = null;
                os.Ativo = true;
                os.Representante = null;
                Update(os);

               Array idrelexclusao = viewModels.ListRelItensExclusao.Trim().Split(',').ToArray();

                foreach(var item in idrelexclusao)
                {
                    var idItem = 0;
                    if (item != "")
                    {
                        idItem = Convert.ToInt16(item);
                    }
                       

                    if (idItem > 0 &&  idItem != null)
                    {
                        RelatorioItens relexcluido = _relatorioItensRepository.GetById(idItem);
                        relexcluido.Os = null;
                        relexcluido.Extintor = null;
                        relexcluido.Componente1 = null;
                        relexcluido.Componente2 = null;
                        relexcluido.Componente3 = null;
                        relexcluido.Componente4 = null;
                        _relatorioItensRepository.Delete(relexcluido);
                    }
                    
                }

                return _mapper.Map<OsViewModels>(os);

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);

            }
        }
        public OsViewModels GetRelatorioById(int id)
        {
            Os os = this.GetAllComplete().Where(o => o.IdOServico == id).First();

            OsViewModels viewModels = _mapper.Map<OsViewModels>(os);

          
            return viewModels;
        }

        public List<OsViewModels> PaginacaoOs(string nome, string numeroOs, int paginaAtual, out int totalOs)
        {
            int limite = 15;

            List<Os>listOs = this.GetAllComplete().OrderByDescending(o => o.IdOServico).ToList();

            if(numeroOs != null)
            {
                listOs = (List<Os>)listOs.Where(l => l.NumeroOrdemServico == numeroOs).ToList();
            }
            if(!string.IsNullOrEmpty(nome))
            {
                var nomeArray = nome.ToLower().Split(" ");
                foreach (var item in nomeArray)
                {
                    listOs = (List<Os>)listOs.Where(r => r.Cliente.NomeFantasia.ToLower().Replace("á", "a").Replace("ê", "e").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }

            totalOs = listOs.Count();
            List<Os> paginacao = listOs.Skip((paginaAtual - 1) *  limite).Take(limite).ToList();
            //foreach (var item in paginacao)
            //{
            //    item.RelatorioItens = _mapper.Map<List<RelatorioItens>>(_relatorioItensRepository.GetAllComplete().Where(r => r.IdOServico == item.IdOServico));
            //}

            return _mapper.Map<List<OsViewModels>>(paginacao);
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

