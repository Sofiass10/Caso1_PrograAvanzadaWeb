using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albums.Flujo.Flujos
{
    public class AlbumFlujo : IAlbumFlujo
    {
        private readonly IAlbumDA _albumDA;

        public AlbumFlujo(IAlbumDA albumDA)
        {
            _albumDA = albumDA;
        }

        public async Task<IEnumerable<AlbumResponse>> Obtener(string artista)
        {
            return await _albumDA.Obtener(artista);
        }

        public async Task<AlbumResponse> Obtener(Guid Id)
        {
            return await _albumDA.Obtener(Id);
        }

        public async Task<Guid> Agregar(AlbumRequest album)
        {
            return await _albumDA.Agregar(album);
        }

        public async Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            return await _albumDA.Editar(Id, album);
        }

        public async Task<bool> Eliminar(Guid Id)
        {
            return await _albumDA.Eliminar(Id);
        }
    }
}