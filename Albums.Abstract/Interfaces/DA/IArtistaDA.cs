using Albums.Abstract.Models;

namespace Albums.Abstract.Interfaces.DA
{
    public interface IArtistaDA
    {
        Task<IEnumerable<ArtistaResponse>> Obtener();
        Task<ArtistaResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(ArtistaRequest artista);
    }
}