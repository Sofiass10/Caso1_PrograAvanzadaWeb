using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Interfaces.FL;
using Albums.API.Endpoints;
using Albums.DataAccess;
using Albums.DataAccess.Contexto;
using Albums.Flujo.Flujos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AlbumContexto>(opciones =>
    opciones.UseSqlite(builder.Configuration.GetConnectionString("Vinilos")));

builder.Services.AddScoped<IAlbumFlujo, AlbumFlujo>();
builder.Services.AddScoped<IAlbumDA, AlbumnesDA>();
builder.Services.AddScoped<IArtistaFlujo, ArtistaFlujo>();
builder.Services.AddScoped<IArtistaDA, ArtistaDA>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<AlbumContexto>();
    await contexto.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.MapArtistaEndpoints();

app.Run();
