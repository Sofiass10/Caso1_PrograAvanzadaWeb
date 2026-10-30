using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Models;

namespace Albums.DataAccess
{
    public class AlbumnesDA : IAlbumDA
    {
        private static readonly List<AlbumResponse> _albumes = new List<AlbumResponse>
        {
            new AlbumResponse
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Titulo = "Thriller",
                Artista = "Michael Jackson",
                Genero = "Pop",
                AnioLanzamiento = 1982,
                CantidadCanciones = 9
            },
            new AlbumResponse
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Titulo = "Abbey Road",
                Artista = "The Beatles",
                Genero = "Rock",
                AnioLanzamiento = 1969,
                CantidadCanciones = 17
            },
            new AlbumResponse
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Titulo = "Un Verano Sin Ti",
                Artista = "Bad Bunny",
                Genero = "Reggaetón",
                AnioLanzamiento = 2022,
                CantidadCanciones = 23
            }
        };

        public Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            if (string.IsNullOrWhiteSpace(artista))
                return Task.FromResult<IEnumerable<AlbumResponse>>(_albumes.ToList());

            var resultado = _albumes
                .Where(a => a.Artista.Contains(artista, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult<IEnumerable<AlbumResponse>>(resultado);
        }

        public Task<AlbumResponse?> Obtener(Guid Id)
        {
            var album = _albumes.FirstOrDefault(a => a.Id == Id);
            return Task.FromResult(album);
        }

        public Task<Guid> Agregar(AlbumRequest album)
        {
            var nuevoAlbum = new AlbumResponse
            {
                Id = Guid.NewGuid(),
                Titulo = album.Titulo,
                Artista = album.Artista,
                Genero = album.Genero,
                AnioLanzamiento = album.AnioLanzamiento,
                CantidadCanciones = album.CantidadCanciones
            };

            _albumes.Add(nuevoAlbum);
            return Task.FromResult(nuevoAlbum.Id);
        }

        public Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            var albumExistente = _albumes.FirstOrDefault(a => a.Id == Id);
            if (albumExistente == null)
                return Task.FromResult(false);

            albumExistente.Titulo = album.Titulo;
            albumExistente.Artista = album.Artista;
            albumExistente.Genero = album.Genero;
            albumExistente.AnioLanzamiento = album.AnioLanzamiento;
            albumExistente.CantidadCanciones = album.CantidadCanciones;

            return Task.FromResult(true);
        }

        public Task<bool> Eliminar(Guid Id)
        {
            var albumExistente = _albumes.FirstOrDefault(a => a.Id == Id);
            if (albumExistente == null)
                return Task.FromResult(false);

            _albumes.Remove(albumExistente);
            return Task.FromResult(true);
        }
    }
}
