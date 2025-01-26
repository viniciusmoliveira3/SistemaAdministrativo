using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Colex.Repository
{

    public class CapacidadeRepository : RepositoryBase<Capacidade>, ICapacidadeRepository
    {
        private readonly IMapper _mapper;
        public CapacidadeRepository(BaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<CapacidadeViewModels> PaginacaoCapacidade(string capacidade, int paginaAtual, out int totalCapacidade)
        {
            int limite = 15;

            List<Capacidade> listCapacidade = this.GetAll();

            if (!string.IsNullOrEmpty(capacidade))
            {
                var capacidadeArray = capacidade.ToLower().Split(" ");
                foreach (var item in capacidadeArray)
                {
                    listCapacidade = (List<Capacidade>)listCapacidade.Where(r => r.CapacidadeCarga.ToLower().Replace("á", "a").Replace("é", "e")
                    .Replace("í", "i").Replace("ó", "o").Replace("õ", "o").Replace("ã", "a").Replace("ç", "c").Replace("ú", "u")
                    .Contains(RemoverCaracterEspecial(item))).ToList();
                }
            }
            totalCapacidade = listCapacidade.Count();
            List<Capacidade> paginacao = listCapacidade.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<CapacidadeViewModels>>(paginacao);
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

