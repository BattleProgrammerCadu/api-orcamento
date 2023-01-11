using api.Models;
using api.Repositorios;
using api.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServico<PessoaFisica>, PessoaFisicaRepositorio>();
builder.Services.AddScoped<IServico<PessoaJuridica>, PessoaJuridicaRepositorio>();
builder.Services.AddScoped<IServico<Orcamento>, OrcamentoRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline. q
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
