using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Albums.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumFlujo _albumFlujo;

        public AlbumController(IAlbumFlujo albumFlujo)
        {
            _albumFlujo = albumFlujo;
        }

        // GET api/Album?artista={texto}
        [HttpGet]
        public async Task<IActionResult> Obtener([FromQuery] string artista = "")
        {
            var resultado = await _albumFlujo.Obtener(artista);
            return Ok(resultado);
        }

        // GET api/Album/{Id}
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var album = await _albumFlujo.Obtener(Id);

            if (album == null)
                return NotFound(new { mensaje = "Álbum no encontrado" });

            return Ok(album);
        }

        // POST api/Album
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] AlbumRequest album)
        {
            var nuevoId = await _albumFlujo.Agregar(album);
            return CreatedAtAction(nameof(Obtener), new { Id = nuevoId }, new { id = nuevoId });
        }

        // PUT api/Album/{Id}
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] AlbumRequest album)
        {
            var editado = await _albumFlujo.Editar(Id, album);

            if (!editado)
                return NotFound(new { mensaje = "Álbum no encontrado para editar" });

            return NoContent();
        }

        // DELETE api/Album/{Id}
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            var eliminado = await _albumFlujo.Eliminar(Id);

            if (!eliminado)
                return NotFound(new { mensaje = "Álbum no encontrado para eliminar" });

            return NoContent();
        }
    }
}