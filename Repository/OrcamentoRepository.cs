using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Colex.Repository
{
    public class OrcamentoRepository : RepositoryBase<Orcamento>, IOrcamentoRepository
    {
        private readonly IMapper _mapper;
        private readonly IOrcamentoProdutoRepository _orcamentoProdutoRepository;
        public OrcamentoRepository(BaseContext context, IMapper mapper, IOrcamentoProdutoRepository orcamentoProdutoRepository) : base(context)
        {
            _mapper = mapper;
            _orcamentoProdutoRepository = orcamentoProdutoRepository;

        }

        public OrcamentoViewModels Adicionar (OrcamentoViewModels model)
        {
            try
            {
                List<OrcamentoProdutoViewModels> orcamentoProduto = model.OrcamentoProduto;
                Orcamento orcamento = _mapper.Map<Orcamento>(model);

                foreach (var item in orcamentoProduto)
                {
                    orcamento.ValorFinal += item.ValorTotal;
                }
                orcamento.Cliente = orcamento.Cliente.ToUpper();
                orcamento.Email = orcamento.Email != null ? orcamento.Email.ToUpper() : null;
                orcamento.Comprador = orcamento.Comprador != null ? orcamento.Comprador.ToUpper() : null;
                orcamento.Vendedor = orcamento.Vendedor != null ? orcamento.Vendedor.ToUpper() : null;
                Add(orcamento);

                orcamentoProduto.ForEach(o => o.IdOrcamento = orcamento.IdOrcamento);
                orcamentoProduto.ForEach(o => o.Nome = o.Nome.ToUpper());
                _orcamentoProdutoRepository.Adicionar(orcamentoProduto);

                return(_mapper.Map<OrcamentoViewModels>(orcamento));
            }
            catch (Exception ex)
            {
                return new OrcamentoViewModels();
            }

        }
        public OrcamentoViewModels Alterar(OrcamentoViewModels model)
        {
            List<OrcamentoProdutoViewModels> listOrcamentoProduto = model.OrcamentoProduto;
            Orcamento orcamento = _mapper.Map<Orcamento>(model);
            orcamento.ValorFinal = 0;
            listOrcamentoProduto.ForEach(o => o.IdOrcamento = orcamento.IdOrcamento);
            listOrcamentoProduto.ForEach(o => o.Nome = o.Nome.ToUpper());

            foreach (var item in listOrcamentoProduto)
            {
                orcamento.ValorFinal += item.ValorTotal;
            }

            List<OrcamentoProdutoViewModels> orcamentoProdutoViewModels = _orcamentoProdutoRepository.Alterar(listOrcamentoProduto);
            orcamento.Cliente = orcamento.Cliente != null ? orcamento.Cliente.ToUpper() : null;
            orcamento.Email = orcamento.Email != null ? orcamento.Email.ToUpper() : null;
            orcamento.Comprador = orcamento.Comprador != null ? orcamento.Comprador.ToUpper() : null;
            orcamento.Vendedor = orcamento.Vendedor != null ? orcamento.Vendedor.ToUpper() : null;
            Update(orcamento);


            OrcamentoViewModels viewModel = _mapper.Map<OrcamentoViewModels>(orcamento);
            viewModel.OrcamentoProduto = orcamentoProdutoViewModels;

            return viewModel;
        }
        public List<OrcamentoViewModels> GetAllComplete()
        {
            List<Orcamento> orcamento = Db.Set<Orcamento>()
                .Include(o => o.Cliente)
                .ToList();

            return _mapper.Map<List<OrcamentoViewModels>>(orcamento);
        }
        public List<OrcamentoViewModels> PaginacaoOrcamento(string nome, long numeroOrcamento, int paginaAtual, out int totalOrcamento)
        {
            int limite = 15;

            List<Orcamento> listOrcamento = this.GetAll();

            if (numeroOrcamento > 0)
            {
                listOrcamento = listOrcamento.Where(l => l.IdOrcamento == numeroOrcamento).ToList();
            }
            if (!string.IsNullOrEmpty(nome))
            {
                var nomeArray = nome.ToLower().Split(" ");
                foreach (var item in nomeArray)
                {
                    listOrcamento = listOrcamento.Where(r => r.Cliente.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }

            totalOrcamento = listOrcamento.Count();
            List<Orcamento> paginacao = listOrcamento.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            //foreach (var item in paginacao)
            //{
            //    item.RelatorioItens = _mapper.Map<List<RelatorioItens>>(_relatorioItensRepository.GetAllComplete().Where(r => r.IdOServico == item.IdOServico));
            //}

            return _mapper.Map<List<OrcamentoViewModels>>(paginacao);
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
