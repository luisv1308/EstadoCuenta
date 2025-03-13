using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.Data;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Hubs;
using EstadoCuenta.Api.Repositories;
using EstadoCuenta.Api.Services;
using EstadoCuenta.Api.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios a la API
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Estado de Cuenta API",
        Version = "v1",
        Description = "API para manejar estados de cuenta de tarjetas de crédito"
    });
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITarjetaCreditoRepository, TarjetaCreditoRepository>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();

builder.Services.AddMediatR(typeof(GetTarjetaByIdQuery).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<TarjetaCreditoValidator>();
});
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<ExcelService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estado de Cuenta API v1");
        c.RoutePrefix = "swagger"; // Esto hará que Swagger cargue en la raíz "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHub<TransaccionesHub>("/transaccionesHub").RequireCors("AllowFrontend");

app.MapControllers();

app.Run();
