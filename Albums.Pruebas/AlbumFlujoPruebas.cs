using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Models;
using Albums.Flujo.Flujos;
using Moq;

namespace Albums.Pruebas
{
    public class AlbumFlujoPruebas
    {
        private readonly Mock<IAlbumDA> _albumDA = new();
        private readonly Mock<IArtistaDA> _artistaDA = new();
        private readonly AlbumFlujo _flujo;

        public AlbumFlujoPruebas()
        {
            _flujo = new AlbumFlujo(_albumDA.Object, _artistaDA.Object);
        }

        [Fact]
        public async Task Agregar_ArtistaNoExiste_RetornaGuidEmptyYNoGuarda()
        {
            var request = new AlbumRequest { Titulo = "Prueba", ArtistaId = Guid.NewGuid() };
            _artistaDA.Setup(d => d.Obtener(request.ArtistaId))
                      .ReturnsAsync((ArtistaResponse?)null);

            var resultado = await _flujo.Agregar(request);

            Assert.Equal(Guid.Empty, resultado);
            _albumDA.Verify(d => d.Agregar(It.IsAny<AlbumRequest>()), Times.Never);
        }

        [Fact]
        public async Task Agregar_ArtistaExiste_GuardaYRetornaId()
        {
            var artistaId = Guid.NewGuid();
            var idEsperado = Guid.NewGuid();
            var request = new AlbumRequest { Titulo = "Prueba", ArtistaId = artistaId };
            _artistaDA.Setup(d => d.Obtener(artistaId))
                      .ReturnsAsync(new ArtistaResponse { Id = artistaId, Nombre = "Artista", Pais = "Costa Rica" });
            _albumDA.Setup(d => d.Agregar(request))
                    .ReturnsAsync(idEsperado);

            var resultado = await _flujo.Agregar(request);

            Assert.Equal(idEsperado, resultado);
            _albumDA.Verify(d => d.Agregar(It.IsAny<AlbumRequest>()), Times.Once);
        }

        [Fact]
        public async Task ExportarCsv_GeneraEncabezadoYUnaFilaPorAlbum()
        {
            var lista = new List<AlbumResponse>
            {
                new AlbumResponse { Id = Guid.NewGuid(), Titulo = "Thriller", Artista = "Michael Jackson", Genero = "Pop", AnioLanzamiento = 1982, CantidadCanciones = 9 },
                new AlbumResponse { Id = Guid.NewGuid(), Titulo = "Abbey Road", Artista = "The Beatles", Genero = "Rock", AnioLanzamiento = 1969, CantidadCanciones = 17 }
            };
            _albumDA.Setup(d => d.Obtener(It.IsAny<string?>()))
                    .ReturnsAsync(lista);

            var resultado = await _flujo.ExportarCsv();
            var lineas = resultado.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(3, lineas.Length);
            Assert.Equal("Id,Titulo,Artista,Genero,AnioLanzamiento,CantidadCanciones", lineas[0]);
        }
    }
}
