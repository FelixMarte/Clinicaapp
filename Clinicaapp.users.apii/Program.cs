using BoletosApp.Persistance.Context;
using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Services;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Clinicaapp.Persistence.Repositories.Configuracion;
using Clinicaapp.Persistence.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura la conexión a la base de datos
builder.Services.AddDbContext<ClinicaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaDb")));

// Registro de los repositorios
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();

// Registro de los servicios
builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClinicaApp API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configura el pipeline para el manejo de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClinicaApp API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

