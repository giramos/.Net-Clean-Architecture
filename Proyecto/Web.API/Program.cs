using Application;
using Infrastructure;
using Web.API;
using Web.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation() // Añadimos la inyección de dependencias de presentación
                .AddInfrastructure(builder.Configuration) // Añadimos la inyección de dependencias de infraestructura
                .AddApplication(); // Añadimos la inyección de dependencias de aplicación

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations(); // Añadimos la migración
}

app.UseExceptionHandler("/error"); // Añadimos el manejador de excepciones

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Añadimos los controladores

app.Run();
