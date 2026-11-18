using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Models;
using Albums.DataAccess.Contexto;
using Albums.DataAccess.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Albums.DataAccess
{
    public class AlbumnesDA : IAlbumDA
    {
        private readonly AlbumContexto _contexto;

        public AlbumnesDA(AlbumContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            var consulta = _contexto.Albumes
                .Include(a => a.Artista)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(artista))
                consulta = consulta.Where(a => EF.Functions.Like(a.Artista.Nombre, $"%{artista}%"));

            var albumes = await consulta
                .OrderBy(a => a.Titulo)
                .ToListAsync();

            return albumes.Select(AResponse);
        }

        public async Task<AlbumResponse?> Obtener(Guid Id)
        {
            var album = await _contexto.Albumes
                .Include(a => a.Artista)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == Id);

            return album == null ? null : AResponse(album);
        }

        public async Task<Guid> Agregar(AlbumRequest album)
        {
            var entidad = new AlbumEntidad
            {
                Id = Guid.NewGuid(),
                Titulo = album.Titulo,
                Genero = album.Genero,
                AnioLanzamiento = album.AnioLanzamiento,
                CantidadCanciones = album.CantidadCanciones,
                ArtistaId = album.ArtistaId
            };

            _contexto.Albumes.Add(entidad);
            await _contexto.SaveChangesAsync();
            return entidad.Id;
        }

        public async Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            var entidad = await _contexto.Albumes.FindAsync(Id);
            if (entidad == null) return false;

            entidad.Titulo = album.Titulo;
            entidad.Genero = album.Genero;
            entidad.AnioLanzamiento = album.AnioLanzamiento;
            entidad.CantidadCanciones = album.CantidadCanciones;
            entidad.ArtistaId = album.ArtistaId;

            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(Guid Id)
        {
            var entidad = await _contexto.Albumes.FindAsync(Id);
            if (entidad == null) return false;

            _contexto.Albumes.Remove(entidad);
            await _contexto.SaveChangesAsync();
            return true;
        }

        private static AlbumResponse AResponse(AlbumEntidad a) => new AlbumResponse
        {
            Id = a.Id,
            Titulo = a.Titulo,
            Genero = a.Genero,
            AnioLanzamiento = a.AnioLanzamiento,
            CantidadCanciones = a.CantidadCanciones,
            ArtistaId = a.ArtistaId,
            Artista = a.Artista.Nombre
        };
    }
}