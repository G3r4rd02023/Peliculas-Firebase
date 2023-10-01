using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peliculas.Helpers;
using Peliculas.Models;

namespace Peliculas.Controllers
{
    public class PeliculasController : Controller
    {
        //inyectamos el contexto de datos y la interfaz para acceder a ellos.
        private readonly PeliculaContext _context;
        private readonly IFilesHelper _filesHelper;

        public PeliculasController(PeliculaContext context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Peliculas
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {

                Stream image = Imagen.OpenReadStream();
                //llamamos a nuestra interfaz para subir el archivo
                string urlimagen = await _filesHelper.SubirArchivo(image, Imagen.FileName);

                try
                {

                    //agregamos la url que nos devolvio el metodo SubirArchivo, a nuestro objeto pelicula.
                    pelicula.URLImagen = urlimagen;
                    //agregamos el objeto en base de datos	
                    _context.Add(pelicula);
                    //guardamos los cambios	
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ViewBag.Error(exception.Message);
                }
            }
            return View(pelicula);
        }
    }
}
