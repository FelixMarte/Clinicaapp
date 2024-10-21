using Clinicaapp.Persistence.Context;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Clinicaapp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaappDb")));

//REGISTRO DE DEPENDENCIAS REPOSITORIOS
builder.Services.AddScoped<IDoctorsRepository, DoctorsRepository>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();

//REGISTRO DE DEPENDENCIAS SERVICIOS

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
