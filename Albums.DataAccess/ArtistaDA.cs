using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Models;
using Albums.DataAccess.Contexto;
using Albums.DataAccess.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Albums.DataAccess
{
    public class ArtistaDA : IArtistaDA
    {
        private readonly AlbumContexto _contexto;

        public ArtistaDA(AlbumContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<ArtistaResponse>> Obtener()
        {
            return await _contexto.Artistas
                .AsNoTracking()
                .OrderBy(a => a.Nombre)
                .Select(a => new ArtistaResponse { Id = a.Id, Nombre = a.Nombre, Pais = a.Pais })
                .ToListAsync();
        }

        public async Task<ArtistaResponse?> Obtener(Guid Id)
        {
            return await _contexto.Artistas
                .AsNoTracking()
                .Where(a => a.Id == Id)
                .Select(a => new ArtistaResponse { Id = a.Id, Nombre = a.Nombre, Pais = a.Pais })
                .FirstOrDefaultAsync();    
        }

        public async Task<Guid> Agregar(ArtistaRequest artista)
        {
            var entidad = new ArtistaEntidad
            {
                Id = Guid.NewGuid(),
                Nombre = artista.Nombre,
                Pais = artista.Pais
            };

            _contexto.Artistas.Add(entidad);
            await _contexto.SaveChangesAsync();
            return entidad.Id;
        }
    }
}