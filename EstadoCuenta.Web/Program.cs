using EstadoCuenta.Web.Filters;
using EstadoCuenta.Web.Models;
using EstadoCuenta.Web.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITransaccionService<CompraViewModel>, ComprasService>();
builder.Services.AddScoped<ITransaccionService<PagoViewModel>, PagosService>();
builder.Services.AddScoped<HandleErrorFilter>();
builder.Services.AddScoped<ValidationModelFilter>();

var app = builder.Build();

app.UseMiddleware<EstadoCuenta.Web.Middleware.ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
