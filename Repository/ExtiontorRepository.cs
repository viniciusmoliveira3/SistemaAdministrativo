using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.Models.Auxiliares;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace Colex.Repository
{
    public class ExtiontorRepository : RepositoryBase<Extintor>, IExtintorRepository
    {
        
        private readonly IMapper _mapper;
       public ExtiontorRepository(IMapper mapper ,BaseContext context) : base(context)
       {
            
            _mapper = mapper; 

       }

        public List<Extintor> GetAllComplete()
        {
            List<Extintor> extintor = Db.Set<Extintor>()
                .Include(e => e.MateriaPrima)
                .Include(e => e.Capacidade)
                .Include(e => e.MarcaExtintor)
                .ToList();
            return extintor;
        }



            public List<ExtintorViewModels> PesquisarExtintor(string codigoExtintor)
            {
                var lista = this.GetAll();

                if (codigoExtintor != null)
                {
                    lista = lista.Where(e => e.NumeroCilindro == codigoExtintor).ToList();
                }


                  var listaExtintores = lista.OrderBy(e => e.NumeroCilindro).ToList();

                return _mapper.Map<List<ExtintorViewModels>>(listaExtintores);

            }



        public ExtintorViewModels PesquisarExtintor (int idExintor, string numeroCilindro)
        {
            var extintor = this.GetAllComplete();
            List<ExtintorViewModels> lista = _mapper.Map<List<ExtintorViewModels>>(extintor);

            ExtintorViewModels viewmodel = lista.Where(e => e.NumeroCilindro.ToLower() == numeroCilindro.ToLower()).FirstOrDefault();

            
            return viewmodel;
               

        }
        public ExtintorViewModels GetByIdExtintor(int idExintor)
        {
            var extintor = this.GetAllComplete();
            List<ExtintorViewModels> lista = _mapper.Map<List<ExtintorViewModels>>(extintor);

            ExtintorViewModels viewmodel = lista.Where(e => e.Id == idExintor).FirstOrDefault();


            return viewmodel;


        }

        public ExtintorViewModels Alterar (ExtintorViewModels viewModels)
        {
            Extintor extintor = _mapper.Map<Extintor>(viewModels);
            extintor.MarcaExtintor = null;
            extintor.Capacidade = null;
            extintor.MateriaPrima = null;
            extintor.Ativo = true;
            extintor.NumeroCilindro = extintor.NumeroCilindro.ToLower();
            extintor.Projeto = extintor.Projeto != null ? extintor.Projeto.ToLower() : null;
            extintor.CapacExtintora = extintor.CapacExtintora.ToLower();

            this.Update(extintor);

            return _mapper.Map<ExtintorViewModels>(extintor);
        }

        public List<ExtintorViewModels> PaginacaoExtintor(string numeroExtintor, int paginaAtual, out int totalExtintor)
        {
            int limite = 15;

            List<Extintor> listExtintores = this.GetAllComplete();

            if (numeroExtintor != null)
            {
                listExtintores = (List<Extintor>)listExtintores.Where(l => l.NumeroCilindro == numeroExtintor).ToList();
            }

            totalExtintor = listExtintores.Count();
            List<Extintor> paginacao = listExtintores.Skip((paginaAtual - 1) * limite).Take(limite).ToList();
            return _mapper.Map<List<ExtintorViewModels>>(paginacao);
        }
        //public List<ExtintorViewModels> GetExtintiorByLote(int carga, string lote)
        //{
        //    List<Extintor> extintiresCompleto = this.GetAllComplete();

        //    List<Extintor> extintorLote = extintiresCompleto.Where(e => e.Lote == lote && e.IdMateriaPrima == carga).ToList();

        //    return _mapper.Map<List<ExtintorViewModels>>(extintorLote);
        //}

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
