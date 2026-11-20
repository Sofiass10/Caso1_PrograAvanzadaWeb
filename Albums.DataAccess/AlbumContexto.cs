using Albums.DataAccess.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Albums.DataAccess.Contexto
{
    public class AlbumContexto : DbContext
    {
        public AlbumContexto(DbContextOptions<AlbumContexto> options) : base(options) { }

        public DbSet<ArtistaEntidad> Artistas { get; set; }
        public DbSet<AlbumEntidad> Albumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistaEntidad>()
                .ToTable("Artistas")
                .HasKey(a => a.Id);

            modelBuilder.Entity<ArtistaEntidad>()
                .Property(a => a.Nombre).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<ArtistaEntidad>()
                .Property(a => a.Pais).IsRequired().HasMaxLength(60);

            modelBuilder.Entity<AlbumEntidad>()
                .ToTable("Albumes")
                .HasKey(a => a.Id);

            modelBuilder.Entity<AlbumEntidad>()
                .Property(a => a.Titulo).IsRequired().HasMaxLength(150);

            modelBuilder.Entity<AlbumEntidad>()
                .Property(a => a.Genero).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<AlbumEntidad>()
                .Property(a => a.AnioLanzamiento).IsRequired();

            modelBuilder.Entity<AlbumEntidad>()
                .Property(a => a.CantidadCanciones).IsRequired();

            modelBuilder.Entity<AlbumEntidad>()
                .HasOne(a => a.Artista)
                .WithMany(r => r.Albumes)
                .HasForeignKey(a => a.ArtistaId)
                .OnDelete(DeleteBehavior.Restrict);

            var jackson = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var beatles = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var badBunny = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            modelBuilder.Entity<ArtistaEntidad>().HasData(
                new ArtistaEntidad { Id = jackson, Nombre = "Michael Jackson", Pais = "Estados Unidos" },
                new ArtistaEntidad { Id = beatles, Nombre = "The Beatles", Pais = "Reino Unido" },
                new ArtistaEntidad { Id = badBunny, Nombre = "Bad Bunny", Pais = "Puerto Rico" }
            );

            modelBuilder.Entity<AlbumEntidad>().HasData(
                new AlbumEntidad
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Titulo = "Thriller",
                    Genero = "Pop",
                    AnioLanzamiento = 1982,
                    CantidadCanciones = 9,
                    ArtistaId = jackson
                },
                new AlbumEntidad
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Titulo = "Abbey Road",
                    Genero = "Rock",
                    AnioLanzamiento = 1969,
                    CantidadCanciones = 17,
                    ArtistaId = beatles
                },
                new AlbumEntidad
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Titulo = "Un Verano Sin Ti",
                    Genero = "Reggaetón",
                    AnioLanzamiento = 2022,
                    CantidadCanciones = 23,
                    ArtistaId = badBunny
                }
            );
        }
    }
}