using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Colex.Repository
{
    public class ComponenteRepository : RepositoryBase<Componente>, IComponenteRepository
    {
        private readonly IMapper _mapper;
        public ComponenteRepository(BaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<Componente> GetAllComplete()
        {
            List<Componente> componente = Db.Set<Componente>()
                .Include(c => c.Fornecedor)
                .ToList();

            return componente;

        }
        public void AlterarEstoqueComponente(int idComponente)
        {
            var componente = this.GetById(idComponente);

            componente.QuantidadeAtual = componente.QuantidadeAtual - 1;
            this.Update(componente);

        }
        public List<ComponenteViewModels> PaginacaoComponente(string componente, int paginaAtual, out int totalComponente)
        {
            int limite = 15;

            List<Componente> listComponente = this.GetAll();

            if (!string.IsNullOrEmpty(componente))
            {
                var componenteArray = componente.ToLower().Split(" ");
                foreach (var item in componenteArray)
                {
                    listComponente = (List<Componente>)listComponente.Where(r => r.Nome.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalComponente = listComponente.Count();
            List<Componente> paginacao = listComponente.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<ComponenteViewModels>>(paginacao);
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
