using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Colex.Repository
{
    public class OrcamentoProdutoRepository : RepositoryBase<OrcamentoProduto>, IOrcamentoProdutoRepository
    {
        private IMapper _mapper;
        public OrcamentoProdutoRepository(IMapper mapper,BaseContext context) : base(context)
        {
            _mapper = mapper;
        }
        public List<OrcamentoProdutoViewModels> Adicionar (List<OrcamentoProdutoViewModels> orcamentoProdutoViewModels)
        {
            try
            {
                List<OrcamentoProduto> orcamentoProduto = _mapper.Map<List<OrcamentoProduto>>(orcamentoProdutoViewModels);

                orcamentoProduto.ForEach(o => o.Orcamento = null);
                orcamentoProduto.ForEach(o => o.MateriaPrima = null);
                orcamentoProduto.ForEach(o => o.IdMateriaPrima = o.IdMateriaPrima == 0 ? null : o.IdMateriaPrima);
               
                foreach(var item in orcamentoProduto)
                {
                    Add(item);
                }

                return _mapper.Map<List<OrcamentoProdutoViewModels>>(orcamentoProduto);
            }
            catch (Exception ex) {
                return new List<OrcamentoProdutoViewModels>();
            }
          
        } 
        public List<OrcamentoProdutoViewModels> Alterar(List<OrcamentoProdutoViewModels> viewModels)
        {
            viewModels.ForEach(o => o.Orcamento = null);
            viewModels.ForEach(o => o.MateriaPrima = null);
            viewModels.ForEach(o => o.IdMateriaPrima = o.IdMateriaPrima == 0 ? null : o.IdMateriaPrima);

            List<OrcamentoProduto> orcamentoProdutos = _mapper.Map<List<OrcamentoProduto>>(viewModels);


            foreach (var item in orcamentoProdutos)
            {
                if (item.IdOrcamentoProduto == 0)
                    Add(item);
                else
                    Update(item);
            }

            return _mapper.Map<List<OrcamentoProdutoViewModels>>(orcamentoProdutos);
        }
        public List<OrcamentoProdutoViewModels> GetAllComplete()
        {
            List<OrcamentoProduto> orcamentoProdutos = Db.Set<OrcamentoProduto>()
                .Include(o => o.Orcamento)
                .Include(o => o.MateriaPrima)
                .ToList();

            return _mapper.Map<List<OrcamentoProdutoViewModels>>(orcamentoProdutos);

        }

        public List<OrcamentoProdutoViewModels> GetByIdOrcamento(int IdOrcamento)
        {
            List<OrcamentoProdutoViewModels> list = GetAllComplete().Where(e => e.IdOrcamento == IdOrcamento).ToList(); 
            return list;
        }
    }
}
