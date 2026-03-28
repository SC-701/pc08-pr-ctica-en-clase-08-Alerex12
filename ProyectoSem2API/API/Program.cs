using Abstracciones.Interfaces.Flujo;
using Flujo;
using DA;
using Abstracciones.Interfaces.DA;
using DA.Repositorios;
using Abstracciones.Interfaces.Reglas;
using Reglas;
using Abstracciones.Interfaces.Servicios;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IProductoReglas, ProductoReglas>();
builder.Services.AddScoped<ITipoCambioServicio, TipoCambioServicio>();
builder.Services.AddHttpClient<ITipoCambioServicio, TipoCambioServicio>();
builder.Services.AddScoped<ICategoriaDA, CategoriaDA>();
builder.Services.AddScoped<ICategoriaFlujo, CategoriaFlujo>();
builder.Services.AddScoped<ISubcategoriaFlujo, SubcategoriaFlujo>();
builder.Services.AddScoped<ISubcategoriaDA, SubcategoriaDA>();

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
