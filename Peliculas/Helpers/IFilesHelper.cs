namespace Peliculas.Helpers
{
    public interface IFilesHelper
    {
        Task<string> SubirArchivo(Stream archivo, string nombre);
    }
}
