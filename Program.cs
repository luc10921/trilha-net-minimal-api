using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection")
    );
});

var app = builder.Build();

app.MapGet("/", () => "Hello World! AAA");

app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) => {
    Console.WriteLine("LOgin");
    if (administradorServico.Login(loginDTO) != null){
        Console.WriteLine("Entrou no IOF");
        return Results.Ok("Login com sucesso");
    } else return Results.Unauthorized();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
