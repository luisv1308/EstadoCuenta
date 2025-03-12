using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.Data;
using EstadoCuenta.Api.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

var app = builder.Build();

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
app.MapControllers();

app.Run();
