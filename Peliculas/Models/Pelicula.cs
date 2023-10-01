namespace Peliculas.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Genero { get; set; }
        public string? FechaEstreno { get; set; }
        public string? URLImagen { get; set; }
    }
}
