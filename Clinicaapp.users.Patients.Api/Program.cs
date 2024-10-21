using BoletosApp.Persistance.Context;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Persistance.Base;
using Clinicaapp.Persistance.Repositories;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Clinicaapp.Persistence.Repositories.Configuracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaDb")));


//El registro de cada una de las dependecias Repositorios de configuration. //
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
