using Microsoft.EntityFrameworkCore;
using PruebaHotep.WebApi;
using PruebaHotep.WebApi.Data;
using PruebaHotep.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

//agregar la bd (el contexto)
builder.Services.AddDbContext<ContextoBD>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//agregar controladores
builder.Services.AddControllers();

//agregar servicios 
builder.Services.AddScoped<IServicioCliente, ServicioCliente>();
builder.Services.AddScoped<IServicioCuenta, ServicioCuenta>();
builder.Services.AddScoped<IServicioTransaccion, ServicioTransaccion>();



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
