using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;

namespace Colex.Repository
{
    public class MarcaExintorRepository : RepositoryBase<MarcaExtintor>, IMarcaExintorRepository
    {
        private readonly IMapper _mapper;
        public MarcaExintorRepository(BaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public List<MarcaExtintorViewModels> PaginacaoMarcaExtintor(string marca, int paginaAtual, out int totalMarca)
        {
            int limite = 15;

            List<MarcaExtintor> listMarca = this.GetAll();

            if (!string.IsNullOrEmpty(marca))
            {
                var marcaArray = marca.ToLower().Split(" ");
                foreach (var item in marcaArray)
                {
                    listMarca = (List<MarcaExtintor>)listMarca.Where(r => r.Nome.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalMarca = listMarca.Count();
            List<MarcaExtintor> paginacao = listMarca.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<MarcaExtintorViewModels>>(paginacao);
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
