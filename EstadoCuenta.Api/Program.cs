using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
