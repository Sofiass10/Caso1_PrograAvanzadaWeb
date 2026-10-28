using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Models;
using Albums.DataAccess.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albums.DataAccess
{
    public class AlbumnesDA : IAlbumDA
    {
        private readonly AlbumContexto _Context;

        public AlbumnesDA(AlbumContexto context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<AlbumResponse>> Obtener(string artista)
        {
            var query = _Context.albumnes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(artista))
            {
                query = query.Where(a => a.Artista.ToLower().Contains(artista.ToLower()));
            }

            var listaAlbums = await query.ToListAsync();

            return listaAlbums.Select(a => new AlbumResponse
            {
                Id = a.Id,
                Titulo = a.Titulo,
                Artista = a.Artista,
                Genero = a.Genero,
                AnioLanzamiento = a.AnioLanzamiento,
                CantidadCanciones = a.CantidadCanciones
            }).ToList();
        }
               public async Task<AlbumResponse?> Obtener(Guid Id)
        {
            var album = await _Context.albumnes.FindAsync(Id);

            if (album == null)
                return null;

            return new AlbumResponse
            {
                Id = album.Id,
                Titulo = album.Titulo,
                Artista = album.Artista,
                Genero = album.Genero,
                AnioLanzamiento = album.AnioLanzamiento,
                CantidadCanciones = album.CantidadCanciones
            };
        }

        public async Task<Guid> Agregar(AlbumRequest album)
        {
            var nuevoAlbum = new AlbumBase
            {
                Id = Guid.NewGuid(),
                Titulo = album.Titulo,
                Artista = album.Artista,
                Genero = album.Genero,
                AnioLanzamiento = album.AnioLanzamiento,
                CantidadCanciones = album.CantidadCanciones
            };

            _Context.albumnes.Add(nuevoAlbum);
            await _Context.SaveChangesAsync();

            return nuevoAlbum.Id;
        }

        public async Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            var albumExistente = await _Context.albumnes.FindAsync(Id);

            if (albumExistente == null)
                return false;
            albumExistente.Titulo = album.Titulo;
            albumExistente.Artista = album.Artista;
            albumExistente.Genero = album.Genero;
            albumExistente.AnioLanzamiento = album.AnioLanzamiento;
            albumExistente.CantidadCanciones = album.CantidadCanciones;
            await _Context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Eliminar(Guid Id)
        {
            var albumExistente = await _Context.albumnes.FindAsync(Id);

            if (albumExistente == null)
                return false;

            _Context.albumnes.Remove(albumExistente);
            await _Context.SaveChangesAsync();

            return true;
        }
    }
}