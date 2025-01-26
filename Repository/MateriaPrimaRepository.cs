using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Colex.Repository
{
    public class MateriaPrimaRepository : RepositoryBase<MateriaPrima>, IMateriaPrimaRepository
    {

        private readonly IMapper _mapper;
        private readonly ICapacidadeRepository _capacidadeRepository;
        public MateriaPrimaRepository(BaseContext context, IMapper mapper, ICapacidadeRepository capacidadeRepository) : base(context)
        {
            _mapper = mapper;
            _capacidadeRepository = capacidadeRepository;
        }

        public List<MateriaPrima> GetAllComplete()
        {
            List<MateriaPrima> materia = Db.Set<MateriaPrima>()
                .Include(m => m.Fornecedor)
                .ToList();

            return materia;
        }
        public void AlterarEstoqueMateriaPrima(int idMateriaPrima, int idCapacidade)
        {
            var materiaPrimaAtiva = this.GetById(idMateriaPrima);
            var capacidade = _capacidadeRepository.GetById(idCapacidade).CapacidadeCarga;
            float capacidadeConvertida = Convert.ToSingle(capacidade);
            materiaPrimaAtiva.QuantidadeAtual = materiaPrimaAtiva.QuantidadeAtual - capacidadeConvertida;

            this.Update(materiaPrimaAtiva);
        }
        public List<MateriaPrimaViewModels> PaginacaoMateriaPrima(string materia, int paginaAtual, out int totalMateria)
        {
            int limite = 15;

            List<MateriaPrima> listMateria = this.GetAll();

            if (!string.IsNullOrEmpty(materia))
            {
                var materiaArray = materia.ToLower().Split(" ");
                foreach (var item in materiaArray)
                {
                    listMateria = (List<MateriaPrima>)listMateria.Where(r => r.Nome.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalMateria = listMateria.Count();
            List<MateriaPrima> paginacao = listMateria.Skip((paginaAtual - 1) * limite).Take(limite).OrderByDescending(l => l.Data).ToList();
            return _mapper.Map<List<MateriaPrimaViewModels>>(paginacao);
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
