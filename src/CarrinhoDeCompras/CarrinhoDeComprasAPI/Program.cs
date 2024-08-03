using CarrinhoDeComprasAPI.Repositories.BancoDados;
using CarrinhoDeComprasAPI.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddScoped<CarrinhoRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Carrinho De Compras",
        Version = "v1",
        Description = "Carrinho de compras",
        Contact = new OpenApiContact
        {
            Name = "Siryus Canuto",
            Email = "siryuscanuto@gmail.com",
            Url = new Uri("http://localhost:5217/ItemCarrinho"),
        }

    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

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
