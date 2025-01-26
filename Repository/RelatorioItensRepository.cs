using AutoMapper;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Colex.Repository
{
    public class RelatorioItensRepository : RepositoryBase<RelatorioItens>, IRelatorioItensRepository
    {
        private readonly IMapper _mapper;
        private readonly IExtintorRepository _extintorRepository;
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IComponenteRepository _componenteRepository;


        public RelatorioItensRepository(BaseContext context, IExtintorRepository extintorRepository, IMapper mapper, IMateriaPrimaRepository materiaPrimaRepository, IComponenteRepository componenteRepository) : base(context)
        {
            _mapper = mapper;
            _extintorRepository = extintorRepository;
            _materiaPrimaRepository = materiaPrimaRepository;
            _componenteRepository = componenteRepository;
        }


        public List<RelatorioItensViewModels> Adicionar(List<RelatorioItensViewModels> viewModels)
        {
            var idMateriaPrima = 0;
            var idCapacidade = 0;
            var idComponente = 0;
            List<RelatorioItens> relatorioItens = _mapper.Map<List<RelatorioItens>>(viewModels);
            var tipoCargaAgua = _materiaPrimaRepository.GetAll().Where(m => m.Nome.ToLower() == "ap" || m.Nome.ToLower() == "água" || m.Nome.ToLower() == "agua").FirstOrDefault();
            var comoponenteEstoque = _componenteRepository.GetAll().Where(c => c.Ativo == true).ToList();
            relatorioItens?.ForEach(r =>
            {
                r.IdExtintor = r.Extintor.Id;
                r.Os = null;
                r.DataProximaRecarga = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).AddYears(1);

                if (r.IdComponentes1 != 0)
                {
                    _componenteRepository.AlterarEstoqueComponente((int)r.IdComponentes1);
                }
                if (r.IdComponentes2 != 0)
                {
                    _componenteRepository.AlterarEstoqueComponente((int)r.IdComponentes2);
                }
                if (r.IdComponentes3 != 0)
                {
                    _componenteRepository.AlterarEstoqueComponente((int)r.IdComponentes3);
                }
                if (r.IdComponentes4 != 0)
                {
                    _componenteRepository.AlterarEstoqueComponente((int)r.IdComponentes4);
                }
                r.LaudoAR = r.LaudoAR == null ? "" : r.LaudoAR.ToUpper();
                r.Componente1 = null;
                r.Componente2 = null;
                r.Componente3 = null;
                r.Componente4 = null;
                r.IdComponentes1 = r.IdComponentes1 == 0 ? null : r.IdComponentes1;
                r.IdComponentes2 = r.IdComponentes2 == 0 ? null : r.IdComponentes2;
                r.IdComponentes3 = r.IdComponentes3 == 0 ? null : r.IdComponentes3;
                r.IdComponentes4 = r.IdComponentes4 == 0 ? null : r.IdComponentes4;
                if (r.Extintor.IdMateriaPrima != tipoCargaAgua.Id)
                {
                    idMateriaPrima = r.Extintor.IdMateriaPrima;
                    idCapacidade = r.Extintor.IdCapacidade;
                    _materiaPrimaRepository.AlterarEstoqueMateriaPrima(idMateriaPrima, idCapacidade);
                }
                r.Extintor = null;
            });


            foreach (var item in relatorioItens)
            {

                this.Add(item);
            }

            //DbSet.AddRange(relatorioItens);

            return _mapper.Map<List<RelatorioItensViewModels>>(relatorioItens);
        }
        public RelatorioItensViewModels Adicionar(RelatorioItensViewModels viewModels)
        {
            var idMateriaPrima = 0;
            var idCapacidade = 0;
            var idComponente = 0;
            RelatorioItens relatorioItens = _mapper.Map<RelatorioItens>(viewModels);
            var tipoCargaAgua = _materiaPrimaRepository.GetAll().Where(m => m.Nome.ToLower() == "ap" || m.Nome.ToLower() == "água" || m.Nome.ToLower() == "agua").FirstOrDefault();
            var comoponenteEstoque = _componenteRepository.GetAll().Where(c => c.Ativo == true).ToList();

            relatorioItens.IdExtintor = relatorioItens.Extintor.Id;
            relatorioItens.Os = null;
            relatorioItens.DataProximaRecarga = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).AddYears(1);

            if (relatorioItens.IdComponentes1 != 0)
            {
                _componenteRepository.AlterarEstoqueComponente((int)relatorioItens.IdComponentes1);
            }
            if (relatorioItens.IdComponentes2 != 0)
            {
                _componenteRepository.AlterarEstoqueComponente((int)relatorioItens.IdComponentes2);
            }
            if (relatorioItens.IdComponentes3 != 0)
            {
                _componenteRepository.AlterarEstoqueComponente((int)relatorioItens.IdComponentes3);
            }
            if (relatorioItens.IdComponentes4 != 0)
            {
                _componenteRepository.AlterarEstoqueComponente((int)relatorioItens.IdComponentes4);
            }
            relatorioItens.LaudoAR = relatorioItens.LaudoAR == null ? "" : relatorioItens.LaudoAR.ToUpper();
            relatorioItens.Componente1 = null;
            relatorioItens.Componente2 = null;
            relatorioItens.Componente3 = null;
            relatorioItens.Componente4 = null;
            relatorioItens.IdComponentes1 = relatorioItens.IdComponentes1 == 0 ? null : relatorioItens.IdComponentes1;
            relatorioItens.IdComponentes2 = relatorioItens.IdComponentes2 == 0 ? null : relatorioItens.IdComponentes2;
            relatorioItens.IdComponentes3 = relatorioItens.IdComponentes3 == 0 ? null : relatorioItens.IdComponentes3;
            relatorioItens.IdComponentes4 = relatorioItens.IdComponentes4 == 0 ? null : relatorioItens.IdComponentes4;
            if (relatorioItens.Extintor.IdMateriaPrima != tipoCargaAgua.Id)
            {
                idMateriaPrima = relatorioItens.Extintor.IdMateriaPrima;
                idCapacidade = relatorioItens.Extintor.IdCapacidade;
                _materiaPrimaRepository.AlterarEstoqueMateriaPrima(idMateriaPrima, idCapacidade);
            }
            relatorioItens.Extintor = null;
            Add(relatorioItens);

            //DbSet.AddRange(relatorioItens);

            return _mapper.Map<RelatorioItensViewModels>(relatorioItens);
        }
        public List<RelatorioItensViewModels> GetRelatorioByOsid(int idOs)
        {

            List<RelatorioItens> listaRelatorio = GetAllComplete().Where(l => l.IdOServico == idOs).ToList();

            List<RelatorioItensViewModels> relatorio = _mapper.Map<List<RelatorioItensViewModels>>(listaRelatorio);


            return relatorio;

        }

        public List<RelatorioItens> GetAllComplete()
        {
            List<RelatorioItens> relatorioItens = Db.Set<RelatorioItens>()
                .Include(l => l.Extintor)
                    .ThenInclude(l => l.MarcaExtintor)
                .Include(l => l.Extintor)
                    .ThenInclude(l => l.MateriaPrima)
                .Include(l => l.Extintor)
                    .ThenInclude(l => l.Capacidade)
                .Include(l => l.Os)
                    .ThenInclude(o => o.Cliente)
                .Include(l => l.Componente1)
                .Include(l => l.Componente2)
                .Include(l => l.Componente3)
                .Include(l => l.Componente4)
                .ToList();
            return relatorioItens;
        }
        public List<RelatorioItens> GetAllCompleteCliente()
        {
            List<RelatorioItens> relatorioItens = Db.Set<RelatorioItens>()
                .Include(l => l.Extintor)
                    .ThenInclude(l => l.MarcaExtintor)
                .Include(l => l.Extintor)
                    .ThenInclude(l => l.MateriaPrima)
                .Include(l => l.Extintor)
                    .ThenInclude(l => l.Capacidade)
                .Include(l => l.Os)
                    .ThenInclude(o => o.Cliente)
                .ToList();
            return relatorioItens;
        }
        public List<RelatorioItens> GetExtintorByCliente(int idCliente, int data)
        {
            List<RelatorioItens> extintoresCompleto = this.GetAllCompleteCliente();
            var extintorCliente = extintoresCompleto.Where(e => e.Os.Cliente.Id == idCliente && e.Os.DataAbertura.Year == data).ToList();

            return extintorCliente;
        }

        public RelatorioItens GetExtintorBySelo(long selo)
        {
            List<RelatorioItens> extintoresCompletos = this.GetAllCompleteCliente();

            var extintorSelo = extintoresCompletos.FirstOrDefault(e => e.NumSelo == selo);

            return extintorSelo;
        }
        public List<RelatorioItensViewModels> GetExtintiorByLote(int carga, string lote)
        {
            List<RelatorioItens> extintiresCompleto = this.GetAllCompleteCliente().Where(e => e.Extintor.Lote == lote && e.Extintor.IdMateriaPrima == carga).ToList();

            return _mapper.Map<List<RelatorioItensViewModels>>(extintiresCompleto);
        }
        public List<RelatorioItensViewModels> GetRelaotrioItensByNumeroOs(string numeroOs, int data)
        {
            List<RelatorioItens> extintores = this.GetAllCompleteCliente().Where(e => e.Os.NumeroOrdemServico == numeroOs.Trim() && e.Os.DataAbertura.Year == data).ToList();

            return _mapper.Map<List<RelatorioItensViewModels>>(extintores);
        }
        public RelatorioItensViewModels GetEtiquetasByNumeroExtintior(string numeroExtintor, int data)
        {
            RelatorioItens extintiresCompleto = this.GetAllCompleteCliente().Where(e => e.Extintor.NumeroCilindro == numeroExtintor && e.Os.DataAbertura.Year == data).First();

            return _mapper.Map<RelatorioItensViewModels>(extintiresCompleto);
        }

        public List<RelatorioItensViewModels> GetRelatorioExtintor(string numeroExtintor)
        {
            List<RelatorioItens> listExtintor = GetAllCompleteCliente().Where(e => e.Extintor.NumeroCilindro == numeroExtintor).ToList();

            return _mapper.Map<List<RelatorioItensViewModels>>(listExtintor);
        }
    }
}
