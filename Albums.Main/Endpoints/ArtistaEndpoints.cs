using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Models;

namespace Albums.API.Endpoints
{
    public static class ArtistaEndpoints
    {
        public static void MapArtistaEndpoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/api/artistas");

            grupo.MapGet("/", async (IArtistaFlujo flujo) =>
            {
                var artistas = await flujo.Obtener();
                return Results.Ok(artistas);
            });

            grupo.MapGet("/{id:guid}", async (Guid id, IArtistaFlujo flujo) =>
            {
                var artista = await flujo.Obtener(id);
                return artista is null ? Results.NotFound() : Results.Ok(artista);
            })
            .WithName("ObtenerArtistaPorId");

            grupo.MapPost("/", async (ArtistaRequest artista, IArtistaFlujo flujo) =>
            {
                var id = await flujo.Agregar(artista);
                var creado = await flujo.Obtener(id);
                return Results.CreatedAtRoute("ObtenerArtistaPorId", new { id }, creado);
            });
        }
    }
}
