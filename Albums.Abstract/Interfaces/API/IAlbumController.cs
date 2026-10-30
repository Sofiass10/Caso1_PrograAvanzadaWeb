using Albums.Abstract.Models;
using Microsoft.AspNetCore.Mvc;

namespace Albums.Abstract.Interfaces.API
{
    public interface IAlbumController
    {
        Task<IActionResult> Obtener(string? artista);
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(AlbumRequest album);
        Task<IActionResult> Editar(Guid Id, AlbumRequest album);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
