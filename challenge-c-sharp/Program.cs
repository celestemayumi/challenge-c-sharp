using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Config;
using challenge_c_sharp.Repositories;
using challenge_c_sharp.Models;
using challenge_c_sharp.Services;
using challenge_c_sharp.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Config do banco de dados e log
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDbContext"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information);
});

// Registra os repositórios com suas interfaces
builder.Services.AddScoped<IGenericRepository<DentistaDto>, DentistaRepository>();
builder.Services.AddScoped<DentistaService>();
builder.Services.AddScoped<IGenericRepository<PacienteDto>, PacienteRepository>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<IGenericRepository<ConsultaDto>, ConsultaRepository>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<IGenericRepository<LoginDto>, LoginRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IGenericRepository<EnderecoDto>, EnderecoRepository>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddScoped<IGenericRepository<BairroDto>, BairroRepository>();
builder.Services.AddScoped<BairroService>();
builder.Services.AddScoped<IGenericRepository<CidadeDto>, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();
builder.Services.AddScoped<IGenericRepository<EstadoDto>, EstadoRepository>();
builder.Services.AddScoped<EstadoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

// Construir o aplicativo
var app = builder.Build();

app.UseCors("AllowAll"); // Usar a política CORS configurada

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Habilita a interface do Swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
