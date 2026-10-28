namespace Albums.Abstract.Models
{
    public enum TipoError
    {
        Ninguno,
        Generico,
        NoEncontrado,
        Conflicto
    }

    public class Resultado
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public TipoError Error { get; set; }
    }

    public class Resultado<T> : Resultado
    {
        public T Datos { get; set; }
    }
}
