using System.Text;
using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Models;

namespace Albums.Flujo.Flujos
{
    public class AlbumFlujo : IAlbumFlujo
    {
        private readonly IAlbumDA _albumDA;
        private readonly IArtistaDA _artistaDA;

        public AlbumFlujo(IAlbumDA albumDA, IArtistaDA artistaDA)
        {
            _albumDA = albumDA;
            _artistaDA = artistaDA;
        }

        public async Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            return await _albumDA.Obtener(artista);
        }

        public async Task<AlbumResponse?> Obtener(Guid Id)
        {
            return await _albumDA.Obtener(Id);
        }

        public async Task<Guid> Agregar(AlbumRequest album)
        {
            var artista = await _artistaDA.Obtener(album.ArtistaId);
            if (artista == null)
                return Guid.Empty;

            return await _albumDA.Agregar(album);
        }

        public async Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            var artista = await _artistaDA.Obtener(album.ArtistaId);
            if (artista == null)
                return false;

            return await _albumDA.Editar(Id, album);
        }

        public async Task<bool> Eliminar(Guid Id)
        {
            return await _albumDA.Eliminar(Id);
        }

        public async Task<string> ExportarCsv()
        {
            var albumes = await _albumDA.Obtener(null);

            var csv = new StringBuilder();
            csv.AppendLine("Id,Titulo,Artista,Genero,AnioLanzamiento,CantidadCanciones");

            foreach (var a in albumes)
            {
                csv.AppendLine($"{a.Id},{Escapar(a.Titulo)},{Escapar(a.Artista)},{Escapar(a.Genero)},{a.AnioLanzamiento},{a.CantidadCanciones}");
            }

            return csv.ToString();
        }

        private static string Escapar(string valor)
        {
            if (valor.Contains(',') || valor.Contains('"'))
                return "\"" + valor.Replace("\"", "\"\"") + "\"";

            return valor;
        }
    }
}
