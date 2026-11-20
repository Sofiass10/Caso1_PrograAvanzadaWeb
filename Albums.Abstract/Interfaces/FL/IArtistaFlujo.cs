using Albums.Abstract.Models;

namespace Albums.Abstract.Interfaces.FL
{
    public interface IArtistaFlujo
    {
        Task<IEnumerable<ArtistaResponse>> Obtener();
        Task<ArtistaResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(ArtistaRequest artista);
    }
}
