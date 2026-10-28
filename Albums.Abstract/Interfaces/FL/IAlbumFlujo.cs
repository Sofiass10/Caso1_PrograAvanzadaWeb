using Albums.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.Abstract.Interfaces.FL
{
    public interface IAlbumFlujo
    {
        Task<IEnumerable<AlbumResponse>> Obtener(string artista);
        Task<AlbumResponse> Obtener(Guid Id);
        Task<Guid> Agregar(AlbumRequest album);
        Task<bool> Editar(Guid Id, AlbumRequest album);
        Task<bool> Eliminar(Guid Id);
    }

}
