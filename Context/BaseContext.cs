using Colex.Mapping;
using Colex.Models;
using Colex.ViewModel.Auxiliares;
using Microsoft.EntityFrameworkCore;

namespace Colex.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext() 
        { 
        
        }
        
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CapacidadeMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ComponenteMap());
            modelBuilder.ApplyConfiguration(new ExtintorMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new MarcaExtintorMap());
            modelBuilder.ApplyConfiguration(new MateriaPrimaMap());
            modelBuilder.ApplyConfiguration(new RepresentanteMap());
            modelBuilder.ApplyConfiguration(new SeloMap());
            modelBuilder.ApplyConfiguration(new OsMap());
            modelBuilder.ApplyConfiguration(new Os_RelatorioMap());
            modelBuilder.ApplyConfiguration(new RelatorioItensMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new OrcamentoMap());
            modelBuilder.ApplyConfiguration(new OrcamentoProdutoMap());

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("DataBase"));
        }

    }
}
