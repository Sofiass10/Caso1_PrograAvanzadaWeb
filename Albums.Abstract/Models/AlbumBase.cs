using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.Abstract.Models
{
    public  class AlbumBase
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Artista { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int AnioLanzamiento { get; set; }
        public int CantidadCanciones { get; set; }
    }

    public class AlbumRequest : AlbumBase
    {
    }

    public class AlbumResponse : AlbumBase
    {
        public Guid Id { get; set; }
    }

}
