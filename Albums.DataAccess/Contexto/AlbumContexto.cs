using Albums.Abstract.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.DataAccess.Contexto
{
    public class AlbumContexto : DbContext
    {
        public AlbumContexto(DbContextOptions<AlbumContexto> options) : base(options) { }

        public DbSet<AlbumBase> albumnes => Set<AlbumBase>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumBase>(e =>
            {
                e.Property(a => a.Titulo).HasMaxLength(150).IsRequired();
                e.Property(a => a.Artista).HasMaxLength(100).IsRequired();
                e.Property(a => a.Genero).HasMaxLength(50).IsRequired();
            });

            AlbumnesBase(modelBuilder);
        }
        private static void AlbumnesBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumBase>().HasData(
                new AlbumBase
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Titulo = "Thriller",
                    Artista = "Michael Jackson",
                    Genero = "Pop",
                    AnioLanzamiento = 1982,
                    CantidadCanciones = 9
                },
                new AlbumBase
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Titulo = "Abbey Road",
                    Artista = "The Beatles",
                    Genero = "Rock",
                    AnioLanzamiento = 1969,
                    CantidadCanciones = 17
                },
                new AlbumBase
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Titulo = "Un Verano Sin Ti",
                    Artista = "Bad Bunny",
                    Genero = "Reggaetón",
                    AnioLanzamiento = 2022,
                    CantidadCanciones = 23
                });
        }
    }
}
