using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioMigrationController : Controller
    {
        private readonly BL.Usuario _context;

        public UsuarioMigrationController(BL.Usuario context)
        {
            _context = context;
        }//INYECCION DE DEPENDENCIAS

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TablaGetAll()
        {
            return View(/*Pasar modelo*/);
        }
    }
}
