using Clinicaapp.Application.contracts;
using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Services;
using Clinicaapp.Persistence.Context;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Clinicaapp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaappDb")));

builder.Services.AddScoped<IDoctorsRepository, DoctorsRepository>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();



builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IPatientService, PatientService>();

builder.Services.AddSingleton(new ApiService("http://localhost:5085/api/"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
