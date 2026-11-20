using System.Text;
using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Models;
using Microsoft.AspNetCore.Mvc;
using Albums.Abstract.Interfaces.API;

namespace Albums.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase, IAlbumController
    {
        private readonly IAlbumFlujo _albumFlujo;

        public AlbumController(IAlbumFlujo albumFlujo)
        {
            _albumFlujo = albumFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener([FromQuery] string? artista)
        {
            var albumes = await _albumFlujo.Obtener(artista);
            return Ok(albumes);
        }

        [HttpGet("{Id:guid}", Name = "ObtenerAlbumPorId")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var album = await _albumFlujo.Obtener(Id);
            if (album == null)
                return NotFound();

            return Ok(album);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] AlbumRequest album)
        {
            var id = await _albumFlujo.Agregar(album);
            if (id == Guid.Empty)
                return BadRequest("El artista indicado no existe.");

            var albumCreado = await _albumFlujo.Obtener(id);
            return CreatedAtRoute("ObtenerAlbumPorId", new { Id = id }, albumCreado);
        }

        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] AlbumRequest album)
        {
            var editado = await _albumFlujo.Editar(Id, album);
            if (!editado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            var eliminado = await _albumFlujo.Eliminar(Id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("exportar")]
        public async Task<IActionResult> Exportar()
        {
            var csv = await _albumFlujo.ExportarCsv();
            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv)).ToArray();

            return File(bytes, "text/csv", "albumes.csv");
        }
    }
}
