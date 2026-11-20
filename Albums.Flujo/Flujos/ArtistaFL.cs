using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Interfaces.FL;
using Albums.Abstract.Models;

namespace Albums.Flujo.Flujos
{
    public class ArtistaFlujo : IArtistaFlujo
    {
        private readonly IArtistaDA _artistaDA;

        public ArtistaFlujo(IArtistaDA artistaDA)
        {
            _artistaDA = artistaDA;
        }

        public async Task<IEnumerable<ArtistaResponse>> Obtener()
        {
            return await _artistaDA.Obtener();
        }

        public async Task<ArtistaResponse?> Obtener(Guid Id)
        {
            return await _artistaDA.Obtener(Id);
        }

        public async Task<Guid> Agregar(ArtistaRequest artista)
        {
            return await _artistaDA.Agregar(artista);
        }
    }
}
