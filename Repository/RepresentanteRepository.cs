using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;

namespace Colex.Repository
{
    public class RepresentanteRepository : RepositoryBase<Representante>, IRepresentanteRepository
    {
        private readonly IMapper _mapper;
        public RepresentanteRepository(BaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public List<RepresentanteViewModels> PaginacaoRepresentante(string representante, int paginaAtual, out int totalRepresentante)
        {
            int limite = 15;

            List<Representante> listRepresentante = this.GetAll();

            if (!string.IsNullOrEmpty(representante))
            {
                var representanteArray = representante.ToLower().Split(" ");
                foreach (var item in representanteArray)
                {
                    listRepresentante = (List<Representante>)listRepresentante.Where(r => r.NomeFantasia.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalRepresentante = listRepresentante.Count();
            List<Representante> paginacao = listRepresentante.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<RepresentanteViewModels>>(paginacao);
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
