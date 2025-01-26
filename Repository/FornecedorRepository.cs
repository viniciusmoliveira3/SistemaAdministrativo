using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;

namespace Colex.Repository
{
    public class FornecedorRepository : RepositoryBase<Fornecedor>, IFornecedorRepository
    {
        private readonly IMapper _mapper;
        public FornecedorRepository(BaseContext context, IMapper mapper ): base(context)
        {
            _mapper = mapper;
        }

        public List<FornecedorViewModels> PaginacaoFornecedor(string fornecedor, int paginaAtual, out int totalFornecedor)
        {
            int limite = 15;

            List<Fornecedor> listFornecedor = this.GetAll();

            if (!string.IsNullOrEmpty(fornecedor))
            {
                var fornecedorArray = fornecedor.ToLower().Split(" ");
                foreach (var item in fornecedorArray)
                {
                    listFornecedor = (List<Fornecedor>)listFornecedor.Where(r => r.NomeFantasia.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalFornecedor = listFornecedor.Count();
            List<Fornecedor> paginacao = listFornecedor.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<FornecedorViewModels>>(paginacao);
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
