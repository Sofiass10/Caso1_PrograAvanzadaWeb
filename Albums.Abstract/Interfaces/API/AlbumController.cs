using Albums.Abstract.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.Abstract.Interfaces.API
{
    public  interface AlbumController
    {
        Task<IActionResult> Obtener(string artista);
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(AlbumRequest album);
        Task<IActionResult> Editar(Guid Id, AlbumRequest album);
        Task<IActionResult> Eliminar(Guid Id);

    }
}
