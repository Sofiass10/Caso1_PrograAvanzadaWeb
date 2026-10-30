using Albums.Abstract.Models;

namespace Albums.Abstract.Interfaces.FL
{
    public interface IAlbumFlujo
    {
        Task<IEnumerable<AlbumResponse>> Obtener(string? artista);
        Task<AlbumResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(AlbumRequest album);
        Task<bool> Editar(Guid Id, AlbumRequest album);
        Task<bool> Eliminar(Guid Id);
    }
}
