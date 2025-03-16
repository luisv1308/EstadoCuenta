using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.Data;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Hubs;
using EstadoCuenta.Api.Repositories;
using EstadoCuenta.Api.Services;
using EstadoCuenta.Api.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;
using HealthChecks.UI.Client;
using EstadoCuenta.Api.Filters;



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
builder.Services.AddScoped<IEstadoCuentaRepository, EstadoCuentaRepository>();
builder.Services.AddScoped<IPagosRepository, PagosRepository>();
builder.Services.AddScoped<IComprasRepository, ComprasRepository>();

builder.Services.AddMediatR(typeof(GetTarjetaByIdQuery).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<TarjetaCreditoValidator>();
});
builder.Services.AddScoped<IExportService, ExportService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("https://localhost:7025", "https://127.0.0.1:7025")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});
builder.Services.AddSignalR();
builder.Services.AddScoped(typeof(ValidationFilter<>));
builder.Services.AddScoped<TransaccionNotificationService>();


builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddPrivateMemoryHealthCheck(maximumMemoryBytes: 512 * 1024 * 1024, name: "Uso de Memoria") // 512MB
    .AddDiskStorageHealthCheck(setup =>
    {
        setup.AddDrive("C:\\", minimumFreeMegabytes: 1024); // Verifica si hay al menos 1GB libre en C:
    }, name: "Espacio en Disco");

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

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
app.UseMiddleware<EstadoCuenta.Api.Middleware.GlobalExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapHub<TransaccionesHub>("/transaccionesHub").RequireCors("AllowFrontend");

app.MapControllers();

app.Run();
