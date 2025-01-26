using AutoMapper;
using Colex.Configs;
using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Colex.Models.Auxiliares;
using Colex.Repository;
using Colex.ViewModel;
using Colex.ViewModel.Auxiliares;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var mapper = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<Capacidade, CapacidadeViewModels>().ReverseMap();
    cfg.CreateMap<Cliente, ClienteViewModels>().ReverseMap();
    cfg.CreateMap<Componente, ComponenteViewModels>().ReverseMap();
    cfg.CreateMap<Extintor, ExtintorViewModels>().ReverseMap();
    cfg.CreateMap<Fornecedor, FornecedorViewModels>().ReverseMap();
    cfg.CreateMap<MarcaExtintor, MarcaExtintorViewModels>().ReverseMap();
    cfg.CreateMap<MateriaPrima, MateriaPrimaViewModels>().ReverseMap();
    cfg.CreateMap<Representante, RepresentanteViewModels>().ReverseMap();
    cfg.CreateMap<Selo, SeloViewModels>().ReverseMap();
    cfg.CreateMap<Os, OsViewModels>().ReverseMap();
    cfg.CreateMap<RelatorioItens, RelatorioItensViewModels>().ReverseMap();
    cfg.CreateMap<ClienteAutoComplete, ClienteAutoCompleteViewModels>().ReverseMap();
    cfg.CreateMap<Usuario, UsuarioViewModels>().ReverseMap();
    cfg.CreateMap<Orcamento, OrcamentoViewModels>().ReverseMap();
    cfg.CreateMap<OrcamentoProduto, OrcamentoProdutoViewModels>().ReverseMap();
    

});
IMapper _mapper = mapper.CreateMapper();

// Add services to the container
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<BaseContext>(options => options.UseSqlServer(config.GetConnectionString("DataBase")));
builder.Services.AddDbContext<BaseContext>(options => options.UseNpgsql(config.GetConnectionString("DataBase")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ICapacidadeRepository, CapacidadeRepository>();
builder.Services.AddScoped<IClienteRepository,ClienteRepository>();
builder.Services.AddScoped<IComponenteRepository, ComponenteRepository>(); 
builder.Services.AddScoped<IExtintorRepository, ExtiontorRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IMarcaExintorRepository, MarcaExintorRepository>();
builder.Services.AddScoped<IMateriaPrimaRepository, MateriaPrimaRepository>();
builder.Services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
builder.Services.AddScoped<ISeloRepository, SeloRepository>();
builder.Services.AddScoped<IOsRepository, OsRepository>();
builder.Services.AddScoped<IRelatorioItensRepository, RelatorioItensRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
builder.Services.AddScoped<IOrcamentoProdutoRepository, OrcamentoProdutoRepository>();
builder.Services.AddScoped<BaseContext>();
builder.Services.AddDbContext<BaseContext>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSingleton(_mapper);

builder.Services.AddMvc(options =>
{
    //options.Filters.Add(new ValidationAuthentication());
    options.Filters.AddService<ValidacaoAutenticacao>();

    options.MaxModelBindingCollectionSize = int.MaxValue;
});

builder.Services.AddScoped<ValidacaoAutenticacao>();

builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(9.5);
});
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",  
    pattern: "{controller=Home}/{action=Index}/{id?}");
    




app.Run();
